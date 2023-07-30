using APP.Api.Errors;
using APP.Core.Dtos;
using APP.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace APP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null) return Unauthorized(new BaseCommonResponse(401));

            var result = await signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (result is null || result.Succeeded==false ) return Unauthorized(new BaseCommonResponse(401));

            return Ok(new UserDto
            {
                DisplayName = user.DisplayName,
                Email = dto.Email,
                Token = ""
            });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = new AppUser
            {
                DisplayName = dto.DisplayName,
                UserName = dto.Email,
                Email = dto.Email
            };
            var result = await userManager.CreateAsync(user,dto.Password);
            if (result.Succeeded == false) return BadRequest(new BaseCommonResponse(400));
            return Ok(new UserDto
            {
                DisplayName=dto.DisplayName,
                Email=dto.Email,
                Token=""
            });
        }
    }
}
