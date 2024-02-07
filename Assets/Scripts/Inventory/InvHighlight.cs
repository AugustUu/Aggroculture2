using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InvHighlight : MonoBehaviour
{
    [SerializeField] RectTransform highlighter;
    [SerializeField] RectTransform equip_highlighter;
    RectTransform[] sprites = new RectTransform[2];

    public void Awake(){
        sprites[0] = highlighter;
        sprites[1] = equip_highlighter; // SUCKS !???
    }
    
    public void SetVisible(int index, bool b)
    {
        sprites[index].gameObject.SetActive(b);
    }

    public void SetSize(int index, InvItem held_item, ItemGrid selected_item_grid)
    {
        sprites[index].sizeDelta = new Vector2(
            held_item.item_data.width * selected_item_grid.scaled_tile_size, 
            held_item.item_data.height * selected_item_grid.scaled_tile_size
        );
    }

    public void SetPosition(int index, ItemGrid target_grid, InvItem held_item)
    {
        sprites[index].localPosition = target_grid.GetItemPos(held_item, held_item.grid_pos);
    }

    public void SetPosition(int index, ItemGrid target_grid, InvItem held_item, Vector2Int mouse_grid_pos)
    {
        sprites[index].localPosition = target_grid.GetItemPos(held_item, mouse_grid_pos);
    }

    public void SetPositionRaw(int index, Vector3 pos){
        sprites[index].position = pos;
        // Debug.Log("" + pos + ", " + sprites[index].localPosition);
    }

    public void SetParent(int index, ItemGrid target_grid)
    {
        if(target_grid == null){ return; }
        sprites[index].SetParent(target_grid.GetComponent<RectTransform>());
        sprites[index].SetAsLastSibling();
    }

    public void SetParent(int index, Transform target_obj)
    {
        if(target_obj == null){ return; }
        sprites[index].SetParent(target_obj);
        sprites[index].SetAsLastSibling();
    }
}
