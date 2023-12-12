using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FarmlandScript : MonoBehaviour
{
    public int plant = 0;
    public int growth = 0;

    private MeshFilter mesh;

    public Mesh[] stages;

    void Start()
    {
        mesh = this.GetComponent<MeshFilter>();
        mesh.mesh = stages[0];
        InvokeRepeating("grow", 0.0f, 1.0f);
    }
    void OnDrawGizmos()
    {
        Handles.Label(transform.position, "Plant: " + plant + "\nGrowth: " + growth);
    }

    void grow()
    {
        growth += 1;

        mesh.mesh = stages[growth%4];
    }


}
