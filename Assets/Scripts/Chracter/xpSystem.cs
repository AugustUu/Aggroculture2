using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XpSystem : MonoBehaviour
{
    public Slider experience_points;

    private static Slider experience_points_rf;
    static int exp;
    // Start is called before the first frame update
    void Start()
    {
        experience_points_rf = experience_points;
        setExp(0);
    }

    void Update(){
        //changeExp(1);
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
