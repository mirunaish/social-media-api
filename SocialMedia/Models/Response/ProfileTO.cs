using System;
using System.Collections.Generic;
using SocialMedia.Models.Database;
using SocialMedia.Models.Database.Enums;

namespace SocialMedia.Models.Response
{
    public class ProfileTO // transport object, returned by api as json
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string PictureLink { get; set; }
        public GenderEnum Gender { get; set; }
        public int Race { get; set; }
        public DateTimeOffset Birthday { get; set; }
        public string Home { get; set; }

        public string Year { get; set; }
        public StudyProgram StudyProgram { get; set; }

        public IList<RoleEnum> Roles { get; set; }

        public string Quote { get; set; }
        public string FavoriteShoe { get; set; }
        public string FavoriteArtist { get; set; }
        public string FavoriteColor { get; set; } // maybe hex code
        public PhoneEnum PhoneType { get; set; }
        
        public static ProfileTO From(Profile profile)
        {
            return new()
            {
                Name = profile.Name,
                PictureLink = profile.PictureLink,
                Gender = profile.Gender,
                Race = profile.Race.getInt(),
                Birthday = profile.Birthday,
                Home = profile.Home,
                Year = profile.Year.ToString(),
                StudyProgram = profile.StudyProgram,
                Roles = profile.Roles,
                Quote = profile.Quote,
                FavoriteShoe = profile.FavoriteShoe,
                FavoriteArtist = profile.FavoriteArtist,
                FavoriteColor = profile.FavoriteColor,
                PhoneType = profile.PhoneType
            };
        }
    }
}