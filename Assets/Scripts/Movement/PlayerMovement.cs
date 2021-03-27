using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : AnimatableEntity
{
    public Text speechBubble;
    public GameObject CinematicBars;
    public Animator animator;

    [HideInInspector]
    public bool AnimationActive;

    Rigidbody2D _rigidBody;
    Vector2 _currentVelocity;

    public List<TempAnswerBlock> Inventory;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleCinematicBars();
        if (!AnimationActive)
        {
            Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * Speed * Time.fixedDeltaTime);
        }
    }

    private void HandleCinematicBars()
    {
        if (AnimationActive && !CinematicBars.activeSelf) CinematicBars.SetActive(true);
        if (!AnimationActive && CinematicBars.activeSelf) CinematicBars.SetActive(false);
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
