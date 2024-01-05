using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class InvController : MonoBehaviour
{
    [HideInInspector] private ItemGrid selected_item_grid;
    public ItemGrid Selected_item_grid { 
        get => selected_item_grid; 
        set {
            selected_item_grid = value;
            inv_highlighter.SetParent(selected_item_grid);
        }
    }

    public InvItem Selected_item {
        get => selected_item; 
        set {
            selected_item = value; 
            if(Selected_item_grid != null){
                HandleHighlight(false);
            }
        }
    }

    private InvItem selected_item;

    private Vector3 drag_offset;
    private Vector2Int tile_offset;
    InvItem overlap_item;
    RectTransform rt_held;
    RectTransform rt_new;
    ItemGrid origin_grid;
    Vector2Int origin_pos;
    public static float main_scaled_tile_size;
    public static float main_canvas_tile_size;

    [SerializeField] List<ItemData> items;
    [SerializeField] GameObject item_prefab;
    [SerializeField] Transform canvas_transform;
    [SerializeField] ItemGrid main_grid;
    [SerializeField] GameObject inv_parent;

    InvHighlight inv_highlighter;

    void Start(){
        inv_highlighter = GetComponent<InvHighlight>();
    }

    void Update()
    {
        DragItemIcon();

        if(Input.GetKeyDown(KeyCode.Q) && selected_item == null)
        {
            Selected_item = GenerateItem(Random.Range(0, items.Count));
            rt_held = rt_new;
            rt_new = null;
        }

        if(Input.GetKeyDown(KeyCode.W)){
            InsertItem(GenerateItem(Random.Range(0, items.Count)));
        }

        if(selected_item_grid != null){

            HandleHighlight(true);

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)){

                Vector2Int mouse_pos = selected_item_grid.GetGridPos(Input.mousePosition);

                if (Input.GetMouseButtonDown(0) && selected_item == null)
                {

                    Selected_item = selected_item_grid.PickUpItem(mouse_pos);
                    if (selected_item != null)
                    {
                        selected_item.Rescale(main_canvas_tile_size);
                        rt_held = selected_item.GetComponent<RectTransform>();

                        tile_offset = selected_item.grid_pos - mouse_pos;
                        float offset_scale = selected_item_grid.canvas_tile_size - main_canvas_tile_size;
                        Vector2 offset_tiles = new Vector2(-tile_offset.x - ((selected_item.item_data.width / 2.0f) - 0.5f), -(-tile_offset.y - ((selected_item.item_data.height / 2.0f) - 0.5f))); // LOL this line sucks so bad
                        
                        Vector2 scale_offset = new Vector2(offset_tiles.x * offset_scale, offset_tiles.y * offset_scale);

                        drag_offset = Input.mousePosition - rt_held.position;
                        drag_offset.x -= scale_offset.x;
                        drag_offset.y -= scale_offset.y;
                        
                        origin_pos = selected_item.grid_pos;

                        rt_held.SetParent(inv_parent.transform);
                        origin_grid = selected_item_grid;
                    }
                }
                if (Input.GetMouseButtonUp(0) && selected_item != null)
                {
                    if(selected_item_grid.PlaceItem(selected_item, mouse_pos + tile_offset, ref overlap_item)){
                        Selected_item = null;
                        tile_offset = Vector2Int.zero;
                    }
                    else{
                        if(overlap_item != null){
                            Debug.Log(overlap_item);
                            overlap_item = null;
                            Debug.Log("item dropped on other item");
                        }
                        ReturnItem();
                    }
                }
            }
        }
        else{
            inv_highlighter.SetVisible(false);
            if (Input.GetMouseButtonUp(0) && selected_item != null){
                ReturnItem();
                Debug.Log("item dropped outside inv");
            }
        }
        
    }

    InvItem highlighted_item;
    Vector2Int old_pos;
    private void HandleHighlight(bool check_mouse)
    {
        Vector2Int mouse_grid_pos = selected_item_grid.GetGridPos(Input.mousePosition) + tile_offset;
        if(check_mouse){
            if(old_pos == mouse_grid_pos){ return; }
        }
        old_pos = mouse_grid_pos;
        
        if(selected_item == null){
            highlighted_item = selected_item_grid.GetItem(mouse_grid_pos);
            if(highlighted_item != null){
                inv_highlighter.SetVisible(true);
                inv_highlighter.SetSize(highlighted_item, selected_item_grid);
                inv_highlighter.SetParent(selected_item_grid);
                inv_highlighter.SetPosition(selected_item_grid, highlighted_item);
            }
            else{
                inv_highlighter.SetVisible(false);
            }
        }
        else{
            inv_highlighter.SetVisible(selected_item_grid.BoundsCheck(mouse_grid_pos, selected_item.item_data.width, selected_item.item_data.height));
            inv_highlighter.SetSize(selected_item, selected_item_grid);
            inv_highlighter.SetParent(selected_item_grid);
            inv_highlighter.SetPosition(selected_item_grid, selected_item, mouse_grid_pos);
        }
    }
    
    private InvItem GenerateItem(int item_ID)
    {
        InvItem inv_item = Instantiate(item_prefab).GetComponent<InvItem>();
        rt_new = inv_item.GetComponent<RectTransform>();
        rt_new.SetParent(canvas_transform);

        inv_item.Set(items[item_ID]);
        return inv_item;
    }

    private void InsertItem(InvItem inserting_item){
        Vector2Int? open_pos = main_grid.FindSpace(inserting_item);
        if(open_pos != null){
            main_grid.PlaceItem(inserting_item, open_pos.Value);
        }
        else{
            Destroy(inserting_item.gameObject);
            Debug.Log("found no space for inserting item, debug destroying item");
        }
    }

    public void InsertItemID(int item_ID){
        InsertItem(GenerateItem(item_ID));
    }

    private void DragItemIcon()
    {
        if (selected_item != null)
        {
            rt_held.position = Input.mousePosition - drag_offset;
        }
    }

    private void ReturnItem()
    {
        origin_grid.PlaceItem(selected_item, origin_pos);
        tile_offset = Vector2Int.zero;
        Selected_item = null;
    }
}
