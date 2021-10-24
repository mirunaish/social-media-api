using System;
using System.Collections.Generic;
using SocialMedia.Models.Database.Enums;

namespace SocialMedia.Models.Database
{
    public class RaceType
    {
        private byte Data { get; set; } // built based on the dictionary

        public RaceType(byte data)
        {
            Data = data;
        }

        public RaceType(IList<string> races)
        {
            // order of races: americanIndianOrAlaskaNative, asian, blackOrAfricanAmerican, hispanicOrLatino,
            // middleEastern, nativeHawaiianOrOtherPacificIslander, white, other

            Data = Byte.MinValue;
            for (var i = 0; i < races.Count; i++)
            {
                setRace((RaceEnum)i, !string.IsNullOrEmpty(races[i]));
            }
            
        }

        public void setRace(RaceEnum race, bool value)
        {
            // race is just a number between 0 and 7, which corresponds to a bit in the byte
            
            var mask = (byte) (1 << (int) race); // build mask

            // if a bit in | mask is 1, the bit in data becomes 1 as well, otherwise unchanged
            // if a bit in & ~mask is 0, the bit in data becomes 0 as well, otherwise unchanged
            Data = (byte) (value ? Data | mask : Data & ~mask);
        }
        
        public static int Serialize(RaceType r)
        {
            return r.Data;
        }

        public static RaceType DeSerialize(int n)
        {
            return new ((byte)n);
        }

        public int getInt()
        {
            return Data;
        }

    }
}