using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Soundsource : MonoBehaviour
{
    [SerializeField] private AudioClip buttonSound;

    //returns the sound for the button
    public AudioClip PlayButtonSound()
    {
        return buttonSound;
    }
}
