using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Item
{
    public ItemList item;
    public int count;
}

[Serializable]
public class Trade
{
    public List<Item> input;
    public Item output;
}

public class ShopUiScript : MonoBehaviour
{
    [SerializeField] Button left;
    [SerializeField] Button right;


    [SerializeField] GameObject ShopParent;

    static GameObject ShopParentStatic;

    public InvController inv_controller;



    int page = 0;

    public List<Trade> trades;
    public List<Button> buttons;

    private void loadPage(int page)
    {

        int max = page * 3 + 3 < trades.Count ? page * 3 + 3 : trades.Count;

        //Debug.Log("page " + page + " " + max);

        if (page * 3 < max)
        {

            for (int i = page * 3; i < max; i++)
            {
                int a = i;
                buttons[i % 3].onClick.RemoveAllListeners();
                buttons[i % 3].onClick.AddListener(() =>
                {
                    foreach(Item item in trades[a].input){
                        if(!inv_controller.CheckItemHeld(item.item,item.count)){
                            return;
                        }
                    }
                    foreach(Item item in trades[a].input){
                        inv_controller.RemoveItemHeld(item.item,item.count);
                    }

                    inv_controller.InsertItemID(trades[a].output.item);
                    Debug.Log(a + " " + trades[a]);
                });

                string name = "";

                foreach(Item item in trades[a].input){
                    name += "" + item.count + " " + item.item + " ";
                }
                name += "For " + trades[a].output.item;
                buttons[i % 3].GetComponentInChildren<TextMeshProUGUI>().text = name;
            }
            for (int i = max - page * 3; i < 3; i++)
            {
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
            }

        }

    }

    public void on_click()
    {
        loadPage(page);

        ShopParentStatic =  ShopParent;

        left.interactable = page - 1 >= 0;

        right.interactable = (page+1) * 3 < trades.Count;
    }


    private void Start()
    {
        left.onClick.AddListener(left_click);
        right.onClick.AddListener(right_click);
        loadPage(0);

        on_click();

    }

    void left_click()
    {
        page -= 1;

        on_click();
    }

    void right_click()
    {
        page += 1;

        on_click();
    }

    public static void toggle_trade_menu(){
        ShopParentStatic.SetActive(!ShopParentStatic.activeSelf);
    }
}
