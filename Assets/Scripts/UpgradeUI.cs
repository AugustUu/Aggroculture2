using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    public Transform upgrade_stat_ui_parent;
    
    public GameObject upgrade_stat_ui_peice;

    // upgrades

    public Upgrade[] upgrades;
    private static Upgrade[]  upgrades_static;

    private static List<GameObject> upgrade_list_enements = new List<GameObject>();

    void Start()
    {
        upgrades_static = upgrades;
        buttons_static = buttons;
        upgrade_parent_static =  upgrade_parent;
        RandomiseUpgrades();

        Vector3 position = new Vector3(1450, 800,0);
        
        for (int i = 0; i < upgrades.Length; i++)
        {
            
            GameObject ui_element = Instantiate(upgrade_stat_ui_peice, position, Quaternion.identity,upgrade_stat_ui_parent);
            ui_element.GetComponentInChildren<TextMeshProUGUI>().text = "lvl " + upgrades[i].value;
            ui_element.GetComponentInChildren<Image>().sprite = upgrades[i].sprite;
            position.y -= 30;
            upgrade_list_enements.Add(ui_element);
        }
    }

    public static void RandomiseUpgrades(){
        foreach (Button button in buttons_static)
        {
            int index = Random.Range(0, upgrades_static.Length);
            /*foreach (var image in button.GetComponentsInChildren<Image>()){   old aujust code
                if (image.sprite.name != "brownButton")
                {
                    image.sprite = upgrades_static[index].sprite;
                }
            }*/
            button.transform.GetChild(1).GetComponent<Image>().sprite = upgrades_static[index].sprite;
            
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = upgrades_static[index].name;
            
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => {
                //Debug.Log(Upgrades_static[index].name);
                
                upgrades_static[index].value += 1;
                upgrades_static[index].onUpgrade.Invoke();
                upgrade_list_enements[index].GetComponentInChildren<TextMeshProUGUI>().text = "lvl " + upgrades_static[index].value;
                upgrade_parent_static.SetActive(false);
                Pause.ForcePause(false, false);
            });
        }
    }

     
    public static void open_Upgrade_menu()
    {
        RandomiseUpgrades();
        upgrade_parent_static.SetActive(true);
        Pause.ForcePause(true, false);
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