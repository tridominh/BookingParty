using BirthdayParty.Models;
using BirthdayParty.Models.DTOs;
using BirthdayParty.Models.ModelScaffold;
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
            _serviceService.CreateService(service);
            return Ok();
        }
    }
}
