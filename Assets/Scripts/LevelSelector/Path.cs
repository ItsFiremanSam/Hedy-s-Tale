﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// source: https://www.youtube.com/watch?v=n_RHttAaRCk
/// </summary>
[Serializable]
public class Path
{
    [SerializeField, HideInInspector]
    List<Vector2> points;

    public Vector2 this[int i]
    {
        get
        {
            return points[i];
        }
    }

    public int NumSegments
    {
        get
        {
            return (points.Count - 4) / 3 + 1;
        }
    }

    public int NumPoints
    {
        get
        {
            return points.Count;
        }
    }

    public Path(Vector2 center)
    {
        float vectorMultiplier = 300;
        Vector2 left = Vector2.left * vectorMultiplier;
        Vector2 right = Vector2.right * vectorMultiplier;
        Vector2 leftUp = left + Vector2.up * vectorMultiplier;
        Vector2 rightDown = right + Vector2.down * vectorMultiplier;
        points = new List<Vector2>() {
            center + left,
            center + leftUp * 0.5f,
            center + rightDown * 0.5f,
            center + right
        };
    }

    public void AddSegment(Vector2 anchorPos)
    {
        points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
        points.Add(points[points.Count - 1] + anchorPos * 0.5f);
        points.Add(anchorPos);
    }

    public void DeleteLastSegment()
    {
        for (int i = 0; i < 3; i++)
        {
            if (NumPoints <= 4) return;
            points.RemoveAt(NumPoints - 1);
        }
    }

    public Vector2[] GetPointsInSegment(int i)
    {
        return new Vector2[] { points[i * 3], points[i * 3 + 1], points[i * 3 + 2], points[i * 3 + 3] };
    }

    public void MovePoint(int i, Vector2 pos)
    {
        Vector2 deltaMove = pos - points[i];
        points[i] = pos;

        if (i % 3 == 0)
        {
            if (i + 1 < points.Count) points[i + 1] += deltaMove;
            if (i - 1 >= 0) points[i - 1] += deltaMove;
        }
        else
        {
            bool nextPointIsAnchor = (i + 1) % 3 == 0;
            int correspondingControlIndex = nextPointIsAnchor ? i + 2 : i - 2;
            int anchorIndex = nextPointIsAnchor ? i + 1 : i - 1;

            if (correspondingControlIndex >= 0 && correspondingControlIndex < points.Count)
            {
                float dist = (points[anchorIndex] - points[correspondingControlIndex]).magnitude;
                Vector2 dir = (points[anchorIndex] - pos).normalized;
                points[correspondingControlIndex] = points[anchorIndex] + dir * dist;
            }
        }
    }
}
