using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public GameObject cam;

    void Update()
    {
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        cam = camera;
        transform.LookAt(transform.position + cam.transform.forward);
    }
}
