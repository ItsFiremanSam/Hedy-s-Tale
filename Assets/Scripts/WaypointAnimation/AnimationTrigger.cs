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
    public bool useCollider = true;
    public bool DrawGizmos = true;

    private List<AnimationController> _animationControllers;
    private bool _activated;
    private PlayerMovement _playerMovement;

    private CodingPuzzle _codingPuzzle;
    private CodingBlocksAnimation _codingBlocksAnimation;

    private void Awake()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _codingPuzzle = GetComponentInParent<CodingPuzzle>();
        if (_codingPuzzle) _codingBlocksAnimation = FindObjectOfType<CodingBlocksAnimation>();
    }

    public void ProgressCodingBlocksAnimation(int progressSteps)
    {
        if (_codingPuzzle)
            _codingBlocksAnimation.ProgressAnimation(progressSteps);
    }

    /// <summary>
    /// Checks if the player collided with its collider 
    /// Starts the animation coroutines of the waypointcollection of the trigger's children
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_activated && collision.gameObject.GetComponent<PlayerMovement>() && !_playerMovement.AnimationActive)
        {
            StartAnimation();
        }
    }

    public void StartAnimation()
    {
        _animationControllers = new List<AnimationController>();
        if (_codingPuzzle) _codingBlocksAnimation.EnterCodingBlocksAnimation(_codingPuzzle.Answer);
        foreach (WaypointCollectionScript wps in GetComponentsInChildren<WaypointCollectionScript>())
        {
            AnimationController ac = new AnimationController(wps.Subject, wps.SpeedMultiplier, wps.GetComponentsInChildren<WaypointScript>(), wps.ReturnToOrigin);
            StartCoroutine(ac.Animate());
            _animationControllers.Add(ac);
        }
        _activated = true;
        _playerMovement.AnimationActive = true;
    }

    /// <summary>
    /// Makes sure the player that after the animation:
    /// - The player is no longer tagged as animated
    /// - This trigger is set to inactive
    /// </summary>
    private void FixedUpdate()
    {
        if (_activated && IsAnimationDone())
        {
            // Makes sure that application doesn't crash when there are no coding blocks being animated
            if (_codingBlocksAnimation)
                _codingBlocksAnimation.ExitCodingBlocksAnimation();
            _playerMovement.AnimationActive = false;
            gameObject.SetActive(false);
        }
    }

    private bool IsAnimationDone()
    {
        foreach (AnimationController ac in _animationControllers)
        {
            if (!ac.IsAtEnd()) return false;
        }
        return true;
    }

    private void OnValidate()
    {
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        if (!useCollider && col.enabled)
            col.enabled = false;
        if (useCollider && !col.enabled)
            col.enabled = true;
    }
}
