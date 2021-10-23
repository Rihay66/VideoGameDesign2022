using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : EntityStats
{
    public override void Die()
    {
        base.Die();
        gameObject.SetActive(false); // Makes the player invisible but still in the scene

        // Finds and sets the GameManager Script instance from the scene
        GameManager_Temporary manager = GameManager_Temporary.instance.GetComponent<GameManager_Temporary>();
        if(manager != null)
        {
            manager.restartLevel();
        }
    }
}
