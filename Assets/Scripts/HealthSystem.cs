using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public Slider slider;
    int life;
    
    // Start is called before the first frame update
    void Start()
    {
        setHealth(100);
    }

    public void setHealth(int health){
        life = health;
        slider.value = life;
    }

    public void changeHealth (int healthChange){
        life += healthChange;
        slider.value = life;
    }

    public int getHealth(){
        return life;
    }
}