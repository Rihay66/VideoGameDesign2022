using UnityEngine;

public class EnemyItemDrop : MonoBehaviour
{
    [Header("Set what items the enemy will drop")]
    public int RandomItemRarity;// Here will set how rare an item will spawn in. Make sure the value is negative
    public GameObject[] ItemPrefabs; //Here set the maximum number of items or gameobjects the enemy will drop

    private int randomItem;

    private void Start()
    {
        randomItem = Random.Range(-RandomItemRarity, ItemPrefabs.Length);
    }

    public void DropRandomItem()
    {
        if (randomItem < 0)
        {
            Debug.Log("No Item has been spawned in!!");
        }
        else
        {
            if (randomItem >= 0)
            {
                Instantiate(ItemPrefabs[randomItem], transform.position, Quaternion.identity);
            }
        }

    }
}
