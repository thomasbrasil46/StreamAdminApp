using AutoMapper;
using StreamAdmin.Catalog.Data.ValueObjects;
using StreamAdmin.Catalog.Models.Context;

namespace StreamAdmin.Catalog.Repository
{
    public class PlanRepository : IPlanRepository
    {

        private readonly MySQLContext _context;
        private IMapper _mapper;

        public PlanRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<PlanVO> FindById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PlanVO>> FindAllPlans()
        {
            throw new NotImplementedException();
        }

        public Task<PlanVO> CreatePlan(PlanVO plan)
        {
            throw new NotImplementedException();
        }

        public Task<PlanVO> UpdatePlan(PlanVO plan)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePlan(long id)
        {
            throw new NotImplementedException();
        }
    }
}
