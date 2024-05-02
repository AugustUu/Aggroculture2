using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{


    [SerializeField]
    public TextMeshProUGUI text;

    public static TextMeshProUGUI ui_text;

    public static SortedDictionary<int, (string,UnityEvent)> interactables = new SortedDictionary<int, (string,UnityEvent)>();

    public static int index = 0;

    void Start()
    {
        ui_text = text;
    }

    public static void onEnter(int id, string name, UnityEvent input_event)
    {
        if(!interactables.ContainsKey(id)){
            interactables.Add(id, (name,input_event));
            updateText();
        }
    }


    public static void onExit(int id)
    {
        if(interactables.ContainsKey(id)){
            interactables.Remove(id);
            updateText();
        }
    }

    
    public void Update()
    {
        //if(!Pause.is_paused){
            if (Input.GetButtonDown("Interact") && index < interactables.Count)
            {
                interactables.ElementAt(index).Value.Item2.Invoke();
            }
            if(Input.GetButtonDown("index")){
                index += (int)Input.GetAxisRaw("index");
                index = Math.Clamp(index,0,interactables.Count-1);
                Debug.Log(index);
                updateText();
            }
        //}
    }

    static void updateText()
    {
        ui_text.text = "";
        int loop_index = 0;
        foreach ((string,UnityEvent) value in interactables.Values)
        {
            if(index == loop_index){
                ui_text.text += "[ " +value.Item1 + " ]" + "\n";
            }else{
                ui_text.text += value.Item1 + "\n";
            }
            loop_index++;
        }
    }


}
