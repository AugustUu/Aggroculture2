using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class XPSystem : MonoBehaviour
{

    //life time no die with particles

    static Slider exp_bar;
    TextMeshProUGUI display;
    public InvController inv_controller;
    static int exp;
    int level_up_req = 100;
    int levels = 1;
    // Start is called before the first frame update
    void Start()
    {
        exp_bar = GetComponent<Slider>();
        display = gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        setExp(0);
        display.text = "lvl " + levels;
    }

    void Update(){
        if(exp >= level_up_req && !Pause.is_paused)
        {
            LevelUp(level_up_req);
        }
    }

    private void LevelUp(int exp_req)
    {
        exp -= exp_req;
        exp_bar.value = exp;
        levels += 1;
        level_up_req = (int)(exp_req * 1.4);
        exp_bar.maxValue = level_up_req;
        display.text = "lvl " + levels;
        PlayerInteraction.max_plots++;
        
        UpgradeUi.open_Upgrade_menu();
    }

    public static void setExp(int xp){
        exp = xp;
        exp_bar.value = xp;
    }

    public static void changeExp (int xpChange){
        exp += xpChange;
        exp_bar.value = exp;
    }
}
