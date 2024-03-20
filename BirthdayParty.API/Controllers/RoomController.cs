using BirthdayParty.Models;
using BirthdayParty.Models.DTOs;
using BirthdayParty.Models.LocalImages;
using BirthdayParty.Repository;
using BirthdayParty.Services.Interfaces;
using BirthdayParty.Services.LocalImages;
using Microsoft.AspNetCore.Mvc;

namespace BirthdayParty.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IRoomImageLocalService _roomImageService;

        public RoomController(IRoomService roomService, IRoomImageLocalService roomImageLocalService)
        {
            _roomService = roomService;
            _roomImageService = roomImageLocalService;

        }

        [HttpGet("GetAllRooms")]
        public async Task<ActionResult<List<Room>>> GetAllRooms()
        {
            List<Room> rooms = _roomService.GetAllRooms();

            //if (rooms == null || rooms.Count == 0)
            //{
            //    return NotFound();
            //}

            return Ok(rooms);
        }

        [HttpGet("GetRoomById")]
        public async Task<ActionResult<Room>> GetRoomById(int id)
        {
            var result = _roomService.GetRoomById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPut("UpdateRoom")]
        public async Task<ActionResult<Room>> UpdateRoom([FromForm] RoomUpdateDto updatedRoom, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingRoom = _roomService.GetRoomById(updatedRoom.RoomId);

            if (existingRoom == null)
            {
                return NotFound();
            }

            try
            {
                var updatedRoomResult = _roomService.UpdateRoom(updatedRoom);

                if (image != null)
                {
                    byte[] file = FileConvertUtils.ConvertToByteArray(image);
                    var roomImageObj = _roomImageService.GetAllRoomImages()
                        .Where(i => i.RoomId == updatedRoom.RoomId).FirstOrDefault();
                    roomImageObj.Image = file;
                    _roomImageService.UpdateRoomImage(roomImageObj);
                }

                return Ok(updatedRoomResult);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update the service.");
            }
        }


        [HttpDelete("DeleteRoom")]
        public async Task<ActionResult> DeleteRoom(int id)
        {
            var result = _roomService.DeleteRoom(id);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("CreateRoom")]
        public async Task<ActionResult<Room>> CreateRoom([FromForm]RoomCreateDto room, IFormFile Image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingRoom = _roomService.GetAllRooms().Where(r => r.RoomNumber == room.RoomNumber).FirstOrDefault();
            if (existingRoom != null)
            {
                return Conflict("RoomNumber already exists");
            }

            try
            {
                var room1 = _roomService.CreateRoom(room);
                byte[] file = FileConvertUtils.ConvertToByteArray(Image);
                var roomImageObj = new RoomImageLocal
                {
                    RoomId = room1.RoomId,
                    Image = file,
                };
                _roomImageService.CreateRoomImage(roomImageObj);
                return Ok(new { });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
