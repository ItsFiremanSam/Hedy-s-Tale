using System.Collections;
using UnityEngine;

/// <summary>
/// Abstract class to inherit from to make a gameobject animatable
/// </summary>
/// <remarks>
/// NOTE: This is subject to change
/// </remarks>
public abstract class AnimatableEntity : MonoBehaviour
{
    public float Speed = 400;
    bool _isAnimated;

    public abstract IEnumerator MoveTo(Vector2 pos, float speed);
    public abstract IEnumerator Speak(TalkAction ta);
}
