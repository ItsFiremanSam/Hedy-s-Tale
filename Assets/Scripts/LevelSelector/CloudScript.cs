using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CloudScript : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private bool _startAnimation = false;

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
        else if (_startAnimation)
        {
            gameObject.SetActive(false);
            _startAnimation = false;
        }
    }

    public void StartDisappearingAnimation(float animationTime, float animationAngleInDegrees, float speed)
    {
        _alphaChange = 1 / animationTime;
        _animationTimer = Time.time + animationTime;
        _animationDirection = (Quaternion.AngleAxis(animationAngleInDegrees, Vector3.forward) * Vector3.right).normalized;
        _speed = speed;
        _startAnimation = true;
    }
}
