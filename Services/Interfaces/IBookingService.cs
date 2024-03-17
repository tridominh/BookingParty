using BirthdayParty.Models;
using BirthdayParty.Models.DTOs;

namespace BirthdayParty.Services.Interfaces
{
    public interface IBookingService
    {
        List<Booking> GetAllBookings();

        Booking CreateBooking (BookingDTO booking);

        Booking UpdateBooking(BookingDTO booking);

        Booking DeleteBooking(int id);
    }
}
