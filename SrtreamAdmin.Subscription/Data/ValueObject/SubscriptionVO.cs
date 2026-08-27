using StreamAdmin.Subscription.Models.Enums;
using StreamAdmin.Subscription.Models.ValueObjects;

namespace StreamAdmin.Subscription.Data.ValueObject
{
    public class SubscriptionVO
    {
        public long UserId { get; private set; }                
        public long PlatformId { get; private set; }        
        public long? PlanId { get; private set; }                
        public Money Price { get; private set; } = null!;                
        public BillingCycle BillingCycle { get; private set; }                
        public DateTime StartedAt { get; private set; }        
        public DateTime? NextBillingDate { get; private set; }        
        public DateTime? CancelledAt { get; private set; }        
        public SubscriptionStatus Status { get; private set; }        
        public string? Notes { get; private set; }
    }
}
