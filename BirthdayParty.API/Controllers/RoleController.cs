using BirthdayParty.Models;
using BirthdayParty.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BirthdayParty.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class RoleController : ControllerBase
	{
		private readonly RoleManager<Role> _roleManager;

		public RoleController(RoleManager<Role> roleManager)
		{
			_roleManager = roleManager;
		}

		[HttpGet("GetAllRoles")]
		public async Task<ActionResult<IEnumerable<Role>>> GetAllRoles()
		{
			var roles = await _roleManager.Roles.ToListAsync();
			return Ok(roles);
		}

		[HttpPost("CreateRole")]
		public async Task<ActionResult<Role>> CreateRole(RoleCreateDTO roleCreateDTO)
		{
			var role = new Role(roleCreateDTO.Name);
			var result = await _roleManager.CreateAsync(role);
			if (result.Succeeded)
				return Ok(role);
			else
				return BadRequest(result.Errors);
		}

		[HttpPut("UpdateRole")]
		public async Task<ActionResult<Role>> UpdateRole(int id, RoleUpdateDTO roleUpdateDTO)
		{
			var role = await _roleManager.FindByIdAsync(id.ToString());
			if (role == null)
				return NotFound();

			role.Name = roleUpdateDTO.Name;
			var result = await _roleManager.UpdateAsync(role);
			if (result.Succeeded)
				return Ok(role);
			else
				return BadRequest(result.Errors);
		}

		[HttpDelete("DeleteRole")]
		public async Task<ActionResult<Role>> DeleteRole(int id)
		{
			var role = await _roleManager.FindByIdAsync(id.ToString());
			if (role == null)
				return NotFound();

			var result = await _roleManager.DeleteAsync(role);
			if (result.Succeeded)
				return Ok(role);
			else
				return BadRequest(result.Errors);
		}
	}
}
