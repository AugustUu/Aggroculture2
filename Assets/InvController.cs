using System.Collections;
using System.Collections.Generic;
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

    InvItem selected_item;
    InvItem overlap_item;
    RectTransform rt;

    [SerializeField] List<ItemData> items;
    [SerializeField] GameObject item_prefab;
    [SerializeField] Transform canvas_transform;

    InvHighlight inv_highlighter;

    void Awake(){
        inv_highlighter = GetComponent<InvHighlight>();
    }

    void Update()
    {
        DragItemIcon();

        if(Input.GetKeyDown(KeyCode.Q))
        {
            selected_item = GenerateRandomItem();
        }

        if(Input.GetKeyDown(KeyCode.W)){
            //insert random item
        }

        if(selected_item_grid != null){

            HandleHighlight();

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)){

                Vector2Int mouse_pos = selected_item_grid.GetGridPos(Input.mousePosition);

                if (Input.GetMouseButtonDown(0) && selected_item == null)
                {
                    selected_item = selected_item_grid.PickUpItem(mouse_pos);
                    if (selected_item != null)
                    {
                        rt = selected_item.GetComponent<RectTransform>();
                    }
                }
                if (Input.GetMouseButtonUp(0) && selected_item != null)
                {
                    if(selected_item_grid.PlaceItem(selected_item, mouse_pos, ref overlap_item)){
                        selected_item = null;
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
        
        if(selected_item == null){
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
            inv_highlighter.SetVisible(selected_item_grid.BoundsCheck(mouse_grid_pos, selected_item.item_data.width, selected_item.item_data.height));
            inv_highlighter.SetSize(selected_item);
            inv_highlighter.SetParent(selected_item_grid);
            inv_highlighter.SetPosition(selected_item_grid, selected_item, mouse_grid_pos);
        }
    }

    private InvItem GenerateRandomItem()
    {
        InvItem inv_item = Instantiate(item_prefab).GetComponent<InvItem>();
        rt = inv_item.GetComponent<RectTransform>();
        rt.SetParent(canvas_transform);

        int selected_item_ID = Random.Range(0, items.Count);
        inv_item.Set(items[selected_item_ID]);
        return inv_item;
    }

    private void DragItemIcon()
    {
        if (selected_item != null)
        {
            rt.position = Input.mousePosition;
        }
    }
}
