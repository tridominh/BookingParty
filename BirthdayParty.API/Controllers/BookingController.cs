using BirthdayParty.Models;
using BirthdayParty.Models.Converters;
using BirthdayParty.Models.DTOs;
using BirthdayParty.Repository.Interfaces;
using BirthdayParty.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace BirthdayParty.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IServiceService _serviceService;
        private readonly IGenericRepository<BookingService> _bookingServiceService;

        public BookingController(IBookingService bookingService, IServiceService serviceService
                , IGenericRepository<BookingService> bookingServiceService) {
            _bookingService = bookingService;
            _serviceService = serviceService;
            _bookingServiceService = bookingServiceService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Booking>>> GetAll()
        {
            List<Booking> bookings = _bookingService.GetAllBookings();

            //if (packages == null || packages.Count == 0)
            //{
            //    return NotFound();
            //}

            return Ok(bookings);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Booking>> Create([FromBody] BookingDTO bookingDTO)
        {
            var book = _bookingService.CreateBooking(bookingDTO);

            foreach(var serviceId in bookingDTO.ServiceIds)
            {
                var bookingService = new BookingService{
                    BookingId = book.BookingId,
                    ServiceId = serviceId,
                };
                _bookingServiceService.Add(bookingService);
            }

            return Ok(book);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Booking>> UpdateBooking([FromBody] BookingDTO bookingDTO)
        {
            foreach(var serviceId in bookingDTO.ServiceIds)
            {
                var bookingService = new BookingService{
                    BookingId = bookingDTO.BookingId.Value,
                    ServiceId = serviceId,
                };
                _bookingServiceService.Update(bookingService);
            }

            var book = _bookingService.UpdateBooking(bookingDTO);

            return Ok(book);
         
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<Booking>> DeleteBooking([FromBody] int id)
        {
            var bookingServices = _bookingServiceService.GetAll().Where(b => b.BookingId == id);

            foreach(var bookingService in bookingServices)
            {
                _bookingServiceService.Delete(bookingService.BookingServiceId);
            }

            Booking booking = _bookingService.DeleteBooking(id);

            return Ok(booking);
        }

    }
}
