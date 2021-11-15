using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUpProperties : MonoBehaviour
{
    public float radius = 3f;

    private GameObject player;
    private bool hasInteracted = false;

    public virtual void InteractAction()
    {
        //Do something like sub or add health
        // This method is meant to be overwritten
        Debug.Log("Interacted with " + transform.name);
    }

    void Start()
    {
        GameObject playerManager = GameObject.FindGameObjectWithTag("GameManager");
        if (playerManager.GetComponent<PlayerManager>() != null)
        {
            player = PlayerManager.instance.playerInstance;
        }
        else
        {
            if (playerManager.GetComponent<PlayerManager>() == null)
            {
                Debug.LogError(playerManager.name + " DOES NOT HAVE PlayerTargetManager.cs!!!");
            }
        }
    }

    private void Update()
    {
        if (player != null && !hasInteracted)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance <= radius)
            {
                InteractAction();
                hasInteracted = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
