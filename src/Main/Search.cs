using System;
using System.Collections.Generic;
using System.Linq;

namespace Stepic.Algorithms
{
    public static class Search
    {
        public static int Binary<T>(T[] a, T key) where T:IComparable
        {
            var l = 0;
            var r = a.Length - 1;

            while (l <= r)
            {
                var m = l + (r - l)/2;
                if (a[m].CompareTo(key) == 0)
                {
                    return m + 1;
                }
                else if (a[m].CompareTo(key) < 0)
                {
                    l = m + 1;
                }
                else
                {
                    r = m - 1;
                }
            }

            return -1;
        }
    }
}