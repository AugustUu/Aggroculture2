using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class InvItem : MonoBehaviour
{
    [SerializeField]
    public ItemGrid main_grid;
    public ItemData item_data;
    public int count;

    public Vector2Int grid_pos;
    public int list_index;
    private Vector2 sprite_size;

    internal void Set(ItemData data)
    {
        item_data = data;

        GetComponent<UnityEngine.UI.Image>().sprite = item_data.item_icon;

        RectTransform rt = GetComponent<RectTransform>(); // idk how to not get the rt twice haha oh well
        sprite_size = rt.sizeDelta;
        Rescale(InvController.main_canvas_tile_size);

    }

    public void IncreaseCount(int adding){
        count += adding;
    }

    public void Rescale(float scale){
        RectTransform rt = GetComponent<RectTransform>();
        Vector2 size = new Vector2
        {
            x = sprite_size.x * (scale / (sprite_size.x / item_data.width)),
            y = sprite_size.y * (scale / (sprite_size.y / item_data.height))
        };
        rt.sizeDelta = size;
    }
}