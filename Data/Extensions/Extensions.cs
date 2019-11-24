using Domain.Filtering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Extensions
{
    public static class DataExtensions
    {
        public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize, int indexFrom = 0, CancellationToken cancellationToken = default)
        {
            if (indexFrom > pageIndex)
            {
                throw new ArgumentException($"indexFrom: {indexFrom} > pageNumber: {pageIndex}, must indexFrom <= pageNumber");
            }

            var count = await source.CountAsync(cancellationToken).ConfigureAwait(false);
            var items = await source.Skip((pageIndex - indexFrom) * pageSize).Take(pageSize).ToListAsync(cancellationToken).ConfigureAwait(false);

            var pagedList = new PagedList<T>
            {
                PageNumber = pageIndex,
                PageSize = pageSize,
                IndexFrom = indexFrom,
                TotalCount = count,
                Items = items,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };

            return pagedList;
        }
    }
}
