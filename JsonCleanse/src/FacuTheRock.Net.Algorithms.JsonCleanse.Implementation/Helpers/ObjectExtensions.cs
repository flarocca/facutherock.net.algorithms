using System;

namespace FacuTheRock.Net.Algorithms.JsonCleanse.Implementation.Helpers
{
    internal static class ObjectExtensions
    {
        public static T ThrowIfNull<T>(this T obj) =>
            obj ?? throw new ArgumentNullException(nameof(obj));
    }
}
