using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StreamAdmin.Catalog.Data.ValueObjects;
using StreamAdmin.Catalog.Models;
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

        public async Task<PlanVO> FindById(long id)
        {
            StreamingPlan? plan = await _context.StreamingPlans.Where(p => p.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<PlanVO>(plan);
        }

        public async Task<IEnumerable<PlanVO>> FindAllPlans()
        {
            List<StreamingPlan> plans = await _context.StreamingPlans.ToListAsync();
            return _mapper.Map<List<PlanVO>>(plans);
        }

        public async Task<PlanVO> CreatePlan(PlanVO plan)
        {
            StreamingPlan streamingPlan = _mapper.Map<StreamingPlan>(plan);
            _context.StreamingPlans.Add(streamingPlan);
            await _context.SaveChangesAsync();
            return _mapper.Map<PlanVO>(streamingPlan);
        }

        public async Task<PlanVO> UpdatePlan(PlanVO plan)
        {
            StreamingPlan streamingPlan = _mapper.Map<StreamingPlan>(plan);
            _context.StreamingPlans.Update(streamingPlan);
            await _context.SaveChangesAsync();
            return _mapper.Map<PlanVO>(streamingPlan);
        }

        public async Task<bool> DeletePlan(long id)
        {
            try
            {
                StreamingPlan? plan = await _context.StreamingPlans.Where(p => p.Id == id).FirstOrDefaultAsync();
                if (plan == null)
                    return false;
                _context.StreamingPlans.Remove(plan);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
