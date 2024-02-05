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
        
        camera.orthographicSize -= 1*Input.mouseScrollDelta.y;
    }
}
