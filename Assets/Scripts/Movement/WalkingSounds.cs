using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSounds : MonoBehaviour
{
    public AudioClip[] stepSounds;
    public AudioSource walkingSFXSource;

    private void Step()
    {
        walkingSFXSource.PlayOneShot(GetRandomClip());
    }

    private AudioClip GetRandomClip()
    {
        return stepSounds[Random.Range(0, stepSounds.Length)];
    }
} 
