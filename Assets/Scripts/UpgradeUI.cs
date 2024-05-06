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

    TextMeshProUGUI tooltip_header;
    TextMeshProUGUI tooltip_body;


    // upgrades

    public Upgrade[] upgrades;
    private static Upgrade[]  upgrades_static;


    void Start()
    {
        upgrades_static = upgrades;
        buttons_static = buttons;
        upgrade_parent_static =  upgrade_parent;
        tooltip_header = gameObject.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        tooltip_body = gameObject.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        RandomiseUpgrades();
    }

    public static void RandomiseUpgrades(){
        foreach (Button button in buttons_static)
        {
            int index = Random.Range(0, upgrades_static.Length);
            foreach (var image in button.GetComponentsInChildren<Image>()){
                if (image.sprite.name != "brownButton")
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

    public void HandleTooltip(int button_index){
        tooltip_header.text = "<color=#7b7b7b>";
        tooltip_body.text = "<color=#a4a4a4>";

        tooltip_header.text += "fdsa";
        tooltip_body.text += "asdf";
    }
}

[Serializable]
public class Upgrade{
    public int value;
    public string name;

    public Sprite sprite;
    public UnityEvent onUpgrade;
}