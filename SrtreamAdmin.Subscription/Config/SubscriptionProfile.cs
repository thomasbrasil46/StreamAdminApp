using AutoMapper;
using StreamAdmin.Subscription.Data.ValueObject;
using StreamAdmin.Subscription.Models;

namespace StreamAdmin.Subscription.Config
{
    public class SubscriptionProfile : Profile
    {
        public SubscriptionProfile()
        {
            CreateMap<UserSubscription, SubscriptionVO>()
                .ReverseMap();
        }
    }
}
