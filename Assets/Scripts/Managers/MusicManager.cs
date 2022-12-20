using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Diagnostics.Contracts;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{

    //variables
    [Range(0, 1)] private float musicVolume;
    [Range(0, 1)] private float soundVolume;

    //references
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private AudioClip menuAudio;
    [SerializeField] private List<AudioClip> gameMusicClips;

    private AudioSource myAudio;
    private AudioSource soundSource;

    private bool inMenu = true;

    //getter
    public bool InMenu
    {
        get { return inMenu; }
    }

    //save the music volume
    public void SaveMusicVolume()
    {
        PlayerPrefs.SetFloat(Tags.PPREFS_MUSIC_VOLUME, musicSlider.value);
    }

    public void SaveMasterVolume()
    {
        PlayerPrefs.SetFloat(Tags.PPREFS_MASTER_VOLUME, FindObjectOfType<Options>().MasterVolume);
    }

    public void SaveSFXVolume()
    {
        PlayerPrefs.SetFloat(Tags.PPREFS_SFX_VOLUME, soundVolume);
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
        //sets the volumes
        musicVolume = PlayerPrefs.GetFloat(Tags.PPREFS_MASTER_VOLUME, 0.5f);
        soundVolume = PlayerPrefs.GetFloat(Tags.PPREFS_SFX_VOLUME, 0.5f);
        //gets the components
        myAudio = GetComponent<AudioSource>();
        soundSource = FindObjectOfType<Soundsource>().GetComponent<AudioSource>();
        //adjust the volumes
        myAudio.volume = musicVolume;
        soundSource.volume = soundVolume;
        //sets the sliders
        musicSlider.value = myAudio.volume;
        sfxSlider.value = soundSource.volume;
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

        //if there is no soundsource, find it and apply the audiosource to it
        if(soundSource == null)
        {
           Soundsource sfxSource = FindObjectOfType<Soundsource>();
           soundSource = sfxSource.GetComponent<AudioSource>();
        }
    }



}
