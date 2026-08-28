using Microsoft.AspNetCore.Mvc;
using StreamAdmin.Subscription.Data.ValueObject;
using StreamAdmin.Subscription.Repository;

namespace StreamAdmin.Subscription.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSubscriptionController : ControllerBase
    {
        private readonly IUserSubscriptionRepository _userSubscriptionRepository;

        public UserSubscriptionController(IUserSubscriptionRepository userSubscriptionRepository)
        {
            _userSubscriptionRepository = userSubscriptionRepository ?? throw new ArgumentNullException(nameof(userSubscriptionRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscriptionVO>>> GetAllSubscriptions()
        {
            var subscriptions = await _userSubscriptionRepository.FindAllSubscriptions();
            return Ok(subscriptions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubscriptionVO>> GetSubscriptionById(long id)
        {
            var subscription = await _userSubscriptionRepository.FindById(id);
            if (subscription == null)
                return NotFound();
            return Ok(subscription);
        }

        [HttpPost]
        public async Task<ActionResult<SubscriptionVO>> CreateSubscription([FromBody] SubscriptionVO subscription)
        {
            if (subscription == null)
                return BadRequest();
            var createdSubscription = await _userSubscriptionRepository.CreateSubscription(subscription);
            return CreatedAtAction(nameof(GetSubscriptionById), new { id = createdSubscription.UserId }, createdSubscription);
        }

        [HttpPut]
        public async Task<ActionResult<SubscriptionVO>> UpdateSubscription(long id, [FromBody] SubscriptionVO subscription)
        {
            if (subscription == null)
                return BadRequest();
            var updatedSubscription = await _userSubscriptionRepository.UpdateSubscription(subscription);
            if (updatedSubscription == null)
                return NotFound();
            return Ok(updatedSubscription);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SubscriptionVO>> DeleteSubscription(long id)
        {
            var subscription = await _userSubscriptionRepository.DeleteSubscription(id);
            if (!subscription) return BadRequest();
            return Ok(subscription);
        }
    }
}               