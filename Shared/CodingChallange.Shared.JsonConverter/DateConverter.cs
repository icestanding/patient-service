using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingChallange.Shared.JsonConverter
{
    public class DateConverter : IsoDateTimeConverter
    {
        public DateConverter()
        {
            DateTimeFormat = "yyyy-MM-ddTHH:mm:sszzz";
        }
    }
}
