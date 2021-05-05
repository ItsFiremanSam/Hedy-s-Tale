using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : AnimatableEntity
{
    public Text speechBubble;
    public GameObject CinematicBars;
    public Animator animator;

    // Hedy can't move when being animated or in the Coding UI
    [HideInInspector]
    public bool AnimationActive;
    [HideInInspector]
    public bool CodingUIActive;
    [HideInInspector]
    public bool DialogUIActive;

    Rigidbody2D _rigidBody;
    Vector2 _currentVelocity;
    private BoxCollider2D _collider;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        HandleAnimationVariables();
        if (!AnimationActive && !CodingUIActive && !DialogUIActive)
        {
            Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * Speed * Time.fixedDeltaTime);
        }
    }

    /// <summary>
    /// This will toggle the cinematic bars during an animation and also disable the collider
    ///     - The disabling of the collider is done because of movement issues when collding with the environment
    ///     - Pathfinding would be a better solution, but this is tricky to implement in 2D, so that's why this solution is chosen
    ///     - Another solution could be to use transform.position in stead of velocity, but this will also stop the walk animation from being shown
    /// </summary>
    private void HandleAnimationVariables()
    {
        if (AnimationActive)
        {
            if (!CinematicBars.activeSelf) CinematicBars.SetActive(true);
            if (_collider.enabled) _collider.enabled = false;
        }
        else
        {
            if (CinematicBars.activeSelf) CinematicBars.SetActive(false);
            if (!_collider.enabled) _collider.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = _currentVelocity;

        animator.SetFloat("speed", Mathf.Abs(_currentVelocity.magnitude));

        if (_currentVelocity != Vector2.zero)
        {
            animator.SetFloat("x", _currentVelocity.x);
            animator.SetFloat("y", _currentVelocity.y);
        }

        _currentVelocity = Vector2.zero;
    }

    bool Move(Vector2 velocity)
    {
        if (_currentVelocity != Vector2.zero) return false;

        _currentVelocity = velocity;

        return true;
    }

    /// <summary>
    /// A coroutine that will move the gameobject to a certain position
    /// </summary>
    public override IEnumerator MoveTo(Vector2 pos, float speed)
    {
        // TODO: Implement Unity built in pathfinding
        while (true)
        {
            // Will move towards the direction of the new position
            Vector2 velNorm = (pos - (Vector2)transform.position).normalized * speed * Time.fixedDeltaTime;
            Vector2 vel = (pos - (Vector2)transform.position) * speed * Time.fixedDeltaTime;

            // If the position is closer by than a single step in the right direction, take the last step and return
            if (velNorm.sqrMagnitude > vel.sqrMagnitude)
            {
                while (!Move(vel)) yield return null;
                break;
            }
            while (!Move(velNorm)) yield return null;
        }
    }



    public override IEnumerator Speak(TalkAction ta)
    {
        float endTime = Time.time + ta.Seconds;
        // TODO: Start speech bubble
        speechBubble.text = ta.Message;
        while (Time.time < endTime) yield return null;
        speechBubble.text = "";
        // TODO: Delete speech bubble
    }
}
