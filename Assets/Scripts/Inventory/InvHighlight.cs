using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvHighlight : MonoBehaviour
{
    [SerializeField] RectTransform highlighter;


    
    public void SetVisible(bool b)
    {
        highlighter.gameObject.SetActive(b);
    }

    public void SetSize(InvItem held_item, ItemGrid selected_item_grid)
    {
        highlighter.sizeDelta = new Vector2(
            held_item.item_data.width * selected_item_grid.scaled_tile_size, 
            held_item.item_data.height * selected_item_grid.scaled_tile_size
        );
    }

    public void SetPosition(ItemGrid target_grid, InvItem held_item)
    {
        highlighter.localPosition = target_grid.GetItemPos(held_item, held_item.grid_pos);
    }

    public void SetPosition(ItemGrid target_grid, InvItem held_item, Vector2Int mouse_grid_pos)
    {
        highlighter.localPosition = target_grid.GetItemPos(held_item, mouse_grid_pos);
    }

    public void SetParent(ItemGrid target_grid)
    {
        if(target_grid == null){ return; }
        highlighter.SetParent(target_grid.GetComponent<RectTransform>());
        highlighter.SetAsLastSibling();
    }
}
