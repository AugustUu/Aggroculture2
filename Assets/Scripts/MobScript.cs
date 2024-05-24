using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class MobScript : MonoBehaviour
{

    private Transform target;
    public int health;
    public int dammage;
    
    public GameObject xp_orb;
    private Transform xpParent;
    public int xp_drop_count;
    public bool drops_item;
    public bool boss_drops;
    public ItemList dropped_item;
    public float drop_chance;
     
    
    void Start(){
        target = GameObject.Find("Player").transform;
        xpParent = GameObject.Find("EXP").GetComponent<Transform>();
    }

    public void hit(int damage){
        if(health > 0){
            health -= damage;
            if(health <= 0){
                for(int i = 0; i < xp_drop_count; i++){
                    if(xp_drop_count > 1){
                        Instantiate(xp_orb, new Vector3(transform.position.x + Random.Range(-2.0f, 2.0f), transform.position.y, transform.position.z + Random.Range(-2.0f, 2.0f)), this.transform.rotation,xpParent);
                    }
                    else{
                        Instantiate(xp_orb, new Vector3(transform.position.x, transform.position.y, transform.position.z), this.transform.rotation,xpParent);
                    }
                    
                }
                if(drops_item){
                    if(Random.Range(0.0f, 1.0f) < drop_chance){
                        InvController.StaticInsertItemID(dropped_item);
                    }
                }
                else if(boss_drops){
                    for(int i = 0; i < Random.Range(0, 11); i++){
                        InvController.StaticInsertItemID(ItemList.tomato);
                    }
                    for(int i = 0; i < Random.Range(0, 11); i++){
                        InvController.StaticInsertItemID(ItemList.potato);
                    }
                    for(int i = 0; i < Random.Range(0, 11); i++){
                        InvController.StaticInsertItemID(ItemList.carrot);
                    }
                }
                Destroy(this.transform.gameObject);
            }
        }
    }

    public bool Attack(float offset)
    {
        Vector3 forward = new Vector3();
        float rotation = Mathf.Deg2Rad * transform.rotation.eulerAngles.y + offset;

        forward.x = (float)(Math.Sin(rotation));
        forward.z = (float)(Math.Cos(rotation));

        Ray ray = new Ray(this.transform.position, forward);
        RaycastHit hit_object;
        if (Physics.Raycast(ray, out hit_object, 2f))
        {
            Debug.DrawRay(ray.origin, ray.direction * 5, Color.red, 1f);
            if (hit_object.transform.name == "Player")
            {
                HealthSystem.changeHealth(-this.dammage + TimeCycle.days);
                return true;
            }
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 5, Color.green, 0.5f);
        }

        return false;
    }

    double last_hit = 0;
    void Update(){
        
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position;

        if (Time.timeSinceLevelLoadAsDouble - last_hit > 0.5)
        {
            last_hit = Time.timeSinceLevelLoadAsDouble;

            if (Attack(0.0f) || Attack(Mathf.PI / 4) || Attack(-Mathf.PI / 4))
            {
                return;
            }


        }

    }

    public void OnDrawGizmos(){
        Handles.Label(transform.position, health.ToString());
    }
}
