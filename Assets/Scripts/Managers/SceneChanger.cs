using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    //variables
    int currentScene;

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
        if (singleTon > 1)
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

    //used to get to the next scene, in this case from menu to sandbox
    public void ChangeToNextScene()
    {
        //change the scene to the next scene
        SceneManager.LoadScene(GetSceneIndex() + 1);
        FindObjectOfType<MusicManager>().InMenu = false;
    }

    private int GetSceneIndex()
    {
        //get the current scene number
        currentScene = SceneManager.GetActiveScene().buildIndex;

        //return it is an integer
        return currentScene;
    }

}
