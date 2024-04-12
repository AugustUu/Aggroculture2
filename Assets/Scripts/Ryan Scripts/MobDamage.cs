using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.Events;


public class MobDamage : MonoBehaviour
{
    [SerializeField]
    //public UnityEvent input_event; 
    private GameObject player;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
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

    private void OnTriggerExit(Collider other)
    {
        // Debug.Log("exit " + other.GetInstanceID() + " " + this.name);
        //InteractionManager.onExit(this.GetInstanceID());
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}