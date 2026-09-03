using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StreamAdmin.Subscription.Data.ValueObject;
using StreamAdmin.Subscription.Models;
using StreamAdmin.Subscription.Models.Context;

namespace StreamAdmin.Subscription.Repository
{
    public class UserAccessRepository : IUserAccessRepository
    {
        private readonly MySQLContext _context;
        private readonly IMapper _mapper;
        public UserAccessRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AccessVO> FindById(long id)
        {
            UserAccess? access = await _context.UserAccesses.Where(p => p.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<AccessVO>(access);
        }
        public async Task<AccessVO> CreateAccess(AccessVO access)
        {
            UserAccess userAccess = _mapper.Map<UserAccess>(access);
            _context.UserAccesses.Add(userAccess);
            await _context.SaveChangesAsync();
            return _mapper.Map<AccessVO>(userAccess);
        }
        public async Task<AccessVO> UpdateAccess(AccessVO access)
        {
            UserAccess userAccess = _mapper.Map<UserAccess>(access);
            _context.UserAccesses.Update(userAccess);
            await _context.SaveChangesAsync();
            return _mapper.Map<AccessVO>(userAccess);
        }
        public async Task<bool> DeleteAccess(long id)
        {
            UserAccess? access = await _context.UserAccesses.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (access == null)
                return false;
            _context.UserAccesses.Remove(access);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
