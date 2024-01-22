using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class xpScript : MonoBehaviour
{
    [SerializeField]
    public UnityEvent xp_event; 

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter " + other.GetInstanceID() + " " + other.name);
        InteractionManager.onEnter(this.GetInstanceID(), this.name, xp_event);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
