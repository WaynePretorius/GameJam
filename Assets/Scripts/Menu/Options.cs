using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

/// <summary>
/// Changes the following options in runtime
/// Master volume
/// FullScreen
/// </summary>
public class Options : MonoBehaviour
{
    //Variables
    [Range(-64f, 0f)][SerializeField] private float masterVolume;

    //getters
    public float MasterVolume
    {
        get { return masterVolume; }
    }

    //References
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private AudioMixer masterMixer;

    private Resolution[] resolutions;

    private void Start()
    {
        //Get the resolutions according to your system
        GetSystemResolutions();

        //set the master volume to what was saved
        masterVolume = PlayerPrefs.GetFloat(Tags.PPREFS_MASTER_VOLUME, -0.01f);
    }

    //get the current resolutions for the system
    private void GetSystemResolutions()
    {
        //make a variable array of resultions and clear the dropdown menu
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        //declare a list of strings that stores the resolutions
        List<string> options = new List<string>();

        //create a index for the resolution
        int currentResolutionIndex = 0;

        //run through all the available resolutions
        for(int i = 0; i < resolutions.Length; i++)
        {
            //add resolutions to the string list indexes
            string optionsString = resolutions[i].width + " x " + resolutions[i].height;

            //add resolutionsinto the string list
            options.Add(optionsString);

            //look if it is the same as the current screen resolution, if so, set it as the current index
            if (resolutions[i].width == Screen.currentResolution.width 
                && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = 1;
            }
        }

        //add all to dropdown, refresh the shown value and set the resolution to current resolution
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void OnEnable()
    {
        GetVolume();
    }

    //Set the master volume according to the slider
    public void SetMasterVolume(float volume)
    {
        if(masterMixer == null) { return; }
        masterMixer.SetFloat(Tags.MIXER_MASTER_VOLUME, volume);
        masterVolume = volume;
    }

    //get the volume of the master mixer
    public void GetVolume()
    {
        masterVolume = PlayerPrefs.GetFloat(Tags.PPREFS_MASTER_VOLUME);
    }

    //sets the game windowed or fullscreen
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    //get the target index of the resolution and set it
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

}
