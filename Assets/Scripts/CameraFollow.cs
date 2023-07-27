using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public player playercontroller;
    public GameObject player;
    public GameObject ghost;
    public float cameraHeight = 20.0f;
    public float cameraUp;
    public float diff;

    void Update()
    {
            Vector3 pos = player.transform.position;
            pos.y += cameraUp;
            pos.z += cameraHeight;
            transform.position = pos;
    }
}
  
