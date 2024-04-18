using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class MobDamage : MonoBehaviour
{
    [SerializeField]
    void Start(){
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player"){
            if (gameObject.name.Equals("Boar(Clone)")){
                HealthSystem.changeHealth(-MobData.boar.getDamage());
            }
            if (gameObject.name.Equals("Wolf(Clone)")){
                HealthSystem.changeHealth(-MobData.wolf.getDamage());
            }
            if (gameObject.name.Equals("Rat(Clone)")){
                HealthSystem.changeHealth(-MobData.rat.getDamage());
            }
        }
    }
}