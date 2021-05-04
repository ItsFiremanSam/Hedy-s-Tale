using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAnimation : MonoBehaviour
{   

    Animation yourAnimation;

    void Awake()
    {
        yourAnimation = this.GetComponent<Animation>();
    }

    // This is an example only

    void Update()
    {
        
            yourAnimation.Play("firstcutsceneanimation");
        
    }
}

