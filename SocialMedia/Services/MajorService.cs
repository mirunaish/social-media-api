using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SocialMedia.Models.Database;
using SocialMedia.Repository;

namespace SocialMedia.Services
{
    // dependency injection: transient
    public class MajorService
    {
        private MajorRepository _repository;
        private ILogger<MajorService> _logger;

        public MajorService(MajorRepository repository, ILogger<MajorService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Major> Create(string name)
        {
            _logger.LogInformation($"MajorService: Creating Major {name}");
            Major major = new Major {Name = name};
            await _repository.Create(major);
            return major;
        }
        
        public async Task<Major> Get(string name)
        {
            _logger.LogInformation($"MajorService: Getting Major with name '{name}'");
            return await _repository.Get(name);
        }
    }
}