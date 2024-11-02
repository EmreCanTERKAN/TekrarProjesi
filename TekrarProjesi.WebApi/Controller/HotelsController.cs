using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TekrarProjesi.Business.Operations.Hotel;
using TekrarProjesi.Business.Operations.Hotel.Dtos;
using TekrarProjesi.WebApi.Models;

namespace TekrarProjesi.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpPost]

        public async Task<IActionResult> AddHotel(AddHotelRequest request)
        {
            var addHotelDto = new AddHotelDto
            {
                Name = request.Name,
                Stars = request.Stars,
                Location = request.Location,
                AccomodationType = request.AccomodationType,
                FeatureId = request.FeatureId,
            };

            var result = await _hotelService.AddHotel(addHotelDto);

            if (!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }
            return Ok();
        }
    }
}
