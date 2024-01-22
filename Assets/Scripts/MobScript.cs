using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobScript : MonoBehaviour
{

    private Transform target;
    public int health = 100;

    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    public void hit(int dammage)
    {
        health -= dammage;
        if (health <= 0)
        {
            Destroy(this.transform.gameObject);
        }
        Debug.Log("Shot3: " + health);
    }

    void checkDir(Vector3 forward, float rotation)
    {
        forward.x = (float)(Math.Sin(rotation));
        forward.z = (float)(Math.Cos(rotation));

        Ray ray = new Ray(this.transform.position, forward);
        RaycastHit hit_object;
        if (Physics.Raycast(ray, out hit_object, 5f, 1 << 10))
        {
            Debug.DrawRay(ray.origin, ray.direction * 5, Color.red, 1f);
        }
        else
        {
            //Debug.DrawRay(ray.origin, ray.direction * 5, Color.blue, 0.01f);
        }

    }

    void FixedUpdate()
    {

        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        try
        {
            agent.destination = target.position;
        }
        catch (Exception)
        {
            Debug.Log(this.transform.position);
        }

        if (this.transform.position.y < 0)
        {
            Debug.Log(this.transform.position);
        }

        Vector3 forward = new Vector3();
        float rotation = Mathf.Deg2Rad * transform.rotation.eulerAngles.y;

        checkDir(forward, rotation);

        checkDir(forward, rotation + Mathf.Deg2Rad * 45);

        checkDir(forward, rotation - Mathf.Deg2Rad * 45);


    }
}
