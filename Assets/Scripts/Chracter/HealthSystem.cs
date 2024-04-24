using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    static Slider hp_bar;
    public static int max_hp = 100;
    public static int hp = max_hp;


    
    // Start is called before the first frame update
    void Start()
    {
        hp_bar = GetComponent<Slider>();
        setHealth(max_hp);
    }

    public static void setHealth(int set_hp){
        hp = set_hp;
        hp_bar.value = hp;
    }

    public static void changeHealth (int hp_change){
        
        hp += hp_change;
        if(hp_change >= 0){
            
        }
        if(hp_change < 0){
            
        }

        hp = Math.Clamp(hp, 0, max_hp + (UpgradeUi.getUpgradeInfo(UpgradeList.healthUp).value * 10));
        hp_bar.value = hp;
    }
}