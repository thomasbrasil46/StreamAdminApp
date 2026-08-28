using StreamAdmin.Subscription.Models.Enums;
using StreamAdmin.Subscription.Models.ValueObjects;

namespace StreamAdmin.Subscription.Data.ValueObject
{
    public class SubscriptionVO
    {
        public long UserId { get; set; }                
        public long PlatformId { get; set; }        
        public long? PlanId { get; set; }                
        public Money Price { get; set; } = null!;                
        public BillingCycle BillingCycle { get; set; }                
        public DateTime StartedAt { get; set; }        
        public DateTime? NextBillingDate { get; set; }        
        public DateTime? CancelledAt { get; set; }        
        public SubscriptionStatus Status { get; set; }        
        public string? Notes { get; set; }
    }
}
