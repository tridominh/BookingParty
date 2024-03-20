using BirthdayParty.Models;
using BirthdayParty.Models.DTOs;
using BirthdayParty.Repository;
using BirthdayParty.Repository.Interfaces;
using BirthdayParty.Services.Interfaces;
using BirthdayParty.Services.PaymentService.Momo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;


namespace BirthdayParty.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MomoController : ControllerBase
    {
        private readonly MomoService _momoService;
        private readonly MomoConfig _config;
        private readonly IBookingService _bookingService;
        private readonly IGenericRepository<Payment> _paymentService;
        private readonly IRoomService _roomService;

        public MomoController(MomoService momoService, IOptions<MomoConfig> config,
                IBookingService bookingService,
                IGenericRepository<Payment> paymentService,
                IRoomService roomService){
            _momoService = momoService;
            _config = config.Value;
            _bookingService = bookingService;
            _paymentService = paymentService;
            _roomService = roomService;
        }

        [HttpPost("CreateMomoLink")]
        public async Task<ActionResult> CreateMomoLink([FromBody] MomoCreateLinkDto dto)
        {
            var booking = _bookingService.GetBooking(dto.BookingId);
            if(booking == null)
            {
                return BadRequest(new {});
            }
             //check already have booking complete
            var room = _roomService.GetRoomById(booking.RoomId);           
            var bookings = _bookingService.GetAllBookings()
                .Where(b => b.RoomId == room.RoomId).ToList();
            if(bookings.Any(b =>((booking.PartyDateTime >= b.PartyDateTime &&
                booking.PartyDateTime <= b.PartyEndTime) || 
                (booking.PartyEndTime >= b.PartyDateTime &&
                booking.PartyEndTime <= b.PartyEndTime)) && 
                (b.BookingStatus == "Deposit" || b.BookingStatus =="Paid" || 
                 b.BookingStatus == "FullPaying" || b.BookingStatus == "DepositPaying")))
            {
                return BadRequest(new {error = "Room is already booked at this time"});
            }

            //Already paid
            if(booking.BookingStatus == "Paid"){
                return Ok(new { url = dto.RedirectUrl });    
            }

            if(DateTime.Now > booking.PartyDateTime){
                return Ok(new { url = dto.RedirectUrl });
            }
            //Already paid but wrong status
            var payments = _paymentService.GetAll().Where(p => p.BookingId == booking.BookingId);
            if(payments.Sum(p => p.DepositMoney) >= booking.TotalPrice ||
              payments.Any(p => p.DepositMoney == 0))
            {
                _bookingService.UpdateBookingStatus(dto.BookingId, "Paid");
                return Ok(new { url = dto.RedirectUrl });
            }
            long amount = Convert.ToInt64(booking.TotalPrice);
             if(dto.PayType == "fullprice")
            {
                //Already deposit
                if(payments.Count() != 0) {
                    amount = amount - Convert.ToInt64(payments.Sum(p => p.DepositMoney));
                }
            }
            else if(dto.PayType == "deposit")
            {
                amount = amount * 1/4;
            }

            var extraData = new ExtraDataDTO{
                BookingId = dto.BookingId,
                PayType = dto.PayType
            };
            MomoOneTimePaymentRequest request = _momoService.CreateRequestModel(amount, dto.Message, extraData);
            var result = request.GetLink(_config.PaymentUrl);
            return Ok(new { url= result.Item2 });
        }

        [HttpPost("momo-ipn")]
        public async Task<ActionResult> MomoNotification([FromBody] object dto)
        {
            System.Console.WriteLine(dto);
            MomoIpnDto momoIpnDto = JsonConvert.DeserializeObject<MomoIpnDto>(dto.ToString());
             Console.WriteLine($"Partner Code: {momoIpnDto.PartnerCode}");
             Console.WriteLine($"Order ID: {momoIpnDto.OrderId}");
             Console.WriteLine($"Extra Data: {FileConvertUtils.Base64Decode(momoIpnDto.ExtraData)}");
            ExtraDataDTO extraData = JsonConvert.DeserializeObject<ExtraDataDTO>(FileConvertUtils.Base64Decode(momoIpnDto.ExtraData));
            var booking = _bookingService.GetBooking(extraData.BookingId);
            var payments = _paymentService.GetAll().Where(p => p.BookingId == booking.BookingId);

            if(extraData.PayType == "fullprice")
            {
                if(payments.Count() == 0) {
                    _paymentService.Add(new Payment{
                        TotalPrice = booking.TotalPrice,
                        PaymentStatus = "Paid", 
                        DepositMoney = booking.TotalPrice,
                        BookingId = booking.BookingId
                    });
                }
                //allready deposit before
                else{
                    var price = payments.Sum(p => p.DepositMoney);
                    _paymentService.Add(new Payment{
                        TotalPrice = booking.TotalPrice,
                        PaymentStatus = "Paid", 
                        DepositMoney = booking.TotalPrice - price,
                        BookingId = booking.BookingId
                    });
                }
                _bookingService.UpdateBookingStatus(extraData.BookingId, "Paid");
            }
            else if(extraData.PayType == "deposit")
            {
                var price = payments.Sum(p => p.DepositMoney);
                if(price < booking.TotalPrice){
                    _paymentService.Add(new Payment{
                        TotalPrice = booking.TotalPrice,
                        PaymentStatus = "Deposit", 
                        DepositMoney = booking.TotalPrice * 1/4,
                        BookingId = booking.BookingId
                    });
                    _bookingService.UpdateBookingStatus(extraData.BookingId, "Deposit");
                }
                else {
                    _bookingService.UpdateBookingStatus(extraData.BookingId, "Paid");
                }
            }
            return NoContent();
        }

        
    }

    public class MomoCreateLinkDto
    {
        public string Message { get; set;}
        public string PayType { get; set; }
        public int BookingId { get; set; }
        public string RedirectUrl { get; set; }
    }

    public class MomoIpnDto
    {
        public string? PartnerCode { get; set; }
        public string? OrderId { get; set; }
        public string? RequestId { get; set; }
        public long Amount { get; set; }
        public string? OrderInfo { get; set; }
        public string? OrderType { get; set; }
        public long TransId { get; set; }
        public int ResultCode { get; set; }
        public string? Message { get; set; }
        public string? PayType { get; set; }
        public long ResponseTime { get; set; }
        public string? ExtraData { get; set; }
        public string? Signature { get; set; }
        public string? PaymentOption { get; set; }
    }
}
