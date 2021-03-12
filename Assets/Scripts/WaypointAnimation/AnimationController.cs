using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Is responsible for animating a single collection of waypoints
/// </summary>
public class AnimationController
{
    private AnimatableEntity _subject;
    private float _speed;
    private WaypointScript[] _waypoints;

    // If true, will return the subject to its position before the animation
    private bool _returnToOrigin;
    private Vector3 _originPosition;

    bool _isAtEnd = false;

    public AnimationController(AnimatableEntity subject, float speed, WaypointScript[] waypoints, bool returnToOrigin)
    {
        _subject = subject;
        _speed = speed;
        _waypoints = waypoints;
        _returnToOrigin = returnToOrigin;

        _originPosition = _subject.transform.position;
    }

    /// <summary>
    /// This is a coroutine that loops over all waypoints and animates them individually
    /// </summary>
    /// <returns></returns>
    public IEnumerator Animate()
    {
        foreach (WaypointScript waypoint in _waypoints)
        {
            yield return waypoint.Animate(_subject, _speed);
        }
        if (_returnToOrigin) yield return _subject.MoveTo(_originPosition, _speed);
        _isAtEnd = true;
    }

    public bool IsAtEnd()
    {
        return _isAtEnd;
    }
}
