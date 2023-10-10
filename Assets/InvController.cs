using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvController : MonoBehaviour
{
    [HideInInspector] public ItemGrid selected_item_grid = null;

    void Update()
    {
        if (selected_item_grid == null) {return;}

        if(Input.GetMouseButtonDown(0)){
            Debug.Log(selected_item_grid.GetGridPos(Input.mousePosition));
        }
    }
}
