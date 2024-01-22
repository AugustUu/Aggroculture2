using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class InvController : MonoBehaviour
{
    public ItemGrid selected_item_grid;
    public ItemGrid Selected_item_grid { 
        get => selected_item_grid; 
        set {
            selected_item_grid = value;
            inv_highlighter.SetParent(0, selected_item_grid);
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
    public InvItem equipped_item;

    private Vector3 drag_offset;
    private Vector2Int tile_offset;
    InvItem overlap_item;
    RectTransform rt_held;
    RectTransform rt_new;
    ItemGrid origin_grid;
    Vector2Int origin_pos;

    [SerializeField] List<ItemData> items;
    [SerializeField] GameObject item_prefab;
    [SerializeField] Transform canvas_transform;
    [SerializeField] ItemGrid main_grid;
    [SerializeField] GameObject inv_parent;

    public static float main_canvas_tile_size;
    public static float main_scaled_tile_size;

    InvHighlight inv_highlighter;

    void Awake(){
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

                        float offset_scale = main_canvas_tile_size / selected_item_grid.canvas_tile_size;
                        drag_offset = (rt_held.position - Input.mousePosition) * offset_scale;
                        
                        tile_offset = selected_item.grid_pos - mouse_pos;
                        
                        origin_pos = selected_item.grid_pos;

                        rt_held.SetParent(inv_parent.transform);
                        if(equipped_item == selected_item){
                            inv_highlighter.SetParent(1, inv_parent.transform);
                        }
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
                        }
                        Debug.Log("item dropped on other item");
                        ReturnItem();
                    }
                    if(equipped_item != null){
                        // inv_highlighter.SetPosition(1, selected_item_grid, equipped_item);
                    }
                }
            }
            if (Input.GetMouseButtonDown(1) && selected_item == null){
                EquipItem();
            }
        }
        else{
            inv_highlighter.SetVisible(0, false);
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
                inv_highlighter.SetSize(0, highlighted_item, selected_item_grid);
                inv_highlighter.SetParent(0, selected_item_grid);
                inv_highlighter.SetPosition(0, selected_item_grid, highlighted_item);
                inv_highlighter.SetVisible(0, true);
            }
            else{
                inv_highlighter.SetVisible(0, false);
            }
        }
        else{
            inv_highlighter.SetSize(0, selected_item, selected_item_grid);
            inv_highlighter.SetParent(0, selected_item_grid);
            inv_highlighter.SetPosition(0, selected_item_grid, selected_item, mouse_grid_pos);
            inv_highlighter.SetVisible(0, selected_item_grid.BoundsCheck(mouse_grid_pos, selected_item.item_data.width, selected_item.item_data.height));
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
            rt_held.position = Input.mousePosition + drag_offset;
            Debug.Log(rt_held.position);
            if(equipped_item == selected_item){
                inv_highlighter.SetPositionRaw(1, Input.mousePosition + drag_offset);
            }
        }
    }

    private void ReturnItem()
    {
        origin_grid.PlaceItem(selected_item, origin_pos);
        tile_offset = Vector2Int.zero;
        Selected_item = null;
    }

    InvItem to_equip_item;
    private void EquipItem(){
        Vector2Int mouse_grid_pos = selected_item_grid.GetGridPos(Input.mousePosition) + tile_offset;
        to_equip_item = selected_item_grid.GetItem(mouse_grid_pos);
        if(to_equip_item == equipped_item){
            inv_highlighter.SetVisible(1, false);
            equipped_item = null;
            Debug.Log("item unequipped");
        }
        else if(to_equip_item != null){
            equipped_item = to_equip_item;
            inv_highlighter.SetSize(1, equipped_item, selected_item_grid);
            inv_highlighter.SetParent(1, selected_item_grid);
            inv_highlighter.SetPosition(1, selected_item_grid, equipped_item);
            inv_highlighter.SetVisible(1, true);
            Debug.Log("item equipped");
        }
    }
}
