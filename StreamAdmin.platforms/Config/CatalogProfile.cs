using AutoMapper;
using StreamAdmin.Catalog.Data.ValueObjects;
using StreamAdmin.Catalog.Models;

namespace StreamAdmin.Catalog.Config
{
    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<StreamingPlatform, PlatformVO>()
                .ReverseMap();

            CreateMap<StreamingPlan, PlanVO>()
                .ReverseMap();
        }
    }
}