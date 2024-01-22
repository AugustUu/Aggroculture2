using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ItemData : ScriptableObject
{

    
    [Serializable]
    public struct GunStats{
        public int fire_rate;
        public int dammage;
        public int recoil;
    }
    
    public int width = 1;
    public int height = 1;

    public Sprite item_icon;

    public int item_type = 1;
    public GunStats gunStats;
}
