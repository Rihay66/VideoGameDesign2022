using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Transform playerPosition;

    private void Start()
    {
        if (PlayerManager.instance.playerInstance != null)
        {
            playerPosition = PlayerManager.instance.playerInstance.transform;
        }
        else
        {
            // If there isn't a PlayerManager it will give an error
            Debug.LogError(PlayerManager.instance.name + " Is either not found or its missing!");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3 (playerPosition.position.x, playerPosition.position.y, playerPosition.position.z - 10f);
    }
}
