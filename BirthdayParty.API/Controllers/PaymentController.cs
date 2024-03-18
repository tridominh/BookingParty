using BirthdayParty.Models;
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
        public async Task<ActionResult> ChangesBookingStatusAfterPayment(PayByCashDto dto)
        {
            var booking = _bookingService.GetBooking(dto.BookingId);
            if(booking == null)
            {
                return BadRequest(new {});
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
        public async Task<ActionResult> ConfirmPayment(ConfirmPaymentDto dto)
        {
            var booking = _bookingService.GetBooking(dto.BookingId);
            if(booking == null)
            {
                return BadRequest(new {});
            }
            if(booking.BookingStatus == "FullPaying")
            {
                _bookingService.UpdateBookingStatus(dto.BookingId, "Paid");
                var payments = _paymentService.GetAll().Where(p => p.BookingId == booking.BookingId);
                if(payments.Count() == 0) {
                    _paymentService.Add(new Payment{
                        TotalPrice = booking.TotalPrice,
                        PaymentStatus = "Paid", 
                        DepositMoney = 0,
                        BookingId = booking.BookingId
                    });
                }
            }
            else if(booking.BookingStatus == "DepositPaying" || booking.BookingStatus == "Deposit")
            {
                _bookingService.UpdateBookingStatus(dto.BookingId, "Deposit");
                var payments = _paymentService.GetAll().Where(p => p.BookingId == booking.BookingId);
                var price = payments.Sum(p => p.DepositMoney);
                System.Console.WriteLine(price);
                if(price < booking.TotalPrice){
                    _paymentService.Add(new Payment{
                        TotalPrice = booking.TotalPrice,
                        PaymentStatus = "Deposit", 
                        DepositMoney = booking.TotalPrice * 1/4,
                        BookingId = booking.BookingId
                    });

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

