using System;
using System.Collections.Generic;

namespace CodingChallange.Shared.Pagination
{
    public class Page<T> 
    {
        public IList<T> Results { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalRowCount { get; set; }
        public int PageCount { get; set; }

        public Page()
        {
            Results = new List<T>();
        }
    }
}
