using BirthdayParty.Models;
using BirthdayParty.Models.Converters;
using BirthdayParty.Models.DTOs;
using BirthdayParty.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace BirthdayParty.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageService packageService;

        public PackageController(IPackageService packageService) {
            this.packageService = packageService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Package>>> GetAllPackages()
        {
            List<Package> packages = packageService.GetAllPackages();

            //if (packages == null || packages.Count == 0)
            //{
            //    return NotFound();
            //}

            return Ok(packages);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Package>> CreatePackage([FromBody] PackageCreateDto packageCreateDto)
        {
            Package package = PackageConverter.toEntity(packageCreateDto);

            packageService.CreatePackage(package);

            return Ok(new { Message = "Create Package Successfully", Data = packageCreateDto });
        }

        [HttpPut("Update")]
        public async Task<ActionResult<Package>> UpdatePackage([FromBody] PackageUpdateDto packageUpdateDto)
        {
            Package updatedPackage = PackageConverter.toEntity(packageUpdateDto);

            Package package = packageService.UpdatePackage(packageUpdateDto.PackageId, updatedPackage);

            if(package == null)
            {
                return NotFound();   
            }

            return Ok(new { Message = "Update Package Successfully", Data =  package});
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<Package>> DeletePackage([FromBody] int id)
        {
            Package package = packageService.DeletePackage(id);

            if(package == null)
            {
                return NotFound();
            }

            return Ok(new { Message = "Delete Package Successfully", Data = package });
        }

        [HttpGet("GetAllServicesByPackageId")]
        public async Task<ActionResult<List<Service>>> GetAllServicesByPackageId(int id)
        {
            List<Service> services = packageService.GetAllServicesByPackageId(id);

            if(services == null || services.Count == 0)
            {
                return NotFound();
            }
            return Ok(services);
        }
    }
}
