using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Dictionary<SeedType, int> farm_dict =
    new Dictionary<SeedType, int>{//seedtype, itemid
        {SeedType.Carrot, 0},
        {SeedType.Potato, 3}
    };
}

public enum ItemList{
    schmeeze,
    tomato_seeds,
    potato_seeds,
    carrot_seeds,
    tomato,
    potato,
    carrot,
    gozoman,
    stockedAK,
    shoel,
    stocklessAK,
    oldGun,
    glock,
    shotgun,
    smg
    
}
