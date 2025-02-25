using DataAccess.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.ApplicationUserDTO;
using Model.Entity;
using Model.Mapper;
using Service.Interface;

namespace SocialMedia.Server.Controllers
{
    [Route("api/ApplicationUser")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationUserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("Resgister")]
        public async Task<IActionResult> Register([FromBody] RegisterApplicationUserDto registerApplicationUserDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var newApplicationUser = new ApplicationUser { UserName = registerApplicationUserDto.UserName, Email = registerApplicationUserDto.Email };
                var result = await _userManager.CreateAsync(newApplicationUser, registerApplicationUserDto.Password);
                if (!result.Succeeded)
                {
                    return StatusCode(500, result.Errors);
                }

                return Ok(new ApplicationUserDto
                {
                    UserName = registerApplicationUserDto.UserName,
                    Email = registerApplicationUserDto.Email,
                    Token = _tokenService.CreateToken(newApplicationUser)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginApplicationUserDto loginApplicationUserDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = await _userManager.FindByEmailAsync(loginApplicationUserDto.Email);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                var result = await _signInManager.CheckPasswordSignInAsync(user, loginApplicationUserDto.Password, false);
                if (!result.Succeeded)
                {
                    return Unauthorized("UserName not Found or/and password is invalid");
                }
                return Ok(new ApplicationUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{applicationUserId:int}")]
        public async Task<IActionResult> GetById([FromRoute] int applicationUserId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //var user = await _userManager.FindByIdAsync(applicationUserId.ToString());
                var user = await _unitOfWork.GetApplicationUser(applicationUserId);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                return Ok(user.ToApplicationUserDto());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{applicationUserId:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int applicationUserId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var userFromUserManager = await _userManager.GetUserAsync(User);
                if (userFromUserManager == null)
                {
                    return BadRequest("User not found");
                }
                if (userFromUserManager.Id != applicationUserId)
                {
                    return Unauthorized("You are not authorized to delete this user");
                }
                var user = await _unitOfWork.DeleteApplicationUserAsync(applicationUserId);
                await _signInManager.SignOutAsync();
                await _userManager.DeleteAsync(userFromUserManager);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                await _unitOfWork.SaveAsync();
                return Ok(user.ToApplicationUserDto());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
