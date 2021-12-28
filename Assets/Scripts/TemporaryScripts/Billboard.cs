using UnityEngine;

public class Billboard : MonoBehaviour
{
    public GameObject cam;

    void FixedUpdate()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        cam = camera;
        transform.LookAt(transform.position + cam.transform.forward);
    }
}
