using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Is responsible for the trigger functionality to start an animation
/// </summary>
/// <remarks>
/// Can only be triggered once
/// After the animation it will set itself to inactive
/// </remarks>
public class AnimationTrigger : MonoBehaviour
{
    public bool DrawGizmos = true;

    List<AnimationController> _animationControllers;
    bool _activated;
    PlayerMovement _mp;

    /// <summary>
    /// Checks if the player collided with its collider 
    /// Starts the animation coroutines of the waypointcollection of the trigger's children
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_activated && (_mp = collision.gameObject.GetComponent<PlayerMovement>()) != null && !_mp.AnimationActive)
        {
            _animationControllers = new List<AnimationController>();
            foreach (WaypointCollectionScript wps in GetComponentsInChildren<WaypointCollectionScript>())
            {
                AnimationController ac = new AnimationController(wps.Subject, wps.Subject.Speed * wps.SpeedMultiplier, wps.GetComponentsInChildren<WaypointScript>(), wps.ReturnToOrigin);
                StartCoroutine(ac.Animate());
                _animationControllers.Add(ac);
            }
            _activated = true;
            _mp.AnimationActive = true;
        }
    }

    /// <summary>
    /// Makes sure the player that after the animation:
    /// - The player is no longer tagged as animated
    /// - This trigger is set to inactive
    /// </summary>
    private void FixedUpdate()
    {
        if (_activated)
        {
            if (IsAnimationDone())
            {
                _mp.AnimationActive = false;
                gameObject.SetActive(false);
            }
        }
    }

    bool IsAnimationDone()
    {
        foreach (AnimationController ac in _animationControllers)
        {
            if (!ac.IsAtEnd()) return false;
        }
        return true;
    }
}
