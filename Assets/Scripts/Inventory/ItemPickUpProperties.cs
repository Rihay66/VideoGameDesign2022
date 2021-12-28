using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUpProperties : MonoBehaviour
{
    public float radius = 3f;
    //shop variables
    public bool isShopItem = false;
    public float shopCost;

    [HideInInspector]
    public GameObject player;
    public bool hasInteracted { get; private set; }

    public virtual void InteractAction()
    {
        //Do something like sub or add health
        // This method is meant to be overwritten
        // if(hasInteracted == true)
        // {
        //     Debug.Log("Interacted with " + transform.name);
        // }

        // adding on buyable piece here

    }

    void Start()
    {  
        if (PlayerManager.instance.playerInstance != null)
        {
            player = PlayerManager.instance.playerInstance;
        }
        else
        {
            if (PlayerManager.instance == null)
            {
                Debug.LogError(PlayerManager.instance.name + " Is either not found or its missing!");
                return;
            }
        }
    }

    private void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < radius)
            {
                hasInteracted = true;
            }
            else if(distance > radius)
            {            
                hasInteracted = false;
            }
            InteractAction();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
