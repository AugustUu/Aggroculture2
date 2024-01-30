using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    
    public int width;
    public int height;
    public bool stackable;
    public bool equippable;
    public Sprite item_icon;
    public ItemType item_type;
    
}