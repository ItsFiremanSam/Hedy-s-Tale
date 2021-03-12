using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvilHedyMovement : AnimatableEntity
{

    public Text speechBubble;

    private Rigidbody2D _rigidBody;
    private Vector2 _currentVelocity;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = _currentVelocity;
        _currentVelocity = Vector2.zero;
    }

    private bool Move(Vector2 velocity)
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
            Vector2 normalizedVelocity = (pos - (Vector2)transform.position).normalized * speed * Time.fixedDeltaTime;
            Vector2 velocity = (pos - (Vector2)transform.position) * speed * Time.fixedDeltaTime;

            // If the position is closer by than a single step in the right direction, take the last step and return
            if (normalizedVelocity.sqrMagnitude > velocity.sqrMagnitude)
            {
                while (!Move(velocity)) yield return null;
                break;
            }
            while (!Move(normalizedVelocity)) yield return null;
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
