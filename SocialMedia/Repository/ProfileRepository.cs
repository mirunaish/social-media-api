using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SocialMedia.Database;
using SocialMedia.Models.Database;

namespace SocialMedia.Repository
{
    // dependency injection: scoped
    public class ProfileRepository
    {
        private readonly SocialMediaDbContext _context;
        private readonly ILogger<ProfileRepository> _logger;

        public ProfileRepository(SocialMediaDbContext context, ILogger<ProfileRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Create(Profile profile)
        {
            _logger.LogInformation("ProfileRepository: Creating profile for " + profile.Name);
            await _context.Profiles.AddAsync(profile);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Profile>> Read()
        {
            _logger.LogInformation("ProfileRepository: Reading all profiles");
            return await IncludeSubtables(_context.Profiles).ToListAsync();
        }
        
        public async Task<Profile> Get(int id)
        {
            _logger.LogInformation($"ProfileRepository: Getting profile with id {id}");
            return await IncludeSubtables(_context.Profiles).FirstOrDefaultAsync(p => p.Id == id);
        }

        private IQueryable<Profile> IncludeSubtables(IQueryable<Profile> query)
        {
            // the query should know that there's subtables to be queried also
            return query
                .Include(q => q.StudyProgram)
                .Include(q => q.StudyProgram.Major1)
                .Include(q => q.StudyProgram.Major2)
                .Include(q => q.StudyProgram.Minor1)
                .Include(q => q.StudyProgram.Minor2)
                .Include(q => q.StudyProgram.Modification);
        }
    }
}