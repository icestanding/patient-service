using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodingChallange.Shared.Pagination;

namespace CodingChallange.Shared.Pagination
{
    public static class PagedResultExtensions
    {
        public static Page<T> GetPaged<T, K>(this IQueryable<T> query, int page, int pageSize, Func<T, K> sort)
        {
            var result = new Page<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalRowCount = query.Count()
            };

            var pageCount = (double)result.TotalRowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }
    }
}
