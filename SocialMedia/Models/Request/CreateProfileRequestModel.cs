using System;
using System.Collections.Generic;
using SocialMedia.Models.Database.Enums;

namespace SocialMedia.Models.Request
{
    public class CreateProfileRequestModel
    {
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Gender { get; set; } 
        
        // this is how the input data is formatted for some reason
        public string AmericanIndianOrAlaskaNative { get; set; }
        public string Asian { get; set; }
        public string BlackOrAfricanAmerican { get; set; }
        public string HispanicOrLatino { get; set; }
        public string MiddleEastern { get; set; }
        public string NativeHawaiianOrOtherPacificIslander { get; set; }
        public string White { get; set; }
        public string Other { get; set; }

        public string Birthday { get; set; }
        public string Home { get; set; }

        public string Year { get; set; }
        public string Major { get; set; }
        public string Minor { get; set; }
        public string Modification { get; set; }
        
        public string Role { get; set; }

        public string Quote { get; set; }
        public string FavoriteShoe { get; set; }
        public string FavoriteArtist { get; set; }
        public string FavoriteColor { get; set; }
        public string PhoneType { get; set; }
    }
}