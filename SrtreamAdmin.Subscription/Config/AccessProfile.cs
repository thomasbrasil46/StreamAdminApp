using AutoMapper;
using StreamAdmin.Subscription.Data.ValueObject;
using StreamAdmin.Subscription.Models;

namespace StreamAdmin.Subscription.Config
{
    public class AccessProfile : Profile
    {
        public AccessProfile()
        {
            CreateMap<UserAccess, AccessVO>()
                .ReverseMap();
        }
    }
}
