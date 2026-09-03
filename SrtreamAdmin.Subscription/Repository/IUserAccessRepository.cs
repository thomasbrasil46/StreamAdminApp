using StreamAdmin.Subscription.Data.ValueObject;

namespace StreamAdmin.Subscription.Repository
{
    public interface IUserAccessRepository
    {
        Task<AccessVO?> FindById(long id);
        Task<AccessVO> CreateAccess(AccessVO access);
        Task<AccessVO> UpdateAccess(AccessVO access);
        Task<bool> DeleteAccess(long id);
    }
}
