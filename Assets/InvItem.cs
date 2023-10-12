using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class InvItem : MonoBehaviour
{
    public int item_width = 1;
    public int item_height = 1;
    
    public void Awake(){
        RectTransform rt = GetComponent<RectTransform>();
        float item_pixel_width = rt.sizeDelta.x;
        float item_pixel_height = rt.sizeDelta.y;
        rt.sizeDelta = new Vector2(item_pixel_width * (ItemGrid.canvas_tile_size / (item_pixel_width / item_width)), item_pixel_height * (ItemGrid.canvas_tile_size / (item_pixel_height / item_height)));
    }
}
