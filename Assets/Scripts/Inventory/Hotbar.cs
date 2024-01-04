using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hotbar : MonoBehaviour
{
    [SerializeField] ItemGrid selected_item_grid;
    [SerializeField] public int grid_width = 10;
    [SerializeField] public int grid_height = 1;
    [SerializeField] public GameObject canvas;


    RectTransform rect_transform;
    private void Start(){
        scaled_tile_size = local_tile_size * transform.localScale.y;
        Debug.Log(scaled_tile_size);
        canvas_scale = canvas.transform.localScale.y;
        canvas_tile_size =  scaled_tile_size * canvas_scale;
        rect_transform = GetComponent<RectTransform>();
        InvInit(grid_width, grid_height);
    }

    private void Update(){
        if(canvas_scale != canvas.transform.localScale.y){
            canvas_scale = canvas.transform.localScale.y;
            canvas_tile_size = scaled_tile_size * canvas_scale;
        }

    }

    private void InvInit(int width, int height){
        inventory = new InvItem[width, height];
        Vector2 size = new Vector2(width * local_tile_size, height * local_tile_size);
        rect_transform.sizeDelta = size;
    }
    [SerializeField] public static float local_tile_size = 32;
    public static float scaled_tile_size;
    public float canvas_scale;
    public static float canvas_tile_size;

    InvItem[,] inventory;
    List<InvItem> item_list = new List<InvItem>();
    int last_index = 0;

    Vector2 grid_pos = new Vector2();
    Vector2Int tile_grid_pos = new Vector2Int();
    
    public Vector2Int GetGridPos(Vector2 mouse_pos){
        grid_pos.x = mouse_pos.x - rect_transform.position.x;
        grid_pos.y = rect_transform.position.y - mouse_pos.y;
        tile_grid_pos.x = Mathf.Clamp((int) (grid_pos.x / canvas_tile_size), 0, grid_width - 1);
        tile_grid_pos.y = Mathf.Clamp((int) (grid_pos.y / canvas_tile_size), 0, grid_height - 1);
        return tile_grid_pos;
    }

    public InvItem PickUpItem(Vector2Int mouse_pos){
        InvItem picked_up_item = inventory[mouse_pos.x, mouse_pos.y];
        if(picked_up_item != null){
            for(int x = 0; x < picked_up_item.item_data.width; x++){
                for(int y = 0; y < picked_up_item.item_data.height; y++){
                    inventory[picked_up_item.grid_pos.x + x, picked_up_item.grid_pos.y + y] = null;
                }
            }
        }
        return picked_up_item;
    }

    public bool PlaceItem(InvItem inv_item, Vector2Int mouse_grid_pos, ref InvItem overlap_item)
    {
        if (!BoundsCheck(mouse_grid_pos, inv_item.item_data.width, inv_item.item_data.height)) { return false; }

        if (!OverlapCheck(mouse_grid_pos, inv_item.item_data.width, inv_item.item_data.height, ref overlap_item)) { return false; }

        PlaceItem(inv_item, mouse_grid_pos);

        foreach(InvItem i in inventory){
            if(i != null){
                Debug.Log(i.item_data.name);
            }
        }

        return true;
    }

    public void PlaceItem(InvItem inv_item, Vector2Int mouse_grid_pos)
    {
        RectTransform item_rect_transform = inv_item.GetComponent<RectTransform>();
        item_rect_transform.SetParent(rect_transform);

        for (int x = 0; x < inv_item.item_data.width; x++)
        {
            for (int y = 0; y < inv_item.item_data.height; y++)
            {
                inventory[mouse_grid_pos.x + x, mouse_grid_pos.y + y] = inv_item;
                item_list.Add(inv_item);
            }
        }

        inv_item.grid_pos = mouse_grid_pos;
        Vector2 item_position = GetItemPos(inv_item, mouse_grid_pos);

        item_rect_transform.localPosition = item_position;
    }

    public Vector2 GetItemPos(InvItem inv_item, Vector2Int mouse_pos)
    {
        Vector2 item_position = new Vector2
        {
            x = mouse_pos.x * local_tile_size + inv_item.item_data.width * local_tile_size / 2,
            y = -(mouse_pos.y * local_tile_size + inv_item.item_data.height * local_tile_size / 2)
        };
        return item_position;
    }

    public InvItem GetItem(Vector2Int pos){
        return inventory[pos.x, pos.y];
    }

    public bool BoundsCheck(Vector2Int pos, int width, int height){
        if(pos.x < 0 || pos.y < 0 || pos.x + width - 1 >= grid_width || pos.y + height - 1 >= grid_height){
            return false;
        }
        return true;
    }

    public bool OverlapCheck(Vector2Int pos, int width, int height, ref InvItem overlap_item){
        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                if(inventory[pos.x + x, pos.y + y] != null){
                    overlap_item = inventory[pos.x + x, pos.y + y];
                    return false;
                }
            }
        }
        return true;
    }

    public bool SpaceCheck(int pos_x, int pos_y, int width, int height){
        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                if(inventory[pos_x + x, pos_y + y] != null){
                    return false;
                }
            }
        }
        return true;
    }

    public Vector2Int? FindSpace(InvItem inserting_item){
        int height = grid_height - inserting_item.item_data.height + 1;
        int width = grid_width - inserting_item.item_data.width + 1;

        for(int y = 0; y < height; y++){
            for(int x = 0; x < width; x++){
                if(SpaceCheck(x, y, inserting_item.item_data.width, inserting_item.item_data.height)){
                    return new Vector2Int(x, y);
                }
            }
        }
        return null;
    }
}
