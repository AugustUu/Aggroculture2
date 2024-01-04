using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public new Camera camera;
    public GameObject placeable;

    public Transform model_transform;

    public bool gun_mode = true;

    void Start()
    {

    }

    public void OnKeyDown(){
        Debug.Log("KEY DOWN");
    }
    
    // Update is called once per frame
    void Update(){
        if(Input.GetButtonDown("BuildShoot")){
            gun_mode = !gun_mode;
            Debug.Log(gun_mode+" gunmode");
        }
    }
    void FixedUpdate()
    {
        if(gun_mode){
            if (Input.GetMouseButton(0)){
                
                Vector3 forward = new Vector3();

                forward.x = (float)(Math.Cos(0.0) * Math.Sin(Movement.rotation)); 
                forward.z = (float)(Math.Cos(0.0) * Math.Cos(Movement.rotation));
                Debug.Log(forward);
                //Debug.Log(model_transform.forward);
                Ray ray = new Ray(this.transform.position, forward);    
                RaycastHit hit_object;
                if (Physics.Raycast(ray, out hit_object,300f, 1 << 11)){
                    Debug.DrawRay(ray.origin, ray.direction * hit_object.distance, Color.red, 1f);
                }
            }
        }else{
           if (Input.GetMouseButton(0)){
                
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit_object;

                if (Physics.Raycast(ray, out hit_object, 300f, 1 << 6))
                {
                    if (hit_object.normal != Vector3.up) // dont place on walls
                        return;

                    Vector3 scale = placeable.transform.localScale;

                    Vector3 position = new Vector3(
                        Mathf.Round(hit_object.point.x / scale.x) * scale.x,
                        hit_object.point.y,
                        Mathf.Round(hit_object.point.z / scale.z) * scale.z
                    );
                                                                                    
                    if (!Physics.CheckBox(position, placeable.transform.localScale / 2.01f, Quaternion.identity, ~(1 << 6)))
                    {
                        GameObject farmland = Instantiate(placeable, position, Quaternion.identity);
                        FarmlandScript script = farmland.GetComponent<FarmlandScript>();
                    
                        Debug.DrawRay(ray.origin, ray.direction * hit_object.distance, Color.green, 1f);
                    }
                    else
                    {
                        Debug.DrawRay(ray.origin, ray.direction * hit_object.distance, Color.red, 1f);
                    }
                }
            }

            if (Input.GetMouseButton(1))
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit_object;

                if (Physics.Raycast(ray, out hit_object, 300f, 1 << 7))
                {
                    FarmlandScript script = hit_object.collider.gameObject.GetComponent<FarmlandScript>();
                    if(script != null){
                        script.plant +=1;
                    }
                    
                    Debug.DrawRay(ray.origin, ray.direction * hit_object.distance, Color.blue, 0.5f);
                }
            } 
        }
    
    }
}
