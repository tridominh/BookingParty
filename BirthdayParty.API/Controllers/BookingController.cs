using BirthdayParty.Models;
using BirthdayParty.Models.DTOs;
using BirthdayParty.Repository.Interfaces;
using BirthdayParty.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BirthdayParty.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IServiceService _serviceService;
        private readonly IRoomService _roomService;
        private readonly IGenericRepository<BookingService> _bookingServiceService;
        private readonly IGenericRepository<User> _userRepository;

        public BookingController(IBookingService bookingService, IServiceService serviceService
                , IGenericRepository<BookingService> bookingServiceService,
                IGenericRepository<User> userRepository,
                IRoomService roomService) {
            _bookingService = bookingService;
            _serviceService = serviceService;
            _bookingServiceService = bookingServiceService;
            _userRepository = userRepository;
            _roomService = roomService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Booking>>> GetAll()
        {
            List<Booking> bookings = _bookingService.GetAllBookings();
            
            return Ok(bookings);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<List<Booking>>> GetById(int id)
        {
            Booking booking = _bookingService.GetBooking(id);
            //Get all services
            booking.BookingServices = _bookingServiceService
                .GetAll()
                .Where(b => b.BookingId == booking.BookingId).ToList();
            foreach (var b in booking.BookingServices){
                b.Service = _serviceService.GetServiceById(b.ServiceId);
                b.Service.BookingServices = null;
            } 
            //Get user
            booking.User = _userRepository.Get(booking.UserId);
            //Get room
            booking.Room = _roomService.GetRoomById(booking.RoomId);
            return Ok(booking);
        }

        [HttpGet("GetAllByUserId")]
        public async Task<ActionResult<List<Booking>>> GetAllByUserId(int id)
        {
            List<Booking> bookings = _bookingService.GetAllBookings().Where(b => b.UserId == id).ToList();

            return Ok(bookings);
        }

        [HttpGet("GetAllOngoingByUserId")]
        public async Task<ActionResult<List<Booking>>> GetAllOngoingByUserId(int id)
        {
            List<Booking> bookings = _bookingService.GetAllBookings()
                .Where(b => b.UserId == id &&
                        b.BookingStatus == "Accepted" &&
                        b.PartyDateTime > DateTime.Now).ToList();

            return Ok(bookings);
        }

        [HttpGet("GetAllPending")]
        public async Task<ActionResult<List<Booking>>> GetAllPendingBookings()
        {
            List<Booking> bookings = _bookingService.GetAllBookings().Where(b => b.BookingStatus == "Pending").ToList();

            return Ok(bookings);
        }

        [HttpGet("GetAllOngoing")]
        public async Task<ActionResult<List<Booking>>> GetAllOngoingBookings()
        {
            List<Booking> bookings = _bookingService.GetAllBookings()
                .Where(b => (
                       b.BookingStatus == "DepositPaying" || 
                       b.BookingStatus == "FullPaying") && 
                       b.PartyDateTime > DateTime.Now 
                ).ToList();

            return Ok(bookings);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Booking>> Create([FromBody] BookingDTO bookingDTO)
        {
            var book = _bookingService.CreateBooking(bookingDTO);
            var room = _roomService.GetRoomById(bookingDTO.RoomId);
            if(room == null) return NotFound(new {});
            decimal totalPrice = room.Price;
            foreach(var serviceObj in bookingDTO.ServiceIds)
            {
                var service = _serviceService.GetServiceById(serviceObj.ServiceId);
                totalPrice += service.ServicePrice * serviceObj.Amount;
                var bookingService = new BookingService{
                    BookingId = book.BookingId,
                    ServiceId = serviceObj.ServiceId,
                    Amount = serviceObj.Amount
                };
                _bookingServiceService.Add(bookingService);
            }
            book.TotalPrice = totalPrice;

            _bookingService.UpdateBooking(book);

            return Ok(book);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Booking>> UpdateBooking([FromBody] BookingDTO bookingDTO)
        {
            foreach(var service in bookingDTO.ServiceIds)
            {
                var bookingService = new BookingService{
                    BookingId = bookingDTO.BookingId.Value,
                    ServiceId = service.ServiceId,
                    Amount = service.Amount
                };
                _bookingServiceService.Update(bookingService);
            }

            var book = _bookingService.UpdateBooking(bookingDTO);

            return Ok(book);
         
        }

        [HttpPut("UpdateStatus")]
        public async Task<ActionResult<Booking>> UpdateStatus([FromBody] UpdateBookingStatusDTO statusDTO)
        {
            var booking = _bookingService.GetBooking(statusDTO.BookingId);

            var bookingDTO = new BookingDTO{
                BookingId = booking.BookingId,
                UserId = booking.UserId,
                RoomId = booking.RoomId,
                PartyDateTime = booking.PartyDateTime,
                BookingStatus= statusDTO.Status,
                Feedback = booking.Feedback,
            };

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

    public class UpdateBookingStatusDTO
    {
        public int BookingId { get; set; }
        public string Status { get; set; } = null!;
    }
}
