using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TekrarProjesi.Business.Operations.User;
using TekrarProjesi.Business.Operations.User.Dtos;
using TekrarProjesi.WebApi.Jwt;
using TekrarProjesi.WebApi.Models;

namespace TekrarProjesi.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register (RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
                //todo actionFilter
            }

            var addUserDto = new AddUserDto
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password,
                BirthDate = request.BirthDate
            };

            var result = await _userService.AddUser(addUserDto);

            if (!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login (LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userDto = new LoginUserDto
            {
                Email = request.Email,
                Password = request.Password
            };

            var result = await _userService.LoginUser(userDto);

            if (!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }
            

            var user = result.Data;

            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var token = JwtHelper.GenerateJwtToken(new JwtDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserType = user.UserType,
                SecretKey = configuration["Jwt:SecretKey"]!,
                Issuer = configuration["Jwt:Issuer"]!,
                Audience = configuration["Jwt:Audience"]!,
                ExpireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"]!)

            });

            var response = new LoginResponse
            {
                Token = token,
                Message = "Giriş Başarıyla tamamlandı"
            };

            return Ok(response);

        }

        [HttpGet("Me")]
        [Authorize]
        public async Task<IActionResult> Test()
        {
            return Ok();
        } 
    } 
}
