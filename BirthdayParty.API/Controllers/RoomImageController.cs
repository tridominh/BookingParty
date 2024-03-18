using BirthdayParty.Models.DTOs;
using BirthdayParty.Models.LocalImages;
using BirthdayParty.Repository;
using BirthdayParty.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirthdayParty.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomImageController : ControllerBase
    {
        private readonly IRoomImageLocalService _roomImageService;

        public RoomImageController(IRoomImageLocalService roomImageService)
        {
            _roomImageService= roomImageService;
        }

        [HttpGet("GetAllRoomImages")]
        public async Task<ActionResult<List<RoomImageLocal>>> GetAllRoomImages()
        {
            var rooms = _roomImageService.GetAllRoomImages();

            if (rooms == null || rooms.Count() == 0)
            {
                return NotFound();
            }

            return Ok(rooms);
        }
        [HttpPut("UpdateRoomImages")]
        public async Task<ActionResult<RoomImageLocal>> UpdateRoomImage([FromForm]RoomImageDto roomImage)
        {
            var existingRoom = _roomImageService.GetRoomImage(roomImage.RoomImageId.Value);

            if (existingRoom == null)
            {
                return NotFound();
            }

            try
            {
                var result = _roomImageService.UpdateRoomImage(existingRoom);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update the service.");
            }
        }

        [HttpDelete("DeleteRoomImage")]
        public async Task<ActionResult> DeleteRoomImage(int id)
        {
            var result = _roomImageService.DeleteRoomImage(id);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("CreateRoomImage")]
        public async Task<ActionResult<RoomImageLocal>> CreateRoomImage([FromForm]RoomImageDto roomImage)
        {
            byte[] file = FileConvertUtils.ConvertToByteArray(roomImage.Image);
            var roomImageObj = new RoomImageLocal{
                RoomId = roomImage.RoomId,
                Image = file,
            };
            _roomImageService.CreateRoomImage(roomImageObj);
            return Ok();
        }
    }
}
