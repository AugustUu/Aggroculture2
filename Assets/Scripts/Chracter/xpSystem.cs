using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class xp : MonoBehaviour
{
    public Slider experience_points;
    int exp;
    // Start is called before the first frame update
    void Start()
    {
        setExp(0);
    }

    void Update(){
        changeExp(1);
    }

    public void setExp(int xp){
        exp = xp;
        experience_points.value = exp;
    }

    public void changeExp (int xpChange){
        exp += xpChange;
        experience_points.value = exp;
    }

    public int getExp(){
        return exp;
    }
}
