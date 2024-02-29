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
    eeee,
    aaaa,
    big,
    gozoman,
    stockedAK,
    shoel,
    stocklessAK,
    goofyAK,
    oldGun,
    glock,
    shotgun,
    smg
}
