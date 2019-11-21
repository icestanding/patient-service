using CodingChallange.Shared.Models.Pagination;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallange.Services.Patient.Sieve
{
    public static class ISieveProcessorExtensions
    {
        public static PagedResult<T> GetPaged<T>(this ISieveProcessor sieveProcessor, IQueryable<T> query, SieveModel sieveModel = null) where T : class
        {
            var result = new PagedResult<T>();

            var (pagedQuery, page, pageSize, rowCount, pageCount) =  GetPagedResultAsync(sieveProcessor, query, sieveModel);

            result.PageNumber = page;
            result.PageSize = pageSize;
            result.TotalNumberOfRecords = rowCount;
            result.TotalNumberOfPages = pageCount;

            result.Results = pagedQuery.ToList();

            return result;
        }

        private static (IQueryable<T> pagedQuery, int page, int pageSize, int rowCount, int pageCount) GetPagedResultAsync<T>(ISieveProcessor sieveProcessor, IQueryable<T> query, SieveModel sieveModel = null) where T : class
        {
            var page = sieveModel?.Page ?? 1;
            var pageSize = sieveModel?.PageSize ?? 50;

            if (sieveModel != null)
            {
                // apply pagination in a later step
                query = sieveProcessor.Apply(sieveModel, query, applyPagination: false);
            }

            var rowCount =  query.Count();

            var pageCount = (int)Math.Ceiling((double)rowCount / pageSize);

            var skip = (page - 1) * pageSize;
            var pagedQuery = query.Skip(skip).Take(pageSize);

            return (pagedQuery, page, pageSize, rowCount, pageCount);
        }
    }
}
