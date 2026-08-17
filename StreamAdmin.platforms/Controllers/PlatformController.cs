using Microsoft.AspNetCore.Mvc;
using StreamAdmin.Catalog.Data.ValueObjects;
using StreamAdmin.Catalog.Repository;

namespace StreamAdmin.Catalog.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformRepository _platformRepository;

        public PlatformController(IPlatformRepository platformRepository)
        {
            _platformRepository = platformRepository ?? throw new 
                ArgumentNullException(nameof(platformRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlatformVO>>> GetAllPlatforms()
        {
            var platforms = await _platformRepository.FindAllPlatforms();
            return Ok(platforms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlatformVO>> GetPlatformById(long id)
        {
            var platform = await _platformRepository.FindById(id);
            if (platform == null)
                return NotFound();
            return Ok(platform);
        }

        [HttpPost]
        public async Task<ActionResult<PlatformVO>> CreatePlatform([FromBody] PlatformVO platform)
        {
            if (platform == null)
                return BadRequest();
            var createdPlatform = await _platformRepository.CreatePlatform(platform);
            return CreatedAtAction(nameof(GetPlatformById), new { id = createdPlatform.Id }, createdPlatform);
        }

        [HttpPut]
        public async Task<ActionResult<PlatformVO>> UpdatePlatform(PlatformVO platform)
        {
            if (platform == null)
                return BadRequest();
            var updatedPlatform = await _platformRepository.UpdatePlatform(platform);
            if (updatedPlatform == null)
                return NotFound();
            return Ok(updatedPlatform);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PlatformVO>> DeletePlatform(long id)
        {
            var platform = await _platformRepository.DeletePlatform(id);
            if (!platform) return BadRequest();
            return Ok(platform);
        }
    }
}
