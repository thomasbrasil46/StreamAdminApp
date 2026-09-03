using Microsoft.AspNetCore.Mvc;
using StreamAdmin.Subscription.Data.ValueObject;
using StreamAdmin.Subscription.Repository;

namespace StreamAdmin.Subscription.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccessController : ControllerBase
    {
        private readonly IUserAccessRepository _userAccessRepository;

        public UserAccessController(IUserAccessRepository userAccessRepository)
        {
            _userAccessRepository = userAccessRepository ?? throw new ArgumentNullException(nameof(userAccessRepository));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccessVO>> FindById(long id)
        {
            var access = await _userAccessRepository.FindById(id);
            if (access == null)
                return NotFound();
            return Ok(access);
        }

        [HttpPost]
        public async Task<ActionResult<AccessVO>> CreateAccess([FromBody] AccessVO access)
        {
            if (access == null)
                return BadRequest();

            var createdAccess = await _userAccessRepository.CreateAccess(access);
            return CreatedAtAction(nameof(FindById), new { id = createdAccess.Id }, createdAccess);
        }

        [HttpPut]
        public async Task<ActionResult<AccessVO>> UpdateSubscription(long id, [FromBody] AccessVO access)
        {
            if (access == null)
                return BadRequest();

            var updatedAccess = await _userAccessRepository.UpdateAccess(access);
            if (updatedAccess == null)
                return NotFound();
            return Ok(updatedAccess);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAccess(long id)
        {
            var deleted = await _userAccessRepository.DeleteAccess(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}            
