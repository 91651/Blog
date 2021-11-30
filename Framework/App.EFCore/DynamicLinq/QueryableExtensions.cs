using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace App.EFCore.DynamicLinq
{
    public static class QueryableExtensions
    {
        public static DataSourceResult ToDataSourceResult<T>(this IQueryable<T> queryable, int pageIndex, int pageSize, IEnumerable<Sort> sorts, IEnumerable<Filter> filters)
        {
            var result = new DataSourceResult();
            if (filters != null && filters.Any())
            {
                var filterStr = string.Join(",", filters?.Select((filter, index) => $"{filter.Field}==@{index}"));
                var args = filters?.Select(filter => filter.Value);
                queryable = queryable.Where(filterStr, args);
            }
            if (sorts != null && sorts.Any())
            {
                var sortStr = string.Join(",", sorts?.Select(sort => $"{sort.Field} " + (sort.Desc ? "descending " : " ")));
                queryable = queryable.OrderBy(sortStr);
            }
            result.Total = queryable.Count();
            if (pageIndex > 0)
            {
                queryable = queryable.Skip(pageSize * (pageIndex - 1));
            }
            if (pageSize > 0)
            {
                queryable = queryable.Take(pageSize);
            }
            result.Data = queryable.ToList();
            return result;
        }
        public static DataSourceResult ToDataSourceResult<T>(this IQueryable<T> queryable, Query query)
        {
            return queryable.ToDataSourceResult(query.PageIndex, query.PageSize, query.Sort, query.Filter);
        }

        public static async Task<DataSourceResult> ToDataSourceResultAsync<T>(this IQueryable<T> queryable, int pageIndex, int pageSize, IEnumerable<Sort> sorts, IEnumerable<Filter> filters)
        {
            var result = new DataSourceResult();
            if (filters != null && filters.Any())
            {
                var filterStr = string.Join(",", filters?.Select((filter, index) => $"{filter.Field}==@{index}"));
                var args = filters?.Select(filter => filter.Value);
                queryable = queryable.Where(filterStr, args);
            }
            if (sorts != null && sorts.Any())
            {
                var sortStr = string.Join(",", sorts?.Select(sort => $"{sort.Field} " + (sort.Desc ? "descending " : " ")));
                queryable = queryable.OrderBy(sortStr);
            }
            result.Total = queryable.Count();
            if (pageIndex > 0)
            {
                queryable = queryable.Skip(pageSize * (pageIndex - 1));
            }
            if (pageSize > 0)
            {
                queryable = queryable.Take(pageSize);
            }
            result.Data = await queryable.ToListAsync();
            return result;
        }
        public static async Task<DataSourceResult> ToDataSourceResultAsync<T>(this IQueryable<T> queryable, Query query)
        {
            return await queryable.ToDataSourceResultAsync(query.PageIndex, query.PageSize, query.Sort, query.Filter);
        }
    }
}
