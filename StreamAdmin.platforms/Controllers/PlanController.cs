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

        [HttpPost]
        public async Task<ActionResult<PlanVO>> CreatePlan([FromBody] PlanVO plan)
        {
            if (plan == null)
                return BadRequest();
            var createdPlan = await _planRepository.CreatePlan(plan);
            return Ok(plan);
        }

        [HttpPut]
        public async Task<ActionResult<PlanVO>> UpdatePlan(PlanVO plan)
        {
            if (plan == null)
                return BadRequest();
            var updatedPlan = await _planRepository.UpdatePlan(plan);
            if (updatedPlan == null)
                return NotFound();
            return Ok(updatedPlan);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PlanVO>> DeletePlan(long id)
        {
            var plan = await _planRepository.DeletePlan(id);
            if (!plan) return BadRequest();
            return Ok(plan);
        }
    }
}
