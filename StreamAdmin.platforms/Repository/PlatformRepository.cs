using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StreamAdmin.Catalog.Data.ValueObjects;
using StreamAdmin.Catalog.Models;
using StreamAdmin.Catalog.Models.Context;

namespace StreamAdmin.Catalog.Repository
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly MySQLContext _context;
        private readonly IMapper _mapper;

        public PlatformRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PlatformVO?> FindById(long id)
        {
            StreamingPlatform? platform = await _context.StreamingPlatforms
                .Include(p => p.Plans)
                .FirstOrDefaultAsync(p => p.Id == id);
            return _mapper.Map<PlatformVO>(platform);
        }

        public async Task<IEnumerable<PlatformVO>> FindAllPlatforms()
        {
            List<StreamingPlatform> platforms = await _context.StreamingPlatforms
                .Include(p => p.Plans)
                .ToListAsync();
            return _mapper.Map<List<PlatformVO>>(platforms);
        }

        public async Task<PlatformVO> CreatePlatform(PlatformVO platform)
        {
            StreamingPlatform streamingPlatform = _mapper.Map<StreamingPlatform>(platform);
            _context.StreamingPlatforms.Add(streamingPlatform);
            await _context.SaveChangesAsync();
            return _mapper.Map<PlatformVO>(streamingPlatform);
        }

        public async Task<PlatformVO> UpdatePlatform(PlatformVO platform)
        {
            StreamingPlatform streamingPlatform = _mapper.Map<StreamingPlatform>(platform);
            _context.StreamingPlatforms.Update(streamingPlatform);
            await _context.SaveChangesAsync();
            return _mapper.Map<PlatformVO>(streamingPlatform);
        }

        public async Task<bool> DeletePlatform(long id)
        {
            try
            {
                StreamingPlatform? platform = await _context.StreamingPlatforms
                .Include(p => p.Plans)
                .FirstOrDefaultAsync(p => p.Id == id);
                if (platform == null)
                    return false;
                _context.StreamingPlatforms.Remove(platform);
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
