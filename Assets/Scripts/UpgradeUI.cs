using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class UpgradeUi : MonoBehaviour
{
    
    public List<Button> buttons;
    
    static List<Button> buttonsStatic;

    public GameObject UpgradeParent;

    static GameObject UpgradeParentStatic;


    // upgrades

    public List<Upgrade> Upgrades;
    private static List<Upgrade> Upgrades_static;


    void Start()
    {
        Upgrades_static = Upgrades;
        buttonsStatic = buttons;
        UpgradeParentStatic =  UpgradeParent;
        RandomiseUpgrades();
    }

    public static void RandomiseUpgrades(){
        foreach (Button button in buttonsStatic)
        {
            int index = Random.Range(0, Upgrades_static.Count - 1);
            button.GetComponentInChildren<TextMeshProUGUI>().text = Upgrades_static[index].name;
            button.onClick.AddListener(() => {
                Debug.Log(Upgrades_static[index].name);
                Upgrades_static[index].value += 1;
                UpgradeParentStatic.SetActive(false);
            });
        }
    }

    
    public static void open_Upgrade_menu()
    {
        RandomiseUpgrades();
        UpgradeParentStatic.SetActive(true);
    }

    public static Upgrade getUpgradeInfo(string name)
    {
        foreach (var upgrade in Upgrades_static)
        {
            if (upgrade.name == name)
            {
                return upgrade;
            }
        }

        return null;
    }
}

[Serializable]
public class Upgrade{
    public int value;
    public string name;

    public Image sprite;
}