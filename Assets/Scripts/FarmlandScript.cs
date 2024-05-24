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
        
    }
    void OnDrawGizmos()
    {
        
        Handles.Label(transform.position, "Plant: " + plant + "\nGrowth: " + growth);
        
    }
    


    public void Plant(SeedType seedType){
        if(plant == SeedType.None){
            plant = seedType;
        }
        InvokeRepeating("grow", 1, 0.2f / (3 * (UpgradeUi.getUpgradeInfo(UpgradeList.growthUP).value + 1)));
    }

    public void RemovePlant(){
        growth = 0;
        plant = SeedType.None;
        CancelInvoke("grow");
        gameObject.GetComponent<PlantCrop>().shovelPlant();
        XPSystem.changeExp(25);
    }

    void grow()
    {
        if(growth < Utils.plant_list[(int) plant].grow_time){
            growth += 1;
        }
        //Invoke("grow", growth_speed);
    }


}
