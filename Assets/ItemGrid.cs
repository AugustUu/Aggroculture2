using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemGrid : MonoBehaviour
{
    [SerializeField] ItemGrid selected_item_grid;
    [SerializeField] public int grid_width = 10;
    [SerializeField] public int grid_height = 15;

    [SerializeField] GameObject inv_item_prefab;

    RectTransform rect_transform;
    private void Start(){
        scaled_tile_size = local_tile_size * transform.localScale.x;
        rect_transform = GetComponent<RectTransform>();
        InvInit(grid_width, grid_height);
        InvItem inv_item = Instantiate(inv_item_prefab).GetComponent<InvItem>();
        PlaceItem(inv_item, 3, 2);
    }

    private void InvInit(int width, int height){
        inv_item_slot = new InvItem[width, height];
        Vector2 size = new Vector2(width * local_tile_size, height * local_tile_size);
        rect_transform.sizeDelta = size;
    }
    public static float local_tile_size = 16;
    public static float scaled_tile_size;

    InvItem[,] inv_item_slot;

    Vector2 grid_pos = new Vector2();
    Vector2Int tile_grid_pos = new Vector2Int();
    
    public Vector2Int GetGridPos(Vector2 mouse_pos){
        grid_pos.x = mouse_pos.x - rect_transform.position.x;
        grid_pos.y = rect_transform.position.y - mouse_pos.y;
        tile_grid_pos.x = Mathf.Clamp((int) (grid_pos.x / scaled_tile_size), 0, grid_width - 1);
        tile_grid_pos.y = Mathf.Clamp((int) (grid_pos.y / scaled_tile_size), 0, grid_height - 1);
        return tile_grid_pos;
    }

    public void PlaceItem(InvItem inv_item, int pos_x, int pos_y){
        RectTransform item_rect_transform = inv_item.GetComponent<RectTransform>();
        item_rect_transform.SetParent(rect_transform)   ;
        inv_item_slot[pos_x, pos_y] = inv_item;

        Vector2 item_position = new Vector2();
        item_position.x = pos_x * local_tile_size + local_tile_size / 2;
        item_position.y = -(pos_y * local_tile_size + local_tile_size / 2);

        item_rect_transform.localPosition = item_position;
    }
}
