using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvController : MonoBehaviour
{
    [HideInInspector] public ItemGrid selected_item_grid = null;
    InvItem selected_item;
    RectTransform rt;
    void Update()
    {
        if(selected_item != null){
            rt.position = Input.mousePosition;
        }
        if (selected_item_grid == null) {
            if(Input.GetMouseButtonUp(0)){
                Debug.Log("goop wee");
                selected_item = null;
            }
            return;
            }
        
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)){
            Vector2Int mouse_pos = selected_item_grid.GetGridPos(Input.mousePosition);

            if(Input.GetMouseButtonDown(0) && selected_item == null){
                selected_item = selected_item_grid.PickUpItem(mouse_pos.x, mouse_pos.y);
                if(selected_item != null){
                    rt = selected_item.GetComponent<RectTransform>();
                }
            }
            if(Input.GetMouseButtonUp(0) && selected_item != null){
                selected_item_grid.PlaceItem(selected_item, mouse_pos.x, mouse_pos.y);
                selected_item = null;
            }
        }
    }
}
