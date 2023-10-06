using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvController : MonoBehaviour
{
    [SerializeField] public ItemGrid selected_item_grid;

    void Update()
    {
        if (selected_item_grid == null) {return;}

        Debug.Log(selected_item_grid.GetGridPos(Input.mousePosition));
    }
}
