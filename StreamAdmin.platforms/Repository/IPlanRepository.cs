using StreamAdmin.Catalog.Data.ValueObjects;

namespace StreamAdmin.Catalog.Repository
{
    public interface IPlanRepository
    {
        Task<IEnumerable<PlanVO>> FindAllPlans();
        Task<PlanVO> FindById(long id);
        Task<PlanVO> CreatePlan(PlanVO plan);
        Task<PlanVO> UpdatePlan(PlanVO plan);
        Task<bool> DeletePlan(long id);
    }
}
