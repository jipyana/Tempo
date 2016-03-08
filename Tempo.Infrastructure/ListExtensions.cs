using System;
using System.Collections.Generic;
using FluentAssertions;

namespace Tempo.Infrastructure
{
    public static class ListExtensions
    {
        private static readonly Random random = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            for (var n = list.Count; n > 1; n--)
            {
                var k = random.Next(n);
                list.SwapElements(n - 1, k);
            }
        }

        public static void SwapElements<T>(this IList<T> list, int indexA, int indexB)
        {
            indexA.Should().BeGreaterOrEqualTo(0);
            indexB.Should().BeGreaterOrEqualTo(0);

            var value = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = value;
        }
    }
}
