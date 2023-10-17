using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class InvItem : MonoBehaviour
{
    public ItemData item_data;
    
    public int grid_pos_x;
    public int grid_pos_y;
    public void Awake(){
        
    }

    internal void Set(ItemData data)
    {
        item_data = data;

        GetComponent<UnityEngine.UI.Image>().sprite = item_data.item_icon;

        RectTransform rt = GetComponent<RectTransform>();
        float item_pixel_width = rt.sizeDelta.x;
        float item_pixel_height = rt.sizeDelta.y;
        Vector2 size = new Vector2
        {// scales item proportionally to scaled item grid with canvas and item size
            x = item_pixel_width * (ItemGrid.canvas_tile_size / (item_pixel_width / item_data.width)),
            y = item_pixel_height * (ItemGrid.canvas_tile_size / (item_pixel_height / item_data.height))
        };
        rt.sizeDelta = new Vector2(size.x, size.y);
    }
}
