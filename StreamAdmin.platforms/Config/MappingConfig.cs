using AutoMapper;

namespace StreamAdmin.Catalog.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Models.Platform, Data.ValueObjects.PlatformVO>();
                config.CreateMap<Data.ValueObjects.PlatformVO, Models.Platform>();
            });
            return mappingConfig;
        }
    }
}
