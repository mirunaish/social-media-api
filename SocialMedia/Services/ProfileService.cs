using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SocialMedia.Models.Database;
using SocialMedia.Models.Database.Enums;
using SocialMedia.Models.Exceptions;
using SocialMedia.Models.Request;
using SocialMedia.Models.Response;
using SocialMedia.Repository;

namespace SocialMedia.Services
{
    // dependency injection: transient
    public class ProfileService
    {
        private MajorService _majorService;
        // don't need a StudyProgramService because ef takes care of that for me

        private readonly ProfileRepository _repository;
        private readonly ILogger<ProfileService>_logger;

        public ProfileService(ProfileRepository repository, ILogger<ProfileService> logger, MajorService majorService)
        {
            _repository = repository;
            _logger = logger;
            _majorService = majorService;
        }

        public async Task<ProfileTO> Create(CreateProfileRequestModel requestModel)
        {
            _logger.LogDebug("ProfileService received POST");  // TODO fix logger messages

            // parse enums
            Enum.TryParse(requestModel.Gender, out GenderEnum gender);
            Enum.TryParse(requestModel.PhoneType, out PhoneEnum phoneType);
            
            // parse birthday
            DateTimeOffset.TryParse(requestModel.Birthday, out var birthday);

            // convert roles to array
            var roleStrings = requestModel.Role.Split(", ").Select(s => s.Trim()).ToArray();
            RoleEnum[] roles = new RoleEnum[roleStrings.Length];
            for (var i=0; i<roleStrings.Length; i++)
            {
                Enum.TryParse(roleStrings[i], out roles[i]);
            }

            // get majors and create studyProgram
            string[] majors;
            majors = requestModel.Major.Split(", ").Select(s => s.Trim()).ToArray();
            string[] minors;
            minors = requestModel.Minor.Split(", ").Select(s => s.Trim()).ToArray();
            
            var array = new[] {
                majors.Length>0 ? majors[0] : null,
                majors.Length>1 ? majors[1] : null,  // majors[1] if there is a second major, null otherwise
                minors.Length>0 ? minors[0] : null,
                minors.Length>1 ? minors[1] : null,
                requestModel.Modification};
            majors = null; minors = null; // no need to keep these around in memory 
            
            var final = new List<Major>();

            foreach (var name in array)
            {
                if (!string.IsNullOrEmpty(name))
                {
                    try
                    {
                        // get major object by name
                        final.Add(await _majorService.Get(name));
                    }
                    catch (MajorNotFoundException) // the major does not exist; add it
                    {
                        final.Add(await _majorService.Create(name));
                    }
                }
                else
                {
                    final.Add(null);
                }
            }

            StudyProgram studyProgram = new StudyProgram
            {
                Major1 = final[0],
                Major2 = final[1],
                Minor1 = final[2],
                Minor2 = final[3],
                Modification = final[4]
            };

            // create profile
            Profile entity = new Profile {
                // id will be automatically created
                Name = requestModel.Name,
                PictureLink = requestModel.Picture,
                Gender = gender,
                Race = new RaceType(new[] {
                    requestModel.AmericanIndianOrAlaskaNative,
                    requestModel.Asian,
                    requestModel.BlackOrAfricanAmerican,
                    requestModel.HispanicOrLatino,
                    requestModel.MiddleEastern,
                    requestModel.NativeHawaiianOrOtherPacificIslander,
                    requestModel.White,
                    requestModel.Other
                }),
                Birthday = birthday,
                Home = requestModel.Home,
                Year = new YearType(requestModel.Year),
                StudyProgram = studyProgram,
                Roles = roles,
                Quote = requestModel.Quote,
                FavoriteShoe = requestModel.FavoriteShoe,
                FavoriteArtist = requestModel.FavoriteArtist,
                FavoriteColor = requestModel.FavoriteColor,
                PhoneType = phoneType
            };
            
            await _repository.Create(entity);  // send it to the repository to store in the database
            return ProfileTO.From(entity);  // create a TO from the entity and return it
            // TODO TO does not have correct id
        }
        
        public async Task<IList<ProfileTO>> Read()
        {
            _logger.LogDebug("ProfileService: Reading all profiles");
            IList<Profile> entities = await _repository.Read();
            return entities.Select(p => ProfileTO.From(p)).ToList();
        }
    }
}