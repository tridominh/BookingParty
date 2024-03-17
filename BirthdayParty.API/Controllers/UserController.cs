using BirthdayParty.Models;
using BirthdayParty.Models.DTOs;
using BirthdayParty.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BirthdayParty.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly SignInManager<User> _signIn;
        private readonly JWTService _jwtService;
        private readonly UserManager<User> _manager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUserService userService;

		public UserController(ILogger<WeatherForecastController> logger, 
                SignInManager<User> signIn, UserManager<User> manager,
                JWTService jwtService, RoleManager<Role> roleManager, 
                IUserService userService)
        {
            _logger = logger;
            _signIn = signIn;
            _manager = manager;
            _jwtService = jwtService;
            _roleManager = roleManager;
			this.userService = userService;
		}

        [HttpPost("Login")]
        public async Task<ActionResult<JwtDTO>> Login(string email, string password)
        {
            var user = await _manager.FindByEmailAsync(email);
            if(user==null) return Unauthorized("Invalid email!!!");
            var result = await _signIn.CheckPasswordSignInAsync(user, password, false);
            if(!result.Succeeded) return Unauthorized("Invalid email or password!!!");
            return CreateUserToken(user);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(string email, string password, string name)
        {
            if(await _manager.FindByEmailAsync(email) != null){
                return BadRequest("Email already exists!!!");
            }
            var user = new User{
                UserName = name,
                Email = email,
                EmailConfirmed = true,
            };
            var result = await _manager.CreateAsync(user, password);
            if(!result.Succeeded) return BadRequest(result.Errors);
            return Ok("Created successfully!!!");
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var list = _manager.Users;
            return Ok(list.ToList());
        }

        [HttpGet("GetAllRoles")]
        public async Task<ActionResult<IEnumerable<IdentityRole>>> GetAllRole()
        {
            var list = _roleManager.Roles;
            return Ok(list.ToList());
        }

        [HttpPost("AddRoles")]
        [Authorize]
        public async Task<ActionResult> GetAllRole(string roleName)
        {
            var result = await _roleManager.CreateAsync(new Role(roleName));
            if(!result.Succeeded) return BadRequest("Bad request!!!");
            return Ok("Created!!!");
        }

        private JwtDTO CreateUserToken(User user)
        {
            var jwt = new JwtDTO{
                Token = _jwtService.CreateJwt(user),
            };
            return jwt;
        }

        [HttpGet("GetUserById")]
		public async Task<ActionResult<User>> GetUserById(int id)
		{
			User user = userService.GetUserById(id);

			if (user == null)
			{
				return NotFound();
			}
			return Ok(user);
		}

		[HttpPut("UpdateUser")]
		public async Task<ActionResult<User>> UpdateUser([FromBody] UserUpdateDTO userUpdateDto)
		{
			User user = userService.UpdateUser(userUpdateDto);

			if (user == null)
			{
				return NotFound();
			}

			return Ok(new { Message = "Update User Successfully", Data = user });
		}

		[HttpDelete("DeleteUser")]
		public async Task<ActionResult<User>> DeleteUser([FromBody] int id)
		{
			User user = userService.DeleteUser(id);

			if (user == null)
			{
				return NotFound();
			}

			return Ok(new { Message = "Delete User Successfully", Data = user });
		}
	}
}