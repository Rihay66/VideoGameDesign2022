 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_text : MonoBehaviour
{
    private bool isNear;
    private bool interaction = false;
    private bool checkBoxes = false;
    private int textSelection = -1;
    private GameObject player;
    public GameObject canvasObject;
    private List<GameObject> textBoxes ;

    public float radius = 3f;

    // Start is called before the first frame update
    void Start()
    {
        AddChildren();
        if (PlayerManager.instance.playerInstance != null)
        {
            player = PlayerManager.instance.playerInstance;
        }
        else
        {
            // If there isn't a PlayerManager it will give an error
            Debug.LogError(PlayerManager.instance.name + " Is either not found or its missing!");
            return;
        }
    }

    void AddChildren()
    {
        //Adds all text boxes to the list
        textBoxes = new List<GameObject>();
        foreach (Transform tran in canvasObject.transform)
        {
            textBoxes.Add(tran.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkForDistance();
    }

    void checkForDistance()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < radius)
            {
                isNear = true;
            }
            else if (distance > radius)
            {
                isNear = false;
                checkBoxes = true;
            }
            InteractInput();
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    void CheckForSelection(int i)
    {
        if(i > -1)
        {
            textBoxes[i].SetActive(true);
            if(i > 0)
            {
                textBoxes[i - 1].SetActive(false);
            }
            //print(textBoxes[i].gameObject.name + " has been selected");
            StartCoroutine(waitForNext());
        }
    }

    void InteractInput()
    {
        if(textSelection < textBoxes.Count)
        {
            CheckForSelection(textSelection);
        }
        if(isNear == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && interaction == false)
            {
                textSelection++;
                print("Continuing text");
                interaction = true;
            }
            
        }
        else if(isNear == false && checkBoxes == true)
        {
            textSelection = 0;
            for(int i = 0; i < textBoxes.Count;)
            {
                if(i == textBoxes.Count)
                {
                    checkBoxes = false;
                }
                textBoxes[i].SetActive(false);
                i++;            
            }
        }
    }

    //[]Make this into a cooldown for input
    IEnumerator waitForNext()
    {
        yield return new WaitForSeconds(1f);
        interaction = false;
        StopCoroutine(waitForNext());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
