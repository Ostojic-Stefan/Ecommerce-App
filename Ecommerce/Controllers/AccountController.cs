using Ecommerce.DTOs.User;
using Ecommerce.Models;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using System.Security.Claims;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInMagager;
        private readonly TokenService tokenService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInMagager, TokenService tokenService)
        {
            this.userManager = userManager;
            this.signInMagager = signInMagager;
            this.tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized();

            var result = await signInMagager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (result.Succeeded)
            {
                return new UserDTO
                {
                    DisplayName = user.DisplayName,
                    Token = tokenService.CreateToken(user),
                    Username = user.UserName
                };
            }

            return Unauthorized();
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if (await userManager.Users.AnyAsync(x => x.Email == registerDTO.Email))
            {
                return BadRequest("Email Taken");
            }

            if (await userManager.Users.AnyAsync(x => x.UserName == registerDTO.UserName))
            {
                return BadRequest("Email Taken");
            }

            var user = new AppUser
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                UserName = registerDTO.UserName
            };

            var result = await userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                return new UserDTO
                {
                    DisplayName = user.DisplayName,
                    Token = tokenService.CreateToken(user),
                    Username = user.UserName
                };
            }

            return BadRequest("Problem Registering User");
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var user = await userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

            return new UserDTO
            {
                DisplayName = user.DisplayName,
                Token = tokenService.CreateToken(user),
                Username = user.UserName
            };
        }
    }
}
