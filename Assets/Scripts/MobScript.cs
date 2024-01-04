using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobScript : MonoBehaviour
{
    void Start()
    {
        
    }

    public Transform target;

    void Update(){
        
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position; 
    
        Vector3 forward = new Vector3();
        float rotation = Mathf.Deg2Rad *transform.rotation.eulerAngles.y;

        forward.x = (float)(Math.Sin(rotation));
        forward.z = (float)(Math.Cos(rotation));
//ray shooting infront of enemy
        Ray ray = new Ray(this.transform.position, forward);    
        RaycastHit hit_object;
        if (Physics.Raycast(ray, out hit_object)){
            Debug.DrawRay(ray.origin, ray.direction * 5, Color.red, 1f);
        }else{
            Debug.DrawRay(ray.origin, ray.direction * 5, Color.blue, 0.01f);
        }

        rotation += 0.78539816339f;
        
        forward.x = (float)(Math.Sin(rotation));
        forward.z = (float)(Math.Cos(rotation));

        ray = new Ray(this.transform.position, forward);    
        if (Physics.Raycast(ray, out hit_object)){
            Debug.DrawRay(ray.origin, ray.direction * 5, Color.red, 1f);
        }else{
            Debug.DrawRay(ray.origin, ray.direction * 5, Color.blue, 0.01f);
        }

        rotation -= 0.78539816339f*2;
        
        forward.x = (float)(Math.Sin(rotation));
        forward.z = (float)(Math.Cos(rotation));

        ray = new Ray(this.transform.position, forward);    
        if (Physics.Raycast(ray, out hit_object)){
            Debug.DrawRay(ray.origin, ray.direction * 5, Color.red, 1f);
        }else{
            Debug.DrawRay(ray.origin, ray.direction * 5, Color.blue, 0.01f);
        }

        
    }
}
