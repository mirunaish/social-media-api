using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SocialMedia.Database;
using SocialMedia.Models.Database;
using SocialMedia.Models.Exceptions;

namespace SocialMedia.Repository
{
    // dependency injection: scoped
    public class MajorRepository
    {
        private SocialMediaDbContext _context;
        private ILogger<MajorRepository> _logger;

        public MajorRepository(SocialMediaDbContext context, ILogger<MajorRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Create(Major major)
        {
            _logger.LogDebug($"MajorRepository: Creating Major with name {major.Name}");
            await _context.Majors.AddAsync(major);
        }

        public async Task<Major> Get(string name)
        {
            _logger.LogDebug($"MajorRepository: Getting Major with name '{name}'");
            Major major = await _context.Majors.FirstOrDefaultAsync(m => m.Name == name);
            if (major == null) throw new MajorNotFoundException();
            return major;
        }
    }
}