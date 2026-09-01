using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StreamAdmin.Subscription.Data.ValueObject;
using StreamAdmin.Subscription.Models;
using StreamAdmin.Subscription.Models.Context;

namespace StreamAdmin.Subscription.Repository
{
    public class UserSubscriptionRepository : IUserSubscriptionRepository
    {
        private readonly MySQLContext _context;
        private readonly IMapper _mapper;

        public UserSubscriptionRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SubscriptionVO>> FindAllSubscriptions()
        {
            List<UserSubscription> subscriptions = await _context.UserSubscriptions.ToListAsync();
            return _mapper.Map<List<SubscriptionVO>>(subscriptions);
        }
        public async Task<IEnumerable<SubscriptionVO>> FindByUserId(long userId)
        {
            List<UserSubscription> subscriptions = await _context.UserSubscriptions
                .AsNoTracking()
                .Where(subscription => subscription.UserId == userId)
                .OrderBy(subscription => subscription.Id)
                .ToListAsync();
            return _mapper.Map<List<SubscriptionVO>>(subscriptions);
        }
        public async Task<SubscriptionVO?> FindById(long id)
        {
            UserSubscription? subscriptions = await _context.UserSubscriptions.Where(p => p.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<SubscriptionVO>(subscriptions);
        }
        public async Task<SubscriptionVO> CreateSubscription(SubscriptionVO subscription)
        {
            UserSubscription userSubscription = _mapper.Map<UserSubscription>(subscription);
            _context.UserSubscriptions.Add(userSubscription);
            await _context.SaveChangesAsync();
            return _mapper.Map<SubscriptionVO>(userSubscription);
        }
        public async Task<SubscriptionVO> UpdateSubscription(SubscriptionVO subscription)
        {
            UserSubscription userSubscription = _mapper.Map<UserSubscription>(subscription);
            _context.UserSubscriptions.Update(userSubscription);
            await _context.SaveChangesAsync();
            return _mapper.Map<SubscriptionVO>(userSubscription);
        }
        public async Task<bool> DeleteSubscription(long id)
        {
            UserSubscription? userSubscription = await _context.UserSubscriptions.FindAsync(id);
            if (userSubscription == null)
                return false;

            _context.UserSubscriptions.Remove(userSubscription);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
