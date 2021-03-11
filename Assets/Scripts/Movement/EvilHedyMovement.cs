using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvilHedyMovement : AnimatableEntity
{

    public Text speechBubble;

    Rigidbody2D _rb;
    Vector2 _curVel;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = _curVel;
        _curVel = Vector2.zero;
    }

    bool Move(Vector2 vel)
    {
        if (_curVel != Vector2.zero) return false;

        _curVel = vel;
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
