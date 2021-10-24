using System;
using System.Collections.Generic;
using SocialMedia.Models.Database.Enums;

namespace SocialMedia.Models.Database
{
    // defines the data that has to be stored in the database
    public class Profile
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string PictureLink { get; set; }
        public GenderEnum Gender { get; set; } 
        public RaceType Race { get; set; }  // very conveniently precisely 8 races in the data
        public DateTimeOffset Birthday { get; set; }  // why is there no date type without time
        public string Home { get; set; }

        public YearType Year { get; set; }
        public StudyProgram StudyProgram { get; set; }

        public IList<RoleEnum> Roles { get; set; }  // can have multiple roles; will be json-serialized

        public string Quote { get; set; }
        public string FavoriteShoe { get; set; }
        public string FavoriteArtist { get; set; }
        public string FavoriteColor { get; set; }  // color type
        public PhoneEnum PhoneType { get; set; }
    }
}