// TO BE PUT IN SHOP GRIDMAP PREFAB

using System.Collections;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    //making game object variables to call all the available items in the game, along with array of spawnpoints
    public GameObject[] possibleShopItems;

    private int spawnSelection = 0;
    private int itemSelection = 0;
    private int totalSpawnLocations;
    private int currentSpawnLocation;


    public Transform[] spawnLocations;

    //bools
    private bool stop = false;

    IEnumerator shopItemPlacement()
    {    
        while (!stop)
        {          
            for (int i = 0; i <= spawnLocations.Length; i++)
            {
                WaitForSeconds wait = new WaitForSeconds(1f);

                //debugging for the purpose of making sure code = good
                Debug.Log(i);
                Debug.Log("Spawning an item at " + spawnLocations[i]);
                Debug.Log(spawnLocations[i]);

                //spawn in shop item and using debug to confirm, also increasing possiblespawns by 1
                Instantiate(possibleShopItems[itemSelection], spawnLocations[spawnSelection].position, spawnLocations[spawnSelection].rotation);
                spawnSelection++;
                Debug.Log(possibleShopItems[itemSelection]);

                // for purpose of stopping for statement
                if (spawnSelection == spawnLocations.Length)
                {
                    stop = true;
                }
                yield return wait;
            }
        }
    }
    void ErrorCheck()
    {
        if(spawnLocations.Length < 0 || possibleShopItems.Length < 0)
        {
            Debug.LogError("No spawn locations or shop items detected! Did you forget to bind them?");
            stop = true;
            return;
        }
    }

    void CheckTotalItems()
    {
        //Checks if all items have been spawned on all needed spawn points
        currentSpawnLocation = spawnSelection;
        if(currentSpawnLocation == totalSpawnLocations)
        {
            stop = true;
            StopAllCoroutines();
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        ErrorCheck();
        totalSpawnLocations = spawnLocations.Length;
    }

    // Update is called once per frame
    void Update()
    {
        itemSelection = Random.Range(0, possibleShopItems.Length);
        CheckTotalItems();
        StartCoroutine(shopItemPlacement());
    }

}