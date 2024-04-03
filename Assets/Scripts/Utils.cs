using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Utils : MonoBehaviour
{
    [SerializeField]
    public List<PlantData> plant_list_init;
    public static List<PlantData> plant_list;
    
    void Start(){
        plant_list = plant_list_init;
    }
}

public enum ItemList{
    schmeeze,
    eeee,
    aaaa,
    big,
    gozoman,
    stockedAK,
    shoel,
    stocklessAK,
    oldGun,
    glock,
    shotgun,
    smg
}
