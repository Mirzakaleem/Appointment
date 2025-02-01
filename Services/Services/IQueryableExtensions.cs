using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public static class IQueryableExtensions
    {
        public static async Task<PagedResult<T>> GetPaged<T>(this IQueryable<T> query,
                                        int page, int pageSize, string sortOn = null, string sortDirection = null) where T : class
        {
            var result = new PagedResult<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = await query.CountAsync(); //query.Count();

            if (!string.IsNullOrEmpty(sortOn))
            {
                if (!string.IsNullOrEmpty(sortDirection) && sortDirection.ToUpper() == "ASC")
                    query = query.OrderBy(sortOn);
                else
                    query = query.OrderByDescending(sortOn);
            }

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = await query.Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        {
            return source.OrderBy(ToLambda<T>(propertyName));
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            return source.OrderByDescending(ToLambda<T>(propertyName));
        }

        private static Expression<Func<T, object>> ToLambda<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, propertyName);
            var propAsObject = Expression.Convert(property, typeof(object));

            return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
        }

        // Custom where filter
        public static IQueryable<T> AddCustomWhere<T>(this IQueryable<T> query, AurAppointmentContext context, Expression<Func<T, bool>> expression)
        {
            return query.Where(expression).AsQueryable();
        }
    }
}
