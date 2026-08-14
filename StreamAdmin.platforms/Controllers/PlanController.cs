using Microsoft.AspNetCore.Mvc;
using StreamAdmin.Catalog.Data.ValueObjects;
using StreamAdmin.Catalog.Repository;

namespace StreamAdmin.Catalog.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private IPlanRepository _planrRepository;

        public PlanController(IPlanRepository planrRepository)
        {
            _planrRepository = planrRepository ?? throw new
                ArgumentNullException(nameof(planrRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanVO>>> GetAllPlans()
        {
            var plans = await _planrRepository.FindAllPlans();
            return Ok(plans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlanVO>> GetPlanById(long id)
        {
            var plan = await _planrRepository.FindById(id);
            if (plan == null)
                return NotFound();
            return Ok(plan);
        }
    }
}
