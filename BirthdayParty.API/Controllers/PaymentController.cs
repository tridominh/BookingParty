using BirthdayParty.Models;
using BirthdayParty.Models.DTOs;
using BirthdayParty.Repository.Interfaces;
using BirthdayParty.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace BirthdayParty.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IGenericRepository<Payment> _paymentService;
        private readonly IBookingService _bookingService;

        public PaymentController(IGenericRepository<Payment> paymentService,
            IBookingService bookingService) {
            _paymentService = paymentService;
            _bookingService = bookingService;
        }

        [HttpPut("ChangeBookingStatusAfterPayment")]
        //User request
        public async Task<ActionResult> ChangesBookingStatusAfterPayment(PayByCashDto dto)
        {
            var booking = _bookingService.GetBooking(dto.BookingId);
            if(booking == null)
            {
                return BadRequest(new {});
            }
            //Already paid
            if(booking.BookingStatus == "Paid"){
                return Ok(new {});
            }
            //Already paid but wrong status
            var payments = _paymentService.GetAll().Where(p => p.BookingId == booking.BookingId);
            if(payments.Sum(p => p.DepositMoney) >= booking.TotalPrice ||
              payments.Any(p => p.DepositMoney == 0))
            {
                _bookingService.UpdateBookingStatus(dto.BookingId, "Paid");
                return Ok(new {});
            }
            if(dto.Method == "fullprice")
            {
                _bookingService.UpdateBookingStatus(dto.BookingId, "FullPaying");
                
            }
            else if(dto.Method == "deposit")
            {
                _bookingService.UpdateBookingStatus(dto.BookingId, "DepositPaying");
            }
            return Ok(new {});
        }

        [HttpPut("ConfirmPayment")]
        //Host response
        public async Task<ActionResult> ConfirmPayment(ConfirmPaymentDto dto)
        {
            var booking = _bookingService.GetBooking(dto.BookingId);
            if(booking == null)
            {
                return BadRequest(new {});
            }
            if(booking.BookingStatus == "FullPaying")
            {
                var payments = _paymentService.GetAll().Where(p => p.BookingId == booking.BookingId);
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
                _bookingService.UpdateBookingStatus(dto.BookingId, "Paid");
            }
            else if(booking.BookingStatus == "DepositPaying" || booking.BookingStatus == "Deposit")
            {
                var payments = _paymentService.GetAll().Where(p => p.BookingId == booking.BookingId);
                var price = payments.Sum(p => p.DepositMoney);
                if(price < booking.TotalPrice){
                    _paymentService.Add(new Payment{
                        TotalPrice = booking.TotalPrice,
                        PaymentStatus = "Deposit", 
                        DepositMoney = booking.TotalPrice * 1/4,
                        BookingId = booking.BookingId
                    });
                    _bookingService.UpdateBookingStatus(dto.BookingId, "Deposit");
                }
                else {
                    _bookingService.UpdateBookingStatus(dto.BookingId, "Paid");
                }
            }
            
            return Ok(new {});
        }
    }

    public class PayByCashDto
    {
        public string Method { get; set; }
        public int BookingId { get; set; }
    }

    public class ConfirmPaymentDto
    {
        public int BookingId { get; set; }
    }
}


