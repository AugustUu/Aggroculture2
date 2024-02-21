using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[Serializable]
public class GunStats{
    public int firerate;
    public int max_ammo;
    public int mag_size;
    public int damage;
    public int recoil;

    public float spread;

    public int extra_shots;
}


[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    
    public int width;
    public int height;
    public bool stackable;
    public bool equippable;
    public Sprite item_icon;
    public ItemType item_type;
    public SeedType seed_type;

    public GunStats gun_stats;
    public int food_heal_amount;
}
