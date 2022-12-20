using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{

    //variables
    [Range(0, 1)] private float musicVolume;

    //references
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioClip menuAudio;
    [SerializeField] private List<AudioClip> gameMusicClips;

    private AudioSource myAudio;

    private bool inMenu = true;

    //save the music volume
    public void SaveMusicVolume()
    {
        PlayerPrefs.SetFloat(Tags.PPREFS_MUSIC_VOLUME, musicSlider.value);
    }

    private void Awake()
    {
        MakeSingleton();
    }

    //make the manager that it doesn;t get destroyed between scenes, and only one at a time
    private void MakeSingleton()
    {
        //find the amount of musicmanagers
        int singleTon = FindObjectsOfType<MusicManager>().Length;

        //if there is more than one
        if(singleTon > 1)
        {
            //destroy the gameobject
            Destroy(this);
        }
        else
        {
            //dont destroy the singleton at any other stage
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        Initiliaze();
        SetMusicVolume(musicVolume);
    }

    //initlize all variables and set them
    private void Initiliaze()
    { 
        musicVolume = PlayerPrefs.GetFloat(Tags.PPREFS_MASTER_VOLUME, 0.5f);
        myAudio = GetComponent<AudioSource>();
        myAudio.volume = musicVolume;
        musicSlider.value = myAudio.volume;
        //if in the menu
        if (inMenu)
        {
            //play menu music
            myAudio.PlayOneShot(menuAudio);
        }
    }

    //sets the volume of the audiosource
    public void SetMusicVolume(float musicVolume)
    {
        myAudio.volume = musicVolume;
    }

    private void Update()
    {
        //when the audioSource stops playing
        if (!myAudio.isPlaying)
        {
            //if in the menu
            if (inMenu)
            {
                //play the menu music
                myAudio.PlayOneShot(menuAudio);
            }
        }
    }

}
