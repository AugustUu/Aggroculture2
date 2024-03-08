using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class interactable : MonoBehaviour
{

    [SerializeField]
    public UnityEvent input_event; 

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("enter " + other.GetInstanceID() + " " + other.name);
        InteractionManager.onEnter(this.GetInstanceID(), this.name,input_event);
    }

    private void OnTriggerExit(Collider other)
    {
        // Debug.Log("exit " + other.GetInstanceID() + " " + this.name);
        InteractionManager.onExit(this.GetInstanceID());
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

}
