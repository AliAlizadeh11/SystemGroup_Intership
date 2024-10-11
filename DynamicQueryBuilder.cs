using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FinalProject.DynamicQuery
{
    public class DynamicQueryBuilder
    {
        public static IQueryable<T> ApplyQueryParameters<T>(IQueryable<T> query, QueryParameters parameters)
        {
            // Apply filters
            foreach (var filter in parameters.Filters)
            {
                query = ApplyFilter(query, filter);
            }

            // Apply sorting
            foreach (var sortOption in parameters.SortOptions)
            {
                query = ApplySorting(query, sortOption);
            }

            // Select columns
            if (parameters.Columns != null && parameters.Columns.Any())
            {
                query = SelectColumns(query, parameters.Columns);
            }

            return query;
        }

        private static IQueryable<T> ApplyFilter<T>(IQueryable<T> query, Filter filter)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var member = Expression.Property(parameter, filter.PropertyName);
            var constant = Expression.Constant(filter.Value);
            Expression body = filter.Operation switch
            {
                "Equals" => Expression.Equal(member, constant),
                "Contains" => Expression.Call(member, "Contains", null, constant),
                "GreaterThan" => Expression.GreaterThan(member, constant),
                _ => throw new NotSupportedException($"Operation {filter.Operation} is not supported")
            };

            var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
            return query.Where(lambda);
        }

        private static IQueryable<T> ApplySorting<T>(IQueryable<T> query, SortOption sortOption)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var member = Expression.Property(parameter, sortOption.PropertyName);
            var lambda = Expression.Lambda(member, parameter);

            var methodName = sortOption.IsDescending ? "OrderByDescending" : "OrderBy";
            var resultExpression = Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(T), member.Type },
                query.Expression, Expression.Quote(lambda));

            return query.Provider.CreateQuery<T>(resultExpression);
        }

        private static IQueryable<T> SelectColumns<T>(IQueryable<T> query, string[] columns)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var bindings = columns.Select(column => Expression.Bind(
                typeof(T).GetProperty(column),
                Expression.Property(parameter, column)
            ));

            var body = Expression.MemberInit(Expression.New(typeof(T)), bindings);
            var selector = Expression.Lambda<Func<T, T>>(body, parameter);

            return query.Select(selector);
        }
    }
}