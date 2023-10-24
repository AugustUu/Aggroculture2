using System;
using UnityEngine;
using UnityEngine.Events;

public class interactable : MonoBehaviour
{



    private void OnTriggerEnter(Collider other){
        InteractionManager.onEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        InteractionManager.onEnter(other);
    }
}
