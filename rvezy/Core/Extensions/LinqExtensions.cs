using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace rvezy.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> source, Func<T, string> valueFunc, Func<T, string> textFunc)
        {
            return source.Select(x => new SelectListItem
            {
                Value = valueFunc(x),
                Text = textFunc(x)
            });
        }
    }
}