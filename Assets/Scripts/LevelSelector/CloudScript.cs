using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    [HideInInspector]
    public int dissapearLevel = int.MaxValue;

    private CanvasGroup _canvasGroup;
    private bool startAnimation = false;

    private float _animationTimer;
    private float _alphaChange;
    private Vector3 _animationDirection;
    private float _speed;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (Time.time < _animationTimer)
        {
            _canvasGroup.alpha -= _alphaChange * Time.deltaTime;
            transform.position += _animationDirection * _speed * Time.deltaTime;
        }
        else if (startAnimation)
        {
            gameObject.SetActive(false);
            startAnimation = false;
        }
    }

    public void StartDissapearingAnimation(float animationTime, float animationAngleInDegrees, float speed)
    {
        _alphaChange = 1 / animationTime;
        _animationTimer = Time.time + animationTime;
        _animationDirection = (Quaternion.AngleAxis(animationAngleInDegrees, Vector3.forward) * Vector3.right).normalized;
        _speed = speed;
        startAnimation = true;
    }
}
