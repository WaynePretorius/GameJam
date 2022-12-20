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

    //quits the game
    public void Quit()
    {
        Application.Quit();
    }

    //starts the game
    public void Start()
    {
        buttonSound = GetComponent<Soundsource>().GetComponent<AudioSource>();
    }

    //opens option button
    public void Options()
    {
        buttonSound.PlayOneShot(GetComponent<Soundsource>().PlayButtonSound());
        StartCoroutine(StartOptions());
    }

    //activates option panel and plays animation
    private IEnumerator StartOptions()
    {
        optionsPanel.SetActive(true);
        yield return new WaitForSeconds(startAnimation);
        optionsPanel.GetComponent<Animator>().Play(Tags.PLAY_OPTIONS_PANEL_ENABLE);
    }

    //when the back button is pressed in the options panel
    public void OptionsBack()
    {
        MusicManager saveAudio = FindObjectOfType<MusicManager>();
        saveAudio.SaveMusicVolume();
        saveAudio.SaveMasterVolume();
        StartCoroutine(OnOptionsBackPressed());
    }

    //plays the return animation then disables the option panel
    private IEnumerator OnOptionsBackPressed()
    {
        optionsPanel.GetComponent<Animator>().Play(Tags.PLAY_OPTIONS_PANEL_DISABLE);
        yield return new WaitForSeconds(disableOptionPanel);
        optionsPanel.SetActive(false);
    }
}
