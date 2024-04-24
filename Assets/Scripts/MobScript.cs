using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MobScript : MonoBehaviour
{

    private Transform target;
    public int health = 100;

    public GameObject xp_orb;
    private Transform xpParent;
    public int xp_drop_count;

    void Start(){
        target = GameObject.Find("Player").transform;
        xpParent = GameObject.Find("EXP").GetComponent<Transform>();
    }

    public void hit(int dammage){
        if(health > 0){
            health -= dammage;
            if(health <= 0){
                for(int i = 0; i < xp_drop_count; i++){
                    Instantiate(xp_orb, new Vector3(transform.position.x + Random.Range(-2, 2), transform.position.y, transform.position.z + Random.Range(-2, 2)), this.transform.rotation,xpParent);
                }
                Destroy(this.transform.gameObject);
            }
        }
    }


    void Update(){
        
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position; 
    
        Vector3 forward = new Vector3();
        float rotation = Mathf.Deg2Rad *transform.rotation.eulerAngles.y;

        forward.x = (float)(Math.Sin(rotation));
        forward.z = (float)(Math.Cos(rotation));

        Ray ray = new Ray(this.transform.position, forward);    
        RaycastHit hit_object;
        if (Physics.Raycast(ray, out hit_object)){
            Debug.DrawRay(ray.origin, ray.direction * 5, Color.red, 1f);
        }else{
            Debug.DrawRay(ray.origin, ray.direction * 5, Color.blue, 0.01f);
        }

        rotation += Mathf.PI / 4; // check 45deg to right 
        
        forward.x = Mathf.Sin(rotation);
        forward.z = Mathf.Cos(rotation);

        ray = new Ray(this.transform.position, forward);    
        if (Physics.Raycast(ray, out hit_object)){
            Debug.DrawRay(ray.origin, ray.direction * 5, Color.red, 1f);
        }else{
            Debug.DrawRay(ray.origin, ray.direction * 5, Color.blue, 0.01f);
        }

        rotation -= Mathf.PI / 2; // check 45deg to left 
        
        forward.x = Mathf.Sin(rotation);
        forward.z = Mathf.Cos(rotation);

        ray = new Ray(this.transform.position, forward);    
        if (Physics.Raycast(ray, out hit_object)){
            Debug.DrawRay(ray.origin, ray.direction * 5, Color.red, 1f);
        }else{
            Debug.DrawRay(ray.origin, ray.direction * 5, Color.blue, 0.01f);
        }

        
    }
}
