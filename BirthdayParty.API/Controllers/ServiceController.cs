using BirthdayParty.Models;
using BirthdayParty.Models.DTOs;
using BirthdayParty.Services;
using BirthdayParty.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BirthdayParty.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet("GetAllServices")]
        public async Task<ActionResult<List<Service>>> GetAllServices()
        {
            List<Service> services = _serviceService.GetAllServices();

            if (services == null || services.Count == 0)
            {
                return NotFound();
            }

            return Ok(services);
        }
        [HttpPut("UpdateService")]
        public async Task<ActionResult<Service>> UpdateService(ServiceUpdateDto updatedService)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingService = _serviceService.GetServiceById(updatedService.ServiceId);

            if (existingService == null)
            {
                return NotFound();
            }

            try
            {
                var result = _serviceService.UpdateService(updatedService);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update the service.");
            }
        }

        [HttpDelete("DeleteService")]
        public async Task<ActionResult> DeleteService(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }

            var result = _serviceService.DeleteService(id);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("CreateService")]
        public async Task<ActionResult<Service>> CreateService(ServiceCreateDto service)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _serviceService.CreateService(service);
            return Ok();
        }

		[HttpGet("GetServiceById")]
		public async Task<ActionResult<Service>> GetServiceById(int id)
		{
			Service service = _serviceService.GetServiceById(id);

			if (service == null)
			{
				return NotFound();
			}
			return Ok(service);
		}
	}
}
