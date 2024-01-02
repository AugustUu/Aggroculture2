using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    
    public int width = 1;
    public int height = 1;
    public bool stackable = true;

    public Sprite item_icon;
}
