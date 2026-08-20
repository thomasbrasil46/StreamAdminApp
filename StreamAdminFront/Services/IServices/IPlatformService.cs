using StreamAdminFront.Models;

namespace StreamAdminFront.Services.IServices
{
    public interface IPlatformService
    {
        Task<IEnumerable<PlatformModel>> FindAllPlatforms();
        Task<PlatformModel?> FindById(long id);
        Task<PlatformModel> CreatePlatform(PlatformModel platform);
        Task<PlatformModel> UpdatePlatform(PlatformModel platform);
        Task<bool> DeletePlatform(long id);
    }
}
