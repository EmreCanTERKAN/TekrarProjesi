using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TekrarProjesi.Business.Operations.Feature;
using TekrarProjesi.Business.Operations.Feature.Dtos;
using TekrarProjesi.WebApi.Models;

namespace TekrarProjesi.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeatureService _featureService;
        public FeaturesController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [HttpPost("Add/Feature")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddFeature(FeatureRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var featureDto = new AddFeatureDto
            {
                Title = request.Title,
            };
            
           var result = await _featureService.AddFeature(featureDto);

            if (!result.IsSucceed)
            {
                return BadRequest(result);
            }
            return Ok(result.Message);


        }
    }
}
