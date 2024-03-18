using BirthdayParty.Models;
using BirthdayParty.Models.DTOs;
using BirthdayParty.Repository.Interfaces;
using BirthdayParty.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            return _bookingRepository.GetAll(p => p.Include(p => p.User), 
                    p => p.Include(p => p.BookingServices)).ToList();
        }

        public Booking GetBooking(int id)
        {
            return _bookingRepository.Get(id);
        }

        public Booking CreateBooking(BookingDTO booking)
        {
            var book = new Booking{
                UserId = booking.UserId,
                RoomId = booking.RoomId,
                BookingDate = DateTime.Now,
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

        public Booking UpdateBookingStatus(int id, string status)
        {
            Booking booking = _bookingRepository.Get(id);
            booking.BookingStatus = status;
            _bookingRepository.Update(booking);
            return booking;
        }
         

        public Booking DeleteBooking(int id)
        {
            Booking booking = _bookingRepository.Delete(id);

            return booking;
        }
    }
}
