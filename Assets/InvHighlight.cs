using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvHighlight : MonoBehaviour
{
    [SerializeField] RectTransform highlighter;

    public void SetVisible(bool b){
        highlighter.gameObject.SetActive(b);
    }

    public void SetSize(InvItem held_item){
        highlighter.sizeDelta = new Vector2(
            held_item.item_data.width * ItemGrid.scaled_tile_size, 
            held_item.item_data.height * ItemGrid.scaled_tile_size
        );
    }

    public void SetPosition(ItemGrid target_grid, InvItem held_item){
        highlighter.SetParent(target_grid.GetComponent<RectTransform>());
        highlighter.localPosition = target_grid.GetItemPos(held_item, held_item.grid_pos);
    }
}
