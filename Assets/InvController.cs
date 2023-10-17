using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvController : MonoBehaviour
{
    [HideInInspector] public ItemGrid selected_item_grid = null;
    InvItem selected_item;
    InvItem overlap_item;
    RectTransform rt;

    [SerializeField] List<ItemData> items;
    [SerializeField] GameObject item_prefab;
    [SerializeField] Transform canvas_transform;
    void Update()
    {
        DragItemIcon();

        if(Input.GetKeyDown(KeyCode.Q))
        {
            GenerateRandomItem();
        }

        if(selected_item_grid != null){
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
                    if(selected_item_grid.PlaceItem(selected_item, mouse_pos)){
                        selected_item = null;
                    }
                }
            }
        }
        
    }

    private void GenerateRandomItem()
    {
        InvItem inv_item = Instantiate(item_prefab).GetComponent<InvItem>();
        selected_item = inv_item;
        rt = inv_item.GetComponent<RectTransform>();
        rt.SetParent(canvas_transform);

        int selected_item_ID = Random.Range(0, items.Count);
        inv_item.Set(items[selected_item_ID]);
    }

    private void DragItemIcon()
    {
        if (selected_item != null)
        {
            rt.position = Input.mousePosition;
        }
    }
}
