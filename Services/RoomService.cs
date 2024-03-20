using BirthdayParty.Models;
using BirthdayParty.Models.DTOs;
using BirthdayParty.Repository.Interfaces;
using BirthdayParty.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BirthdayParty.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public List<Room> GetAllRooms()
        {
            return _roomRepository.GetAll(q => q.Include(p => p.RoomImages)).ToList();
        }

        public Room GetRoomById(int id)
        {
            return _roomRepository.Get(id);
        }

        public Room UpdateRoom(RoomUpdateDto updatedRoom)
        {
            var room = _roomRepository.Get(updatedRoom.RoomId);
            room.RoomNumber = updatedRoom.RoomNumber;
            room.Price = updatedRoom.Price;
            room.Capacity = updatedRoom.Capacity;
            room.RoomStatus = updatedRoom.RoomStatus;
            return _roomRepository.Update(room);
        }

        public Room DeleteRoom(int id)
        {
            return _roomRepository.Delete(id);
        }

        public Room CreateRoom(RoomCreateDto room)
        {
            var roomObj = new Room{
                RoomNumber = room.RoomNumber,
                Price = room.Price,
                Capacity = room.Capacity,
                RoomStatus = room.RoomStatus,
            };
            return _roomRepository.Add(roomObj);
        }


    }
}
