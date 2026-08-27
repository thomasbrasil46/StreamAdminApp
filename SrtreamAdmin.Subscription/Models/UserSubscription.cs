using StreamAdmin.Subscription.Models.Base;
using StreamAdmin.Subscription.Models.Enums;
using StreamAdmin.Subscription.Models.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreamAdmin.Subscription.Models;

[Table("stream_user_subscriptions")]
public class UserSubscription : BaseEntity
{
    [Column("sus_id")]
    [Required]
    public long UserId { get; private set; }
    [Column("sus_platform_id")]
    [Required]
    public long PlatformId { get; private set; }
    [Column("sus_plan_id")]
    public long? PlanId { get; private set; }
    [Column("sus_price")]
    [Required]
    public Money Price { get; private set; } = null!;
    [Column("sus_billing_cycle")]
    [Required]
    public BillingCycle BillingCycle { get; private set; }
    [Column("sus_started_at")]
    [Required]
    public DateTime StartedAt { get; private set; }
    [Column("sus_next_billing_date")]
    public DateTime? NextBillingDate { get; private set; }
    [Column("sus_cancelled_at")]
    public DateTime? CancelledAt { get; private set; }
    [Column("sus_status")]
    public SubscriptionStatus Status { get; private set; }
    [Column("sus_notes")]
    public string? Notes { get; private set; }

    private UserSubscription()
    {
    }

    public UserSubscription(
        long userId,
        long platformId,
        long? planId,
        Money price,
        BillingCycle billingCycle,
        DateTime startedAt,
        DateTime? nextBillingDate = null,
        string? notes = null)
    {
        if (userId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(userId), "User id must be positive.");
        }

        if (platformId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(platformId), "Platform id must be positive.");
        }

        if (planId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(planId), "Plan id must be positive when provided.");
        }

        if (startedAt == default)
        {
            throw new ArgumentException("Start date must be valid.", nameof(startedAt));
        }

        UserId = userId;
        PlatformId = platformId;
        PlanId = planId;
        Price = price ?? throw new ArgumentNullException(nameof(price));
        BillingCycle = billingCycle;
        StartedAt = startedAt;
        NextBillingDate = nextBillingDate;
        Notes = notes;
        Status = SubscriptionStatus.Active;
    }

    public void Pause()
    {
        EnsureNotCancelled();
        Status = SubscriptionStatus.Paused;
    }

    public void Activate()
    {
        EnsureNotCancelled();
        Status = SubscriptionStatus.Active;
    }

    public void Cancel(DateTime cancelledAt)
    {
        if (cancelledAt < StartedAt)
        {
            throw new ArgumentOutOfRangeException(
                nameof(cancelledAt),
                "Cancellation date cannot be earlier than the start date.");
        }

        CancelledAt = cancelledAt;
        Status = SubscriptionStatus.Cancelled;
    }

    private void EnsureNotCancelled()
    {
        if (Status == SubscriptionStatus.Cancelled)
        {
            throw new InvalidOperationException("A cancelled subscription cannot change status.");
        }
    }
}
