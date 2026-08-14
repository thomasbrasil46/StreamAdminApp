using Microsoft.AspNetCore.Mvc;
using StreamAdmin.Catalog.Data.ValueObjects;
using StreamAdmin.Catalog.Repository;

namespace StreamAdmin.Catalog.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private IPlanRepository _planRepository;

        public PlanController(IPlanRepository planRepository)
        {
            _planRepository = planRepository ?? throw new
                ArgumentNullException(nameof(planRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanVO>>> GetAllPlans()
        {
            var plans = await _planRepository.FindAllPlans();
            return Ok(plans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlanVO>> GetPlanById(long id)
        {
            var plan = await _planRepository.FindById(id);
            if (plan == null)
                return NotFound();
            return Ok(plan);
        }
    }
}
