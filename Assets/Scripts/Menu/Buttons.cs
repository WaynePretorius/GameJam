using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel;

    [SerializeField] private int startAnimation;
    [SerializeField] private int disableOptionPanel;

    //quits the game
    public void Quit()
    {
        Application.Quit();
    }

    //starts the game
    public void Start()
    {
        
    }

    //opens option button
    public void Options()
    {
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
