using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class InvController : MonoBehaviour
{
    public ItemGrid selected_item_grid;

    public InvItem Selected_item {
        get => selected_item; 
        set {
            selected_item = value; 
            if(selected_item_grid != null){
                HandleHighlight(false);
            }
        }
    }

    private InvItem selected_item;
    public static InvItem equipped_item;

    private Vector3 drag_offset;
    private Vector2Int tile_offset;
    InvItem overlap_item;
    RectTransform rt_held;
    RectTransform rt_new;
    [SerializeField] ItemGrid origin_grid;
    Vector2Int origin_pos;

    [SerializeField] List<ItemData> items;
    [SerializeField] GameObject item_prefab;
    [SerializeField] Transform canvas_transform;
    [SerializeField] ItemGrid main_grid;
    [SerializeField] GameObject inv_parent;
    [SerializeField] InvHighlight inv_highlighter;
    [SerializeField] InvHighlight equip_highlighter;

    public static float main_canvas_tile_size;
    public static float main_scaled_tile_size;

    void Update()
    {
        DragItemIcon();

        if (Input.GetKeyDown(KeyCode.O)){
            //InsertItem(GenerateItem(Random.Range(0, items.Count)));
            for (int i = 0; i < items.Count; i++)
            {
                InsertItem(GenerateItem(i));
            }
        }


        if(selected_item_grid != null){

            HandleHighlight(true);

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)){

                Vector2Int mouse_pos = selected_item_grid.GetGridPos(Input.mousePosition);
                

                if (Input.GetMouseButtonDown(0) && selected_item == null)
                {
                    if(selected_item_grid.GetItem(mouse_pos) != equipped_item){
                        Selected_item = selected_item_grid.PickUpItem(mouse_pos);
                        if (selected_item != null)
                        {
                            rt_held = selected_item.GetComponent<RectTransform>();
                            
                            drag_offset = rt_held.position - Input.mousePosition;
                            
                            tile_offset = selected_item.grid_pos - mouse_pos;
                            
                            origin_pos = selected_item.grid_pos;

                            rt_held.SetParent(inv_parent.transform);
                            origin_grid = selected_item_grid;
                        }
                    }
                    
                }
                if (Input.GetMouseButtonUp(0) && selected_item != null)
                {
                    if(selected_item_grid.PlaceItem(selected_item, mouse_pos + tile_offset, ref overlap_item)){
                        tile_offset = Vector2Int.zero;
                        selected_item.back_highlighter.SetSize(selected_item, selected_item_grid);
                        selected_item.back_highlighter.SetParent(selected_item_grid);
                        selected_item.back_highlighter.SetSibling(false);
                        selected_item.back_highlighter.SetPosition(selected_item_grid, highlighted_item);
                        selected_item.back_highlighter.SetScale(1 / selected_item_grid.gameObject.GetComponent<RectTransform>().localScale.x); // I DONT KNOW WHY THIS WORKS
                        selected_item.back_highlighter.SetVisible(true);
                    }
                    else{
                        if(overlap_item != null){
                            Debug.Log(overlap_item);
                            overlap_item = null;
                        }
                        Debug.Log("item dropped on other item");
                        ReturnItem();
                    }
                    
                    Selected_item = null;
                }
            }
            if (Input.GetMouseButtonDown(1) && selected_item == null){
                EquipItem();
            }
        }
        else{
            inv_highlighter.SetVisible(false);
            if (Input.GetMouseButtonUp(0) && selected_item != null){
                ReturnItem();
                Selected_item = null;
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
            if(highlighted_item != null && highlighted_item != equipped_item){
                inv_highlighter.SetSize(highlighted_item, selected_item_grid);
                inv_highlighter.SetParent(selected_item_grid);
                inv_highlighter.SetSibling(true);
                inv_highlighter.SetPosition(selected_item_grid, highlighted_item);
                inv_highlighter.SetVisible(true);
            }
            else{
                inv_highlighter.SetVisible(false);
            }
        }
        else{
            inv_highlighter.SetSize(selected_item, selected_item_grid);
            inv_highlighter.SetParent(selected_item_grid);
            inv_highlighter.SetSibling(true);
            inv_highlighter.SetPosition(selected_item_grid, selected_item, mouse_grid_pos);
            inv_highlighter.SetVisible(selected_item_grid.BoundsCheck(mouse_grid_pos, selected_item.item_data.width, selected_item.item_data.height));
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

    private bool InsertItem(InvItem inserting_item){
        Vector2Int? open_pos = main_grid.FindSpace(inserting_item);
        if(open_pos != null){
            main_grid.PlaceItem(inserting_item, open_pos.Value);
            HandleBackHighlight(inserting_item, main_grid);
            inserting_item.Rescale(main_canvas_tile_size);
            return true;
        }
        else{
            Destroy(inserting_item.gameObject);
            return false;
        }
    }

    public bool InsertItemID(int item_ID){
        return InsertItem(GenerateItem(item_ID));
    }

    public void InsertRandomItem(){
        int rand_index = UnityEngine.Random.Range(0, items.Count);
        InsertItemID(rand_index);
        Debug.Log(items[rand_index].item_type);
    }

    private void DragItemIcon()
    {
        if (selected_item != null)
        {
            rt_held.position = Input.mousePosition + drag_offset;
        }
    }

    private void ReturnItem()
    {
        origin_grid.PlaceItem(selected_item, origin_pos);
        HandleBackHighlight(selected_item, origin_grid);
        tile_offset = Vector2Int.zero;
    }

    private void HandleBackHighlight(InvItem selected_item, ItemGrid grid){
        selected_item.back_highlighter.SetSize(selected_item, grid);
        selected_item.back_highlighter.SetParent(grid);
        selected_item.back_highlighter.SetSibling(false);
        selected_item.back_highlighter.SetPosition(grid, selected_item);
        selected_item.back_highlighter.SetScale(1 / grid.gameObject.GetComponent<RectTransform>().localScale.x); // I DONT KNOW WHY THIS WORKS
        selected_item.back_highlighter.SetVisible(true);
    }

    InvItem to_equip_item;
    private void EquipItem(){
        Vector2Int mouse_grid_pos = selected_item_grid.GetGridPos(Input.mousePosition) + tile_offset;
        to_equip_item = selected_item_grid.GetItem(mouse_grid_pos);
        if(to_equip_item != null){
            if(to_equip_item.item_data.equippable){
                if(to_equip_item == equipped_item){
                    equip_highlighter.SetVisible(false);
                    equipped_item = null;
                    HandleHighlight(false);
                    Debug.Log("item unequipped");
                }
                else if(to_equip_item != null){
                    equipped_item = to_equip_item;
                    equip_highlighter.SetSize(equipped_item, selected_item_grid);
                    equip_highlighter.SetParent(selected_item_grid);
                    equip_highlighter.SetSibling(true);
                    equip_highlighter.SetPosition(selected_item_grid, equipped_item);
                    equip_highlighter.SetVisible(true);
                    inv_highlighter.SetVisible(false);
                    Debug.Log("item equipped");
                }
            }
            else if(to_equip_item.item_data.item_type == ItemType.Food){
                selected_item_grid.PickUpItem(to_equip_item.grid_pos);
                HealthSystem.changeHealth(to_equip_item.item_data.food_heal_amount);
                Destroy(to_equip_item.gameObject);
                inv_highlighter.SetVisible(false);
                Debug.Log("food eaten");
            }
        }
    }
}
