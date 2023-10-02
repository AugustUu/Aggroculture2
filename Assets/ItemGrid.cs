using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemGrid : MonoBehaviour
{
    RectTransform rect_transform;
    private void Start(){
        rect_transform = GetComponent<RectTransform>();
    }

    const float tile_size_width = 16;
    const float tile_size_height = 16;

    Vector2 grid_pos = new Vector2();

    /*public Vector2Int GetGridPos(Vector2 mouse_pos){
        grid_pos.x = mouse_pos.x - rect_transform.position.y
    }*/
}
