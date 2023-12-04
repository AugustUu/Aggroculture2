using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class GridInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InvController inv_controller;
    public ItemGrid item_grid;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("yo");
        inv_controller.Selected_item_grid = item_grid;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inv_controller.Selected_item_grid = null;
    }
}
