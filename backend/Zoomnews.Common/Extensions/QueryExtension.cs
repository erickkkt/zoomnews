using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zoomnews.Common.Extensions
{
    public static class QueryExtension
    {
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string sortField, string sortDirection = "asc")
        {
            IOrderedQueryable<T> orderedQuery = null;

            var isAscending = sortDirection.ToLower().Equals("asc");
            var pi = typeof(T).GetProperty(sortField);
            if (pi != null)
            {
                orderedQuery = isAscending
                  ? query.OrderBy(a => pi.GetValue(a, null))
                    : query.OrderByDescending(a => pi.GetValue(a, null));
            }

            return orderedQuery;
        }
    }
}
