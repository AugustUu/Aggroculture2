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
    public int item;
    public int count;
}

[Serializable]
public class Trade
{
    public List<Item> input;
    public List<Item> output;
}

public class ShopUiScript : MonoBehaviour
{
    [SerializeField] Button left;
    [SerializeField] Button right;


    [SerializeField] GameObject ShopParent;

    int page = 0;

    [SerializeField]
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
                    Debug.Log(a + " " + trades[a]);
                });
                buttons[i % 3].GetComponentInChildren<TextMeshProUGUI>().text = trades[a] + " " + a;
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
}
