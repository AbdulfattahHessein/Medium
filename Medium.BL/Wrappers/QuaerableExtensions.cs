using System.Linq;
using System.Linq.Expressions;

namespace Medium.BL.Wrappers
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber = 1, int pageSize = 10)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 10 : pageSize;
            int count = source.Count();
            if (count == 0) return PaginatedResult<T>.Success(new List<T>(), count, pageNumber, pageSize);

            //pageNumber = pageNumber < 0 ? 1 : pageNumber; 

            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return PaginatedResult<T>.Success(items, count, pageNumber, pageSize);
        }

        public static IOrderedQueryable<T> OrderByColumn<T>(this IQueryable<T> source, string columnPath)
            => source.OrderByColumnUsing(columnPath, "OrderBy");

        public static IOrderedQueryable<T> OrderByColumnDescending<T>(this IQueryable<T> source, string columnPath)
            => source.OrderByColumnUsing(columnPath, "OrderByDescending");

        public static IOrderedQueryable<T> ThenByColumn<T>(this IOrderedQueryable<T> source, string columnPath)
            => source.OrderByColumnUsing(columnPath, "ThenBy");

        public static IOrderedQueryable<T> ThenByColumnDescending<T>(this IOrderedQueryable<T> source, string columnPath)
            => source.OrderByColumnUsing(columnPath, "ThenByDescending");

        private static IOrderedQueryable<T> OrderByColumnUsing<T>(this IQueryable<T> source, string columnPath, string method)
        {
            var parameter = Expression.Parameter(typeof(T), "item");
            var member = columnPath.Split('.')
                .Aggregate((Expression)parameter, Expression.PropertyOrField);
            var keySelector = Expression.Lambda(member, parameter);
            var methodCall = Expression.Call(typeof(Queryable), method, new[]
                    { parameter.Type, member.Type },
                source.Expression, Expression.Quote(keySelector));

            return (IOrderedQueryable<T>)source.Provider.CreateQuery(methodCall);
        }

        //public static async Task<List<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        //{
        //    if (source == null) throw new ArgumentNullException(nameof(source));
        //    pageNumber = pageNumber <= 0 ? 1 : pageNumber;
        //    pageSize = pageSize == 0 ? 10 : pageSize;
        //    int count = source.Count();
        //    if (count == 0) return new List<T>();

        //    //pageNumber = pageNumber < 0 ? 1 : pageNumber; 

        //    var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        //    return items;
        //}

        //public static async Task<PaginatedResult<T>> FilterStudentPaginatedQuarable<T(this IQueryable<T> source, IFilterable filter)
        //{
        //    if (filter == null) throw new ArgumentNullException();
        //    source.Where
        //}

    }
}
