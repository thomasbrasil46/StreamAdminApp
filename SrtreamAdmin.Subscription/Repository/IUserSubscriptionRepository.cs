using StreamAdmin.Subscription.Data.ValueObject;

namespace StreamAdmin.Subscription.Repository
{
    public interface IUserSubscriptionRepository
    {
        Task<IEnumerable<SubscriptionVO>> FindAllSubscriptions();
        Task<IEnumerable<SubscriptionVO>> FindByUserId(long userId);
        Task<SubscriptionVO?> FindById(long id);
        Task<SubscriptionVO> CreateSubscription(SubscriptionVO subscription);
        Task<SubscriptionVO> UpdateSubscription(SubscriptionVO subscription);
        Task<bool> DeleteSubscription(long id);
    }
}
