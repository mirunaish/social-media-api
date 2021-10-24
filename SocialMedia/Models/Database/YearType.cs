using System;
using SocialMedia.Models.Database.Enums;

namespace SocialMedia.Models.Database
{
    // because ef can't automatically convert '25 in the requestModel to Y25 in the enum
    // conversion must be explicit. easier to do in a class like this and then serialize
    public class YearType
    {
        private YearEnum Data { get; set; }

        public YearType(string year)
        {
            year = year.Remove(0, 1); // remove the ' before the year number
            if (int.Parse(year) < 19) year = "Other";
            else year = "Y" + year;
            
            Enum.TryParse(year, out YearEnum result);
            Data = result;
        }

        public YearType(YearEnum year)
        {
            Data = year;
        }
        
        public static int Serialize(YearType r)
        {
            return (int) r.Data;  // return id of enum
        }

        public static YearType DeSerialize(int n)
        {            
            return new ((YearEnum) n); 
        }

        public override string ToString()
        {
            if (Data == YearEnum.Other) return "Other";
            return "'" + Data.ToString().Remove(0, 1); // remove Y and add '
        }
    }
}