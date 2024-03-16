using BirthdayParty.Models.LocalImages;

namespace BirthdayParty.Services.Interfaces
{
    public interface IRoomImageLocalService
    {
        List<RoomImageLocal> GetAllRoomImages();

        RoomImageLocal GetRoomImage(int id);

        RoomImageLocal CreateRoomImage(RoomImageLocal roomImage);

        RoomImageLocal UpdateRoomImage(RoomImageLocal roomImage);

        RoomImageLocal DeleteRoomImage(int id);
    }
}
