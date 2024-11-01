using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TekrarProjesi.Business.Operations.User;
using TekrarProjesi.Business.Operations.User.Dtos;
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

        [HttpPost]
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
    }
}
