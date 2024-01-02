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
            HandleHighlight(false);
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

    [SerializeField] List<ItemData> items;
    [SerializeField] GameObject item_prefab;
    [SerializeField] Transform canvas_transform;
    [SerializeField] ItemGrid main_grid;
    [SerializeField] GameObject inv_parent;

    InvHighlight inv_highlighter;

    void Awake(){
        inv_highlighter = GetComponent<InvHighlight>();
    }

    void Update()
    {
        DragItemIcon();

        if(Input.GetKeyDown(KeyCode.Q) && Selected_item == null)
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

                if (Input.GetMouseButtonDown(0) && Selected_item == null)
                {

                    Selected_item = selected_item_grid.PickUpItem(mouse_pos);
                    if (Selected_item != null)
                    {
                        rt_held = Selected_item.GetComponent<RectTransform>();
                        drag_offset = Input.mousePosition - rt_held.position;
                        
                        tile_offset = selected_item.grid_pos - mouse_pos;
                        
                        origin_pos = selected_item.grid_pos;

                        rt_held.SetParent(inv_parent.transform);
                        origin_grid = selected_item_grid;
                    }
                }
                if (Input.GetMouseButtonUp(0) && Selected_item != null)
                {
                    if(selected_item_grid.PlaceItem(Selected_item, mouse_pos + tile_offset, ref overlap_item)){
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
                }
            }
        }
        else{
            inv_highlighter.SetVisible(false);
            if (Input.GetMouseButtonUp(0) && Selected_item != null){
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
        
        if(Selected_item == null){
            highlighted_item = selected_item_grid.GetItem(mouse_grid_pos);
            if(highlighted_item != null){
                inv_highlighter.SetVisible(true);
                inv_highlighter.SetSize(highlighted_item);
                inv_highlighter.SetParent(selected_item_grid);
                inv_highlighter.SetPosition(selected_item_grid, highlighted_item);
            }
            else{
                inv_highlighter.SetVisible(false);
            }
        }
        else{
            inv_highlighter.SetVisible(selected_item_grid.BoundsCheck(mouse_grid_pos, Selected_item.item_data.width, Selected_item.item_data.height));
            inv_highlighter.SetSize(Selected_item);
            inv_highlighter.SetParent(selected_item_grid);
            inv_highlighter.SetPosition(selected_item_grid, Selected_item, mouse_grid_pos);
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
        if (Selected_item != null)
        {
            rt_held.position = Input.mousePosition - drag_offset;
        }
    }

    private void ReturnItem()
    {
        origin_grid.PlaceItem(Selected_item, origin_pos);
        Selected_item = null;
        tile_offset = Vector2Int.zero;
    }
}
