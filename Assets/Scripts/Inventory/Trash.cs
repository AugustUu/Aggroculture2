using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Trash : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InvController inv_controller;
    public Sprite closed_sprite;
    public Sprite open_sprite;

    public void OnPointerEnter(PointerEventData eventData)
    {        
        inv_controller.trashing = true;
        if(inv_controller.Selected_item != null){
            GetComponent<UnityEngine.UI.Image>().sprite = open_sprite;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inv_controller.trashing = false;
        GetComponent<UnityEngine.UI.Image>().sprite = closed_sprite;
    }

    public void Close(){
        GetComponent<UnityEngine.UI.Image>().sprite = closed_sprite;
    }
}
