using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlantData : ScriptableObject
{
    public int grow_time;
    public ItemList seed_item;
    public ItemList produce_item;
}
