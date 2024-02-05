using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering;

public class xpScript : MonoBehaviour
{
    
    Transform player_position;

    void Start()
    {
        player_position = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate(){
        if(Vector3.Distance(player_position.transform.position, this.transform.position) < 5){
            XpSystem.changeExp(5);
            Destroy(this.transform.gameObject);
        }
    }
    
}
