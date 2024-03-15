using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class Upgrade{
    public int value;
    public string name;

    public Image sprite;
}

public class UpgradeUi : MonoBehaviour
{
    
    public List<Button> buttons;

    [SerializeField] GameObject UpgradeParent;

    static GameObject UpgradeParentStatic;


    // upgrades

    public List<Upgrade> Upgrades;


    void Start()
    {

        ArrayList array = new ArrayList();


        UpgradeParentStatic =  UpgradeParent;
        
    }

    public void randomiseUpgrades(){
        foreach (Button button in buttons){

        }
    }

    
    public static void toggle_Upgrade_menu(){
        UpgradeParentStatic.SetActive(!UpgradeParentStatic.activeSelf);
    }
}
