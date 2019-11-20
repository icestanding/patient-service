using Newtonsoft.Json.Converters;
using System;

namespace CodingChallange.Shared.JsonConverter
{
    public class DateOfBirthConverter : IsoDateTimeConverter
    {
        public DateOfBirthConverter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
