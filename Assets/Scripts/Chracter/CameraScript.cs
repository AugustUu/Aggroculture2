using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public new Camera camera; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (camera.orthographicSize <= 2.5){
            camera.orthographicSize = 2.5f;
        }
        camera.orthographicSize -= 1*Input.mouseScrollDelta.y;
    }
}
