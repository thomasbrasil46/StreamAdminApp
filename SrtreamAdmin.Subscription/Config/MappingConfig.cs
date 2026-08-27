using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using StreamAdmin.Subscription.Data.ValueObject;
using StreamAdmin.Subscription.Models;

namespace StreamAdmin.Subscription.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<SubscriptionVO, UserSubscription>();
            },
            NullLoggerFactory.Instance);

            return mappingConfig;
        }
    }
}
