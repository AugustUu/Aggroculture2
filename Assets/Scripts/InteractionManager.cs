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
    public TextMeshProUGUI text;

    public static TextMeshProUGUI ui_text;

    void Start(){
        ui_text = text;
    }

    public static void onEnter(int id,String name){
        interactables.Add(id,name);
        Debug.Log(name);

        updateText();
    }

    public static void onExit(int id){
        interactables.Remove(id);

        updateText();
    }

    static void updateText(){
        ui_text.text = "";
        foreach (String value in interactables.Values){
            ui_text.text += value + "\n";
        }
    }
}
