using BirthdayParty.Models;
using BirthdayParty.Models.DTOs;

namespace BirthdayParty.Services.Interfaces
{
    public interface IRoomService
    {
        List<Room> GetAllRooms();
        Room GetRoomById(int id);
        Room UpdateRoom(RoomUpdateDto updatedRoom);
        Room DeleteRoom(int id);
        Room CreateRoom(RoomCreateDto room);
    }
}
