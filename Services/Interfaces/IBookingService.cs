using BirthdayParty.Models;
using BirthdayParty.Models.DTOs;

namespace BirthdayParty.Services.Interfaces
{
    public interface IBookingService
    {
        List<Booking> GetAllBookings();

        Booking GetBooking (int id);

        Booking CreateBooking (BookingDTO booking);

        Booking UpdateBooking(BookingDTO booking);

        Booking UpdateBooking(Booking booking);

        Booking UpdateBookingStatus(int id, string status);

        Booking DeleteBooking(int id);
    }
}
