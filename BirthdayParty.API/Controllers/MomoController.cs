using BirthdayParty.Models;
using BirthdayParty.Models.DTOs;
using BirthdayParty.Repository.Interfaces;
using BirthdayParty.Services.Interfaces;
using BirthdayParty.Services.PaymentService.Momo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;


namespace BirthdayParty.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MomoController : ControllerBase
    {
        private readonly MomoService _momoService;
        private readonly MomoConfig _config;
        private readonly IBookingService _bookingService;

        public MomoController(MomoService momoService, IOptions<MomoConfig> config,
                IBookingService bookingService){
            _momoService = momoService;
            _config = config.Value;
            _bookingService = bookingService;
        }

        [HttpPost("CreateMomoLink")]
        public async Task<ActionResult> CreateMomoLink([FromBody] MomoCreateLinkDto dto)
        {
            var booking = _bookingService.GetBooking(dto.BookingId);
            if(booking == null)
            {
                return BadRequest(new {});
            }
            var totalPrice = Convert.ToInt64(booking.TotalPrice);
            MomoOneTimePaymentRequest request = _momoService.CreateRequestModel(totalPrice, dto.Message);
            var result = request.GetLink(_config.PaymentUrl);
            return Ok(new { url= result.Item2 });
        }

        [HttpPost("momo-ipn")]
        public async Task<ActionResult> MomoNotification()
        {
            Console.WriteLine("Hello");
            return NoContent();
        }
    }

    public class MomoCreateLinkDto
    {
        public string Message { get; set;}
        public int BookingId { get; set; }
    }

    public class MomoIpnDto
    {
        public string partnerCode { get; set; }
        public string orderId { get; set; }
        public string requestId { get; set; }
        public long amount { get; set; }
        public string orderInfo { get; set; }
        public string orderType { get; set; }
        public int transId { get; set; }
        public int resultCode { get; set; }
        public string message { get; set; }
        public string payType { get; set; }
        public long responseTime { get; set; }
        public string extraData { get; set; }
        public string signature { get; set; }
    }
}
