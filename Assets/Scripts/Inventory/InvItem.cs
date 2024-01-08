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
    public int count;

    public Vector2Int grid_pos;
    public int list_index;
    public void Awake(){
        
    }

    internal void Set(ItemData data)
    {
        item_data = data;

        GetComponent<UnityEngine.UI.Image>().sprite = item_data.item_icon;
        Rescale(InvController.main_canvas_tile_size);
    }

    public void Rescale(float scale)
    {
        RectTransform rt = GetComponent<RectTransform>();
        float item_pixel_width = rt.sizeDelta.x;
        float item_pixel_height = rt.sizeDelta.y;
        Vector2 size = new Vector2
        {// scales item proportionally to scaled item grid with canvas and item size
            x = item_pixel_width * (scale / (item_pixel_width / item_data.width)),
            y = item_pixel_height * (scale / (item_pixel_height / item_data.height))
        };
        rt.sizeDelta = new Vector2(size.x, size.y);
    }

    public void IncreaseCount(int adding){
        count += adding;
    }
}
