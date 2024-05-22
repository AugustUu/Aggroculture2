using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    static GameObject static_inv_parent;
    [SerializeField] InvHighlight inv_highlighter;
    [SerializeField] InvHighlight equip_highlighter;
    [SerializeField] GameObject tooltip;
    [SerializeField] Trash trash;
    TextMeshProUGUI tooltip_header;
    TextMeshProUGUI tooltip_subtitle;
    TextMeshProUGUI tooltip_body1;
    TextMeshProUGUI tooltip_body2;
    public static int heal_level = 0;
    public bool trashing = false;
    public AudioSource source;
    public AudioClip select_gun;
    public AudioClip select_alt;
    public AudioClip eating;

    public static float main_canvas_tile_size;
    public static float main_scaled_tile_size;

    void Start(){
        tooltip_header = tooltip.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        tooltip_subtitle = tooltip.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        tooltip_body1 = tooltip.transform.GetChild(0).GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        tooltip_body2 = tooltip.transform.GetChild(0).GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
        static_inv_parent = inv_parent;
    }
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

        if (Input.GetKeyDown(KeyCode.PageUp)){
            if(RemoveItemHeld(ItemList.schmeeze, 5)){
                Debug.Log("removed");
            }
            else{
                Debug.Log("not enough schmeeze");
            }
        }


        if(selected_item_grid != null){

            HandleHighlight(true);

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)){

                Vector2Int mouse_pos = selected_item_grid.GetGridPos(Input.mousePosition);
                

                if (Input.GetMouseButtonDown(0) && selected_item == null)
                {
                    if(selected_item_grid.GetItem(mouse_pos) != equipped_item){
                        Selected_item = selected_item_grid.GetItem(mouse_pos);
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
                    if(selected_item_grid.SpaceCheck(selected_item, mouse_pos + tile_offset, ref overlap_item)){
                        origin_grid.RemoveItem(selected_item, false);
                        selected_item_grid.PlaceItem(selected_item, mouse_pos + tile_offset, true);
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
            tooltip.SetActive(false);
            old_pos = new Vector2Int(-1, -1); // just so check mouse moved thing is always false  theres gotta be a better way to do but idfk lol
            if (Input.GetMouseButtonUp(0) && selected_item != null){
                ReturnItem();
                if(trashing){
                    origin_grid.RemoveItem(selected_item, true);
                    trash.Close();
                }
                Debug.Log("item dropped outside inv");
                Selected_item = null;
            }
        }
        
    }

    InvItem highlighted_item;
    Vector2Int old_pos;
    
    public void HandleHighlight(bool check_mouse)
    {
        Vector2Int mouse_grid_pos = selected_item_grid.GetGridPos(Input.mousePosition) + tile_offset;
        tooltip.transform.position = Input.mousePosition;
        if(check_mouse){
            if(old_pos == mouse_grid_pos){ return; }
        }
        old_pos = mouse_grid_pos;
        
        
        if(selected_item == null){
            highlighted_item = selected_item_grid.GetItem(mouse_grid_pos);
            if(highlighted_item != null){
                HandleTooltip(highlighted_item);
                
                if(highlighted_item != equipped_item){
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
            if(highlighted_item == null){
                inv_highlighter.SetVisible(false);
                tooltip.SetActive(false);
            }
            
        }
        else{
            inv_highlighter.SetSize(selected_item, selected_item_grid);
            inv_highlighter.SetParent(selected_item_grid);
            inv_highlighter.SetSibling(true);
            inv_highlighter.SetPosition(selected_item_grid, selected_item, mouse_grid_pos);
            inv_highlighter.SetVisible(selected_item_grid.BoundsCheck(mouse_grid_pos, selected_item.item_data.width, selected_item.item_data.height));
            tooltip.SetActive(false);
        }
    }

    private string dark_gray_color = "<color=#7b7b7b>";
    private string mid_gray_color = "<color=#989898>";
    private string light_gray_color = "<color=#a4a4a4>";
    private string equip_color = "<color=#b7b13c>";
    private string green_color = "<color=#009e30>";
    private string red_color = "<color=#cb4747>";
    private void HandleTooltip(InvItem item){
        ItemData data = item.item_data;
        tooltip.SetActive(true);
        tooltip_header.text = dark_gray_color;
        tooltip_subtitle.text = mid_gray_color;
        tooltip_body1.text = equip_color;
        tooltip_body2.text = light_gray_color;

        tooltip_header.text += data.name;
        if(data.tooltip != ""){
            tooltip_subtitle.text += data.tooltip;
        }
        // tooltip_body1.text += data.item_type.ToString();
        if(item == equipped_item){
            tooltip_body1.text += "equipped";
        }

        switch(data.item_type){
            case ItemType.Gun:
                string firerate_upgrade = "";
                string damage_upgrade = "";
                string shots_upgrade = "";
                if(UpgradeUi.getUpgradeInfo(UpgradeList.firerateUp).value > 0){
                    firerate_upgrade = " " + equip_color + "+" + data.gun_stats.firerate / 10.0 * UpgradeUi.getUpgradeInfo(UpgradeList.firerateUp).value;
                }
                if(UpgradeUi.getUpgradeInfo(UpgradeList.dammageUp).value > 0){
                    damage_upgrade = " " + equip_color + "+" + data.gun_stats.damage / 10.0 * UpgradeUi.getUpgradeInfo(UpgradeList.dammageUp).value;
                }
                if(UpgradeUi.getUpgradeInfo(UpgradeList.shotsUp).value > 0){
                    shots_upgrade = " " + equip_color + "+" + UpgradeUi.getUpgradeInfo(UpgradeList.shotsUp).value;
                }
                if(equipped_item != null && equipped_item != item && equipped_item.item_data.item_type == ItemType.Gun){
                    
                    tooltip_body2.text += "firerate: ";
                    if(equipped_item.item_data.gun_stats.firerate < data.gun_stats.firerate){
                        tooltip_body2.text += green_color + data.gun_stats.firerate + firerate_upgrade + green_color + " ▲";
                    }
                    else if(equipped_item.item_data.gun_stats.firerate > data.gun_stats.firerate){
                        tooltip_body2.text += red_color + data.gun_stats.firerate + firerate_upgrade + red_color + " ▼";
                    }
                    else{
                        tooltip_body2.text += data.gun_stats.firerate + firerate_upgrade;
                    }
                    tooltip_body2.text += "\n" + light_gray_color + "damage: ";
                    if(equipped_item.item_data.gun_stats.damage < data.gun_stats.damage){
                        tooltip_body2.text += green_color + data.gun_stats.damage + damage_upgrade + green_color + " ▲";
                    }
                    else if(equipped_item.item_data.gun_stats.damage > data.gun_stats.damage){
                        tooltip_body2.text += red_color + data.gun_stats.damage + damage_upgrade + red_color + " ▼";
                    }
                    else{
                        tooltip_body2.text += data.gun_stats.damage + damage_upgrade;
                    }
                    tooltip_body2.text += "\n" + light_gray_color + "shots fired: " ;
                    if(equipped_item.item_data.gun_stats.extra_shots < data.gun_stats.extra_shots){
                        tooltip_body2.text += green_color + (data.gun_stats.extra_shots + 1) + shots_upgrade + green_color + " ▲";
                    }
                    else if(equipped_item.item_data.gun_stats.extra_shots > data.gun_stats.extra_shots){
                        tooltip_body2.text += red_color + (data.gun_stats.extra_shots + 1) + shots_upgrade + red_color + " ▼";
                    }
                    else{
                        tooltip_body2.text += data.gun_stats.extra_shots + 1 + shots_upgrade;
                        }
                }
                else{
                    tooltip_body2.text += "firerate: " + data.gun_stats.firerate + firerate_upgrade;
                    tooltip_body2.text += mid_gray_color + "\ndamage: " + data.gun_stats.damage + damage_upgrade;
                    tooltip_body2.text += mid_gray_color + "\nshots fired: " + (data.gun_stats.extra_shots + 1) + shots_upgrade;
                }
                break;
            case ItemType.Food:
                tooltip_body2.text += "heals " + green_color + (data.food_heal_amount + heal_level) + " health";
                break;
            case ItemType.Seeds:
                tooltip_body2.text += data.seed_type.ToString().ToLower() + " seeds"; // only tolower because thats the current look
                break;
            case ItemType.Tool:
                tooltip_body2.text += "plots: " + PlayerInteraction.plots + " / " + PlayerInteraction.max_plots;
                break;
            case ItemType.Misc:
                tooltip_body2.text += "right click to use";
                break;
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
            main_grid.PlaceItem(inserting_item, open_pos.Value, true);
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

    public bool InsertItemID(ItemList item_ID){
        return InsertItem(GenerateItem((int) item_ID));
    }

    public static void StaticInsertItemID(ItemList item_ID){
        static_inv_parent.GetComponent<InvController>().InsertItemID(item_ID);
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
        origin_grid.PlaceItem(selected_item, origin_pos, false);
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
                }
                else if(to_equip_item != null){
                    if (to_equip_item.item_data.item_type == ItemType.Gun)
                    {
                        source.PlayOneShot(select_gun);
                    }
                    else
                    {
                        source.PlayOneShot(select_alt);
                    }
                    equipped_item = to_equip_item;
                    equip_highlighter.SetSize(equipped_item, selected_item_grid);
                    equip_highlighter.SetParent(selected_item_grid);
                    equip_highlighter.SetSibling(true);
                    equip_highlighter.SetPosition(selected_item_grid, equipped_item);
                    equip_highlighter.SetVisible(true);
                }
            }
            else if(to_equip_item.item_data.item_type == ItemType.Food){
                source.PlayOneShot(eating);
                selected_item_grid.PickUpItem(to_equip_item.grid_pos);
                HealthSystem.changeHealth(to_equip_item.item_data.food_heal_amount + heal_level);
                Destroy(to_equip_item.gameObject);
            }
            else if(to_equip_item.item_data.item_type == ItemType.Misc){
                selected_item_grid.PickUpItem(to_equip_item.grid_pos);
                if(to_equip_item.item_data.name == items[(int)ItemList.wooden_claw].name){
                    InsertItemID(((int)ItemList.tomato_seeds) + UnityEngine.Random.Range(0, 3));
                }
                else if(to_equip_item.item_data.name == items[(int)ItemList.premium_fertilizer].name){
                    UpgradeUi.Upgrade((int)UpgradeList.growthUP, 3);
                }
                Destroy(to_equip_item.gameObject);
            }
            HandleHighlight(false);
        }
    }

    public bool RemoveItemHeld(ItemList type, int count){
        return main_grid.RemoveItemHeld(items[(int) type].name, count);
    }

    public bool CheckItemHeld(ItemList type, int count){
        return main_grid.CheckItemHeld(items[(int) type].name, count);
    }
}
