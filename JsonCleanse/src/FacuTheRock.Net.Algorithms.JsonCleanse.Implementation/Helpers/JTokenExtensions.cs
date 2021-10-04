using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FacuTheRock.Net.Algorithms.JsonCleanse.Implementation.Helpers
{
    internal static class JTokenExtensions
    {
        public static void RemoveFromToken<T>(
            this IEnumerable<T> items,
            Func<T, bool> condition) where T : JToken =>
                items.Where(condition)
                    .ToList()
                    .ForEach(child => child.Remove());
    }
}
