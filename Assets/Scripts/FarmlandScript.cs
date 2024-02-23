using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FarmlandScript : MonoBehaviour
{
    public SeedType plant;
    public int growth = 0;



    void Start()
    {
        InvokeRepeating("grow", 5.0f, 5.0f);
    }
    void OnDrawGizmos()
    {
        Handles.Label(transform.position, "Plant: " + plant + "\nGrowth: " + growth);
    }

    public void Plant(SeedType seed_type){
        if(plant == SeedType.None){
            plant = seed_type;
            Debug.Log("planted " + plant);
        }
    }

    void grow()
    {
        growth += 1;
    }


}
