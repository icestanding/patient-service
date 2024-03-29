﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CodingChallange.Shared.Models.Pagination
{
    public class PagedResult<T>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalNumberOfPages { get; set; }

        public int TotalNumberOfRecords { get; set; }
        [JsonProperty("patients")]
        public IEnumerable<T> Results { get; set; }
    }
}
