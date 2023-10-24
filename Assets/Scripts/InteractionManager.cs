using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    public static Dictionary<int,string> interactables = new Dictionary<int,string>();

    [SerializeField]
    public static TextMeshProUGUI text;

    public static void onEnter(Collider collider){
        interactables.Add(collider.transform.GetInstanceID(),collider.name);
    }

    public static void onExit(Collider collider){
        
    }
}
