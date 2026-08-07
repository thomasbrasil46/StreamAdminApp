using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using StreamAdmin.Catalog.Data.ValueObjects;
using StreamAdmin.Catalog.Models;

namespace StreamAdmin.Catalog.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<PlatformVO, StreamingPlatform>();
                config.CreateMap<StreamingPlatform, PlatformVO>();
            },
            NullLoggerFactory.Instance);

            return mappingConfig;
        }
    }
}
