using System.Collections.Generic;
using System;
using System.Linq;

public static class ListUtils
{
    private static Random rng = new Random();

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

    public static List<PuzzleBlock> CreateDeepCopy(this List<PuzzleBlock> list, bool isKeyword)
    {
        return list.Where(block => block.IsKeyword == isKeyword).Select(block => new PuzzleBlock(block)).ToList();
    }
}