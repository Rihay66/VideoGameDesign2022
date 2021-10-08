using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    private void Awake()
    {
        if(instance != this)
        {
            instance = this;
        }
        //Finds the player by tag and sets it to Game object class for other scripts to reference the player
        playerInstance = GameObject.FindGameObjectWithTag("Player");
        if (playerInstance.tag == "Player" && playerInstance != null)
        {
            print("Player found!");
        }
        else
        {
            Debug.LogError("Player not found in scene!");
        }
    }

    public GameObject playerInstance;

}
