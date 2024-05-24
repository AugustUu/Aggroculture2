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
    gozoman,
    tomato_seeds,
    potato_seeds,
    carrot_seeds,
    tomato,
    potato,
    carrot,
    wooden_claw,
    blue_slime,
    green_slime,
    red_slime,
    premium_fertilizer,
    shoel,
    stockedAK,
    stocklessAK,
    oldGun,
    glock,
    shotgun,
    smg,
    lawnMower,
    rat_tail
}


public enum UpgradeList{
    speedUp,
    healthUp,
    regenUp,
    dammageUp,
    firerateUp,
    shotsUp,
    growthUP,
    magnetUp,
    pierceUp
}