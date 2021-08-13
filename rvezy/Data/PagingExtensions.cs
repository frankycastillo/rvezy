using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using rvezy.Models;

namespace rvezy.Data
{
    public static class PagingExtensions
    {
        //used by LINQ to SQL
        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

        //used by LINQ to SQL
        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, IPaginator paginator)
        {
            return source.Skip((paginator.Page - 1) * paginator.Size).Take(paginator.Size);
        }

        //used by LINQ
        public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public static IEnumerable<TSource> Page<TSource>(this IOrderedEnumerable<TSource> source, int page,
            int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> source, IPaginator paginator)
        {
            return source.Skip((paginator.Page - 1) * paginator.Size).Take(paginator.Size);
        }
    }
}
