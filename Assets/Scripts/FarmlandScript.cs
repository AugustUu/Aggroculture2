using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FarmlandScript : MonoBehaviour
{
    public int plant = 0;
    public int growth = 0;


    void Start()
    {
        InvokeRepeating("grow", 0.0f, 1.0f);
    }
    void OnDrawGizmos()
    {
        Handles.Label(transform.position, "Plant: " + plant + "\nGrowth: " + growth);
    }

    void grow()
    {
        growth += 1;
    }


}
