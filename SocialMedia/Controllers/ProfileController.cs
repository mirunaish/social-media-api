using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialMedia.Models.Request;
using SocialMedia.Models.Response;
using SocialMedia.Services;

namespace SocialMedia.Controllers
{
    [ApiController]
    [Route("[controller]")]  // template: the controller name is the class name without "Controller" 
    public class ProfileController : ControllerBase
    {
        private readonly ProfileService _profileService;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(ProfileService profileService, ILogger<ProfileController> logger)
        {
            _profileService = profileService;
            _logger = logger;
        }
        
        // C
        [HttpPost("")]
        public async Task<ActionResult<ProfileTO>> Create([FromBody] CreateProfileRequestModel requestModel)
        { 
            // Ok returns ActionResult, Task because async
            _logger.LogDebug("ProfileController received POST request");
            var result = await _profileService.Create(requestModel);
            return Ok(result);
        }
        
        [HttpPost("all")]
        public async Task<ActionResult> CreateMultiple([FromBody] CreateProfileRequestModel[] requestModels)
        {
            _logger.LogDebug("ProfileController received POST request");
            foreach (var model in requestModels)
            {
                await _profileService.Create(model);
            }
            return Ok();
        }
        
        // R
        [HttpGet("")]
        public async Task<ActionResult<IList<ProfileTO>>> Read()
        {
            _logger.LogDebug("ProfileController received GET request");
            return Ok(await _profileService.Read());
        }
    }
}