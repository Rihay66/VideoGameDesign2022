using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform playerPosition;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3 (playerPosition.position.x, playerPosition.position.y, playerPosition.position.z - 10f);
    }
}
