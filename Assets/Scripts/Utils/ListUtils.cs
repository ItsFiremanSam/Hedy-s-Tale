using System.Collections.Generic;
using System;
using System.Linq;

public static class ListUtils
{
    private static Random rng = new Random();

    // Using custom method O(n) instead of list.OrderBy(item => rng.Next()) O(n log(n))
    // Source: https://stackoverflow.com/questions/273313/randomize-a-listt/3456788
    public static List<T> Shuffle<T>(this List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

        return list;
    }
}