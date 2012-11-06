using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Ebuy
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> Page<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
        {
            return Page<T, IEnumerable<T>>(source, pageIndex, pageSize);
        }

        public static IQueryable<T> Page<T>(this IQueryable<T> source, int pageIndex, int pageSize)
        {
            return Page<T, IQueryable<T>>(source, pageIndex, pageSize);
        }

        private static U Page<T,U>(this U source, int pageIndex, int pageSize)
            where U : IEnumerable<T>
        {
            Contract.Requires(pageIndex >= 0, "Page index cannot be negative");
            Contract.Requires(pageSize >= 0, "Page size cannot be negative");

            int skip = pageIndex * pageSize;

            if (skip > 0)
                source = (U)source.Skip(skip);

            source = (U)source.Take(pageSize);

            return source;
        }
    }
}
