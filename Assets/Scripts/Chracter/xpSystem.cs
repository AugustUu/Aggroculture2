using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class XpSystem : MonoBehaviour
{
    public Slider experience_points;
    public TextMeshProUGUI display;

    private static Slider experience_points_rf;
    static int exp;
    int exp_req = 100;
    int levels = 1;
    // Start is called before the first frame update
    void Start()
    {
        experience_points_rf = experience_points;
        setExp(0);
        display.text = levels.ToString();
    }

    void Update(){
        //changeExp(1);
        if(exp >= exp_req){
            exp = 0;
            levels += 1;
            exp_req = (int)(exp_req *1.2);
            experience_points.maxValue = exp_req;
            display.text = levels.ToString();
            //pauses for level menu
            //Time.timeScale = 0;                                                                                                                                                                                                                                                                                                            
        }
    }

    public static void setExp(int xp){
        exp = xp;
        experience_points_rf.value = exp;
    }

    public static void changeExp (int xpChange){
        exp += xpChange;
        experience_points_rf.value = exp;
    }

    public int getExp(){
        return exp;
    }
}
