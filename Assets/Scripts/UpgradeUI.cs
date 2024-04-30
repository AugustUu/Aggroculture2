using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.Events;

public class UpgradeUi : MonoBehaviour
{
    
    public List<Button> buttons;
    
    static List<Button> buttons_static;

    public GameObject upgrade_parent;

    static GameObject upgrade_parent_static;


    // upgrades

    public Upgrade[] upgrades;
    private static Upgrade[]  upgrades_static;


    void Start()
    {
        upgrades_static = upgrades;
        buttons_static = buttons;
        upgrade_parent_static =  upgrade_parent;
        RandomiseUpgrades();
    }

    public static void RandomiseUpgrades(){
        foreach (Button button in buttons_static)
        {
            int index = Random.Range(0, upgrades_static.Length);
            foreach (var image in button.GetComponentsInChildren<Image>()){
                if (image.sprite.name != "UISprite")
                {
                    image.sprite = upgrades_static[index].sprite;
                }
            }
            
            button.GetComponentInChildren<TextMeshProUGUI>().text = upgrades_static[index].name;
            
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => {
                //Debug.Log(Upgrades_static[index].name);
                upgrades_static[index].value += 1;
                upgrades_static[index].onUpgrade.Invoke();
                upgrade_parent_static.SetActive(false);
            });
        }
    }

     
    public static void open_Upgrade_menu()
    {
        RandomiseUpgrades();
        upgrade_parent_static.SetActive(true);
    }

    public static Upgrade getUpgradeInfo(UpgradeList upgrade)
    {
        return upgrades_static[(int)upgrade];
    }
}

[Serializable]
public class Upgrade{
    public int value;
    public string name;

    public Sprite sprite;
    public UnityEvent onUpgrade;
}