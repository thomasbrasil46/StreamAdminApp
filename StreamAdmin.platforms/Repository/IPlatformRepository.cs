using StreamAdmin.Catalog.Data.ValueObjects;

namespace StreamAdmin.Catalog.Repository
{
    public interface IPlatformRepository
    {
        Task<IEnumerable<PlatformVO>> FindAllPlatforms();
        Task<PlatformVO?> FindById(long id);
        Task<PlatformVO> CreatePlatform(PlatformVO platform);
        Task<PlatformVO> UpdatePlatform(PlatformVO platform);
        Task<bool> DeletePlatform(long id);
    }
}
