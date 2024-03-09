using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public Slider health_points;
    public static Slider health_points2;
    public static int life;
    public static int max_hp;

    
    // Start is called before the first frame update
    void Start()
    {
        health_points2 = health_points;
        setHealth(100);
    }

    public static void setHealth(int health){
        life = health;
        health_points2.value = life;
    }

    public static void changeHealth (int healthChange){
        life += healthChange;
        life = Math.Min(life, 100);
        health_points2.value = life;
    }

    public static int getHealth(){
        return life;
    }
}