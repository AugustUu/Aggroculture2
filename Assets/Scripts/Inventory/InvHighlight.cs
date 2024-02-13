using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class InvHighlight : MonoBehaviour
{
    RectTransform rt;
    Image sprite;
    [SerializeField] float opacity;

    void Awake(){
        rt = GetComponent<RectTransform>();
        sprite = GetComponent<Image>();
    }
    public void SetVisible(bool b)
    {
        Color new_opacity = sprite.color;
        if(b){
            new_opacity.a = opacity;
        }
        else{
            new_opacity.a = 0;
        }
        sprite.color = new_opacity;
    }

    public void SetSize(InvItem held_item, ItemGrid selected_item_grid)
    {
        rt.sizeDelta = new Vector2(
            held_item.item_data.width * selected_item_grid.scaled_tile_size, 
            held_item.item_data.height * selected_item_grid.scaled_tile_size
        );
    }

    public void SetPosition(ItemGrid target_grid, InvItem held_item)
    {
        rt.localPosition = target_grid.GetItemPos(held_item, held_item.grid_pos);
    }

    public void SetPosition(ItemGrid target_grid, InvItem held_item, Vector2Int mouse_grid_pos)
    {
        rt.localPosition = target_grid.GetItemPos(held_item, mouse_grid_pos);
    }

    public void SetPositionRaw(Vector3 pos){
        rt.position = pos;
        // Debug.Log("" + pos + ", " + sprites[index].localPosition);
    }

    public void SetParent(ItemGrid target_grid)
    {
        if(target_grid == null){ return; }
        rt.SetParent(target_grid.GetComponent<RectTransform>());
        rt.SetAsLastSibling();
    }
}
