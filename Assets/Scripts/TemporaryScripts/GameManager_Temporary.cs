using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager_Temporary : MonoBehaviour
{
    public static GameManager_Temporary instance;

    private void Awake()
    {
        if(instance != this)
        {
            instance = this;
        }
    }

    public void restartLevel()
    {
        // Is called to restart the same scene
    }

    public void EndOfGame()
    {
        // Called to show a end screen
    }
}
