using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public Slider health_points;
    int life;

    
    // Start is called before the first frame update
    void Start()
    {
        setHealth(100);
    }

    public void setHealth(int health){
        life = health;
        health_points.value = life;
    }

    public void changeHealth (int healthChange){
        life += healthChange;
        health_points.value = life;
    }

    public int getHealth(){
        return life;
    }
}