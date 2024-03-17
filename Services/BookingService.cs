using BirthdayParty.Models;
using BirthdayParty.Models.DTOs;
using BirthdayParty.Repository.Interfaces;
using BirthdayParty.Services.Interfaces;

namespace BirthdayParty.Services
{
    public class BookingService : IBookingService
    {
        private readonly IGenericRepository<Booking> _bookingRepository;

        public BookingService(IGenericRepository<Booking> bookingRepository, IServiceRepository serviceRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public List<Booking> GetAllBookings()
        {
            return _bookingRepository.GetAll().ToList();
        }

        public Booking CreateBooking(BookingDTO booking)
        {
            var book = new Booking{
                UserId = booking.UserId,
                RoomId = booking.RoomId,
                BookingDate = DateTime.UtcNow,
                PartyDateTime = booking.PartyDateTime,
                BookingStatus = booking.BookingStatus,
                Feedback = booking.Feedback,
            };
            _bookingRepository.Add(book);
            return book;
        }

        public Booking UpdateBooking(BookingDTO booking)
        {
            Booking existingBooking = _bookingRepository.Get(booking.BookingId.Value);

            existingBooking.UserId = booking.UserId;
            existingBooking.RoomId = booking.RoomId;
            existingBooking.BookingDate = DateTime.UtcNow;
            existingBooking.PartyDateTime = booking.PartyDateTime;
            existingBooking.BookingStatus= booking.BookingStatus;
            existingBooking.Feedback = booking.Feedback;

            _bookingRepository.Update(existingBooking);

            return existingBooking;
        }

        public Booking DeleteBooking(int id)
        {
            Booking booking = _bookingRepository.Delete(id);

            return booking;
        }
    }
}
