using Microsoft.AspNetCore.Mvc;
using StreamAdmin.Subscription.Data.ValueObject;
using StreamAdmin.Subscription.Repository;
using StreamAdmin.Subscription.Services;

namespace StreamAdmin.Subscription.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSubscriptionController : ControllerBase
    {
        private readonly IUserSubscriptionRepository _userSubscriptionRepository;
        private readonly IPlatformCatalogClient _platformCatalogClient;

        public UserSubscriptionController(
            IUserSubscriptionRepository userSubscriptionRepository,
            IPlatformCatalogClient platformCatalogClient)
        {
            _userSubscriptionRepository = userSubscriptionRepository ?? throw new ArgumentNullException(nameof(userSubscriptionRepository));
            _platformCatalogClient = platformCatalogClient ?? throw new ArgumentNullException(nameof(platformCatalogClient));
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

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<SubscriptionVO>>> GetSubscriptionsByUserId(long userId)
        {
            if (userId <= 0)
                return BadRequest("User id must be positive.");

            var subscriptions = await _userSubscriptionRepository.FindByUserId(userId);
            return Ok(subscriptions);
        }

        [HttpPost]
        public async Task<ActionResult<SubscriptionVO>> CreateSubscription([FromBody] SubscriptionVO subscription)
        {
            if (subscription == null)
                return BadRequest();

            ActionResult? validationError = await ValidatePlatformAndPlan(subscription);
            if (validationError is not null)
                return validationError;

            var createdSubscription = await _userSubscriptionRepository.CreateSubscription(subscription);
            return CreatedAtAction(nameof(GetSubscriptionById), new { id = createdSubscription.Id }, createdSubscription);
        }

        [HttpPut]
        public async Task<ActionResult<SubscriptionVO>> UpdateSubscription(long id, [FromBody] SubscriptionVO subscription)
        {
            if (subscription == null)
                return BadRequest();

            ActionResult? validationError = await ValidatePlatformAndPlan(subscription);
            if (validationError is not null)
                return validationError;

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

        private async Task<ActionResult?> ValidatePlatformAndPlan(SubscriptionVO subscription)
        {
            PlatformCatalogValidationResult result = await _platformCatalogClient.ValidateAsync(
                subscription.PlatformId,
                subscription.PlanId,
                HttpContext.RequestAborted);

            return result switch
            {
                PlatformCatalogValidationResult.Valid => null,
                PlatformCatalogValidationResult.PlatformNotFound =>
                    BadRequest("The informed platform does not exist."),
                PlatformCatalogValidationResult.PlanNotFound =>
                    BadRequest("The informed plan does not exist."),
                PlatformCatalogValidationResult.PlanDoesNotBelongToPlatform =>
                    BadRequest("The informed plan does not belong to the informed platform."),
                _ => StatusCode(StatusCodes.Status503ServiceUnavailable,
                    "The platform catalog is currently unavailable.")
            };
        }
    }
}
