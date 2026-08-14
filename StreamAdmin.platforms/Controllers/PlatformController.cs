using Microsoft.AspNetCore.Mvc;
using StreamAdmin.Catalog.Data.ValueObjects;
using StreamAdmin.Catalog.Repository;

namespace StreamAdmin.Catalog.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private IPlatformRepository _platformRepository;

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
    }
}
