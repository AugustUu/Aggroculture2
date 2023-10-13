using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FarmlandScript : MonoBehaviour
{
    public int plant = 0;
    
    void OnDrawGizmos() 
    {
        Handles.Label(transform.position, "Plant: " + plant);
    }

}
