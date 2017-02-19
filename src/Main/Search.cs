using System;
using System.Collections.Generic;
using System.Linq;

namespace Stepic.Algorithms
{
    public static class Search
    {
        public static int Binary<T>(T[] a, T key) where T:IComparable<T>
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

        public static int LessCount<T>(T[] a, T key, bool countEqual = false) where T:IComparable<T>
        {
            var l = 0;
            var r = a.Length - 1;

            while (l <= r)
            {
                var m = l + (r - l)/2;
                if (a[m].CompareTo(key) == 0)
                {
                    return countEqual ? GetLastEqualElement(a, key, m, r) : GetFirstEqualElement(a, key, m, l);
                }
                else if (a[m].CompareTo(key) < 0)
                {
                    if (m + 1 <= r)
                    {
                        if (a[m + 1].CompareTo(key) < 0)
                        {
                            l = m + 1;
                        }
                        else if (a[m + 1].CompareTo(key) == 0)
                        {
                            return countEqual ? GetLastEqualElement(a, key, m + 1, r) : m + 1;
                        }
                        else if (a[m + 1].CompareTo(key) > 0)
                        {
                            return m + 1;
                        }
                    }
                    else
                    {
                        // all elements are less then key
                        return r + 1;
                    }
                }
                else
                {
                    // a[m] > key

                    if (m - 1 >= l)
                    {
                        if (a[m - 1].CompareTo(key) < 0)
                        {
                            return m;
                        }
                        else if (a[m - 1].CompareTo(key) == 0)
                        {
                            return countEqual ? m : GetFirstEqualElement(a, key, m - 1, l);
                        }
                        else
                        {
                            r = m - 1;
                        }
                    }
                    else
                    {
                        return 0;
                    }                    
                }
            }

            return 0;
        }

        private static int GetLastEqualElement<T>(T[] a, T key, int position, int lastPosition) where T: IComparable<T>
        {
            var j = 1;
            while (position + j <= lastPosition && a[position + j].CompareTo(key) == 0)
            {
                j++;
            }
            return position + j;
        }

        private static int GetFirstEqualElement<T>(T[] a, T key, int position, int firstPosition) where T: IComparable<T>
        {
            var j = 1;
            while (position - j >= firstPosition && a[position - j].CompareTo(key) == 0)
            {
                j++;
            }
            return position - j + 1;
        }
    }
}