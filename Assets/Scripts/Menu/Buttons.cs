using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    //variables
    [SerializeField] private int startAnimation;
    [SerializeField] private int disableOptionPanel;

    //References
    [SerializeField] private GameObject optionsPanel;

    private AudioSource buttonSound;

    //starts the game
    private void Start()
    {
        buttonSound = GetComponent<Soundsource>().GetComponent<AudioSource>();
    }

    //quits the game
    public void Quit()
    {
        buttonSound.PlayOneShot(GetComponent<Soundsource>().PlayButtonSound());
        Application.Quit();
    }

    //opens option button
    public void Options()
    {
        buttonSound.PlayOneShot(GetComponent<Soundsource>().PlayButtonSound());
        StartCoroutine(StartOptions());
    }

    //when the back button is pressed in the options panel
    public void OptionsBack()
    {
        MusicManager saveAudio = FindObjectOfType<MusicManager>();
        saveAudio.SaveMusicVolume();
        saveAudio.SaveMasterVolume();
        saveAudio.SaveSFXVolume();
        buttonSound.PlayOneShot(GetComponent<Soundsource>().PlayButtonSound());
        StartCoroutine(OnOptionsBackPressed());
    }

    private void StartButton()
    {
        buttonSound.PlayOneShot(GetComponent<Soundsource>().PlayButtonSound());
        FindObjectOfType<SceneChanger>().ChangeToNextScene();
    }

    //activates option panel and plays animation
    private IEnumerator StartOptions()
    {
        optionsPanel.SetActive(true);
        yield return new WaitForSeconds(startAnimation);
        optionsPanel.GetComponent<Animator>().Play(Tags.PLAY_OPTIONS_PANEL_ENABLE);
    }

    //plays the return animation then disables the option panel
    private IEnumerator OnOptionsBackPressed()
    {
        optionsPanel.GetComponent<Animator>().Play(Tags.PLAY_OPTIONS_PANEL_DISABLE);
        yield return new WaitForSeconds(disableOptionPanel);
        optionsPanel.SetActive(false);
    }
}
