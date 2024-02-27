using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Dictionary<SeedType, int> farm_dict =
    new Dictionary<SeedType, int>{//seedtype, itemid
        {SeedType.Carrot, 0},
        {SeedType.Potato, 3}
    };
}
