using BirthdayParty.Models.LocalImages;
using BirthdayParty.Repository.Interfaces;
using BirthdayParty.Services.Interfaces;

namespace BirthdayParty.Services.LocalImages
{
    public class RoomImageLocalService : IRoomImageLocalService
    {
        private readonly IGenericRepository<RoomImageLocal> _roomImageRepository;

        public RoomImageLocalService(IGenericRepository<RoomImageLocal> roomImageRepository)
        {
            _roomImageRepository = roomImageRepository;
        }

        public List<RoomImageLocal> GetAllRoomImages()
        {
            return _roomImageRepository.GetAll().ToList();
        }

        public RoomImageLocal GetRoomImage(int id)
        {
            return _roomImageRepository.Get(id);
        }

        public RoomImageLocal UpdateRoomImage(RoomImageLocal updatedImage)
        {
            return _roomImageRepository.Update(updatedImage);
        }

        public RoomImageLocal DeleteRoomImage(int id)
        {
            return _roomImageRepository.Delete(id);
        }

        public RoomImageLocal CreateRoomImage(RoomImageLocal image)
        {
            return _roomImageRepository.Add(image);
        }

    }
}
