using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XPSystem : MonoBehaviour
{
    public Slider experience_points;
    public TextMeshProUGUI display;

    private static Slider experience_points_rf;
    static int exp;
    int level_up_req = 50;
    int levels = 1;
    // Start is called before the first frame update
    void Start()
    {
        experience_points_rf = experience_points;
        setExp(0);
        display.text = levels.ToString();
    }

    void Update(){
        if(exp >= level_up_req)
        {
            LevelUp(level_up_req);
        }
    }

    private void LevelUp(int exp_req)
    {
        exp -= exp_req;
        experience_points_rf.value = exp;
        levels += 1;
        level_up_req = (int)(exp_req * 1.2);
        experience_points.maxValue = level_up_req;
        display.text = levels.ToString();
        
    }

    public static void setExp(int xp){
        exp = xp;
        experience_points_rf.value = xp;
    }

    public static void changeExp (int xpChange){
        exp += xpChange;
        experience_points_rf.value = exp;
    }
}
