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
            if(value == null){
                inv_highlighter.SetTop(true);
            }
            else{
                inv_highlighter.SetTop(false);
            }
        }
    }

    private InvItem selected_item;
    InvItem overlap_item;
    RectTransform rt_held;
    RectTransform rt_new;

    [SerializeField] List<ItemData> items;
    [SerializeField] GameObject item_prefab;
    [SerializeField] Transform canvas_transform;
    [SerializeField] ItemGrid main_grid;

    InvHighlight inv_highlighter;

    void Awake(){
        inv_highlighter = GetComponent<InvHighlight>();
    }

    void Update()
    {
        DragItemIcon();

        if(Input.GetKeyDown(KeyCode.Q) && Selected_item == null)
        {
            Selected_item = GenerateRandomItem();
            rt_held = rt_new;
            rt_new = null;
        }

        if(Input.GetKeyDown(KeyCode.W)){
            InsertItem(GenerateRandomItem());
        }

        if(selected_item_grid != null){

            HandleHighlight();

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)){

                Vector2Int mouse_pos = selected_item_grid.GetGridPos(Input.mousePosition);

                if (Input.GetMouseButtonDown(0) && Selected_item == null)
                {
                    Selected_item = selected_item_grid.PickUpItem(mouse_pos);
                    if (Selected_item != null)
                    {
                        rt_held = Selected_item.GetComponent<RectTransform>();
                    }
                }
                if (Input.GetMouseButtonUp(0) && Selected_item != null)
                {
                    if(selected_item_grid.PlaceItem(Selected_item, mouse_pos, ref overlap_item)){
                        Selected_item = null;
                    }
                    else{
                        if(overlap_item != null){
                            Debug.Log(overlap_item);
                            overlap_item = null;
                        }
                        Debug.Log("goop wee");
                    }
                }
            }
        }
        else{
            inv_highlighter.SetVisible(false);
        }
        
    }

    InvItem highlighted_item;
    Vector2Int old_pos;
    private void HandleHighlight()
    {
        Vector2Int mouse_grid_pos = selected_item_grid.GetGridPos(Input.mousePosition);
        if(old_pos == mouse_grid_pos){ return; }
        old_pos = mouse_grid_pos;
        
        if(Selected_item == null){
            highlighted_item = selected_item_grid.GetItem(mouse_grid_pos);
            if(highlighted_item != null){
                inv_highlighter.SetTop(true);
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
            inv_highlighter.SetTop(false);
            inv_highlighter.SetVisible(selected_item_grid.BoundsCheck(mouse_grid_pos, Selected_item.item_data.width, Selected_item.item_data.height));
            inv_highlighter.SetSize(Selected_item);
            inv_highlighter.SetParent(selected_item_grid);
            inv_highlighter.SetPosition(selected_item_grid, Selected_item, mouse_grid_pos);
        }
    }

    private InvItem GenerateRandomItem()
    {
        InvItem inv_item = Instantiate(item_prefab).GetComponent<InvItem>();
        rt_new = inv_item.GetComponent<RectTransform>();
        rt_new.SetParent(canvas_transform);

        int selected_item_ID = Random.Range(0, items.Count);
        inv_item.Set(items[selected_item_ID]);
        return inv_item;
    }

    private void InsertItem(InvItem inserting_item){
        Vector2Int? open_pos = main_grid.FindSpace(inserting_item);
        if(open_pos != null){
            main_grid.PlaceItem(inserting_item, open_pos.Value);
        }
        else{
            Destroy(inserting_item.gameObject);
        }
    }

    private void DragItemIcon()
    {
        if (Selected_item != null)
        {
            rt_held.position = Input.mousePosition;
        }
    }
}
