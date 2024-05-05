using System.Linq.Expressions;

namespace SOSRS.Api.Helpers;

public static class LinqExtension
{
    public static IQueryable<T> When<T>(this IQueryable<T> source, bool trigger, Expression<Func<T, bool>> expression)
    {
        if (trigger)
        {
            return source.Where(expression);
        }

        return source;
    }
}
