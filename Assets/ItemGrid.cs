using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemGrid : MonoBehaviour
{
    RectTransform rect_transform;
    private void Start(){
        rect_transform = GetComponent<RectTransform>();
    }
    public float tile_size_width = 24;
    public float tile_size_height = 24;

    Vector2 grid_pos = new Vector2();
    Vector2Int tile_grid_pos = new Vector2Int();
    
    public Vector2Int GetGridPos(Vector2 mouse_pos){
        grid_pos.x = mouse_pos.x - rect_transform.position.x;
        grid_pos.y = rect_transform.position.y - mouse_pos.y;
        tile_grid_pos.x = (int) (grid_pos.x / tile_size_width);
        tile_grid_pos.y = (int) (grid_pos.y / tile_size_width);
        return tile_grid_pos;
    }
}
