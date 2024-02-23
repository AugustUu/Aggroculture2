using System;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerInteraction : MonoBehaviour
{
    public new Camera camera;
    public GameObject placeable;
    public InvController inv_controller;

    public Transform model_transform;

    public void dig()
    {
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
                // FarmlandScript script = farmland.GetComponent<FarmlandScript>();

                Debug.DrawRay(ray.origin, ray.direction * hit_object.distance, Color.green, 1f);
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * hit_object.distance, Color.red, 1f);
            }
        }

    }

    public void plant(SeedType seed_type)
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit_object;

        if (Physics.Raycast(ray, out hit_object, 300f, 1 << 7))
        {
            FarmlandScript script = hit_object.collider.gameObject.GetComponent<FarmlandScript>();
            if(script != null){
                if(script.growth >= 6){
                    inv_controller.InsertItemID(Utils.farm_dict[seed_type]);
                }
                else{
                    script.Plant(seed_type);
                }
                
            }
            
            Debug.DrawRay(ray.origin, ray.direction * hit_object.distance, Color.blue, 0.5f);
        }
    }

    double lastShot = 0;
    public void shoot(GunStats stats)
    {
        double dealy = 1.0 / stats.firerate;
        if (Time.timeSinceLevelLoadAsDouble - lastShot >= dealy)
        {

            lastShot = Time.timeSinceLevelLoadAsDouble;

            for (int i = 0; i <= stats.extra_shots; i++)
            {

                Vector3 player_screen_pos = camera.WorldToScreenPoint(transform.position);
                Vector3 mouse_pos = Input.mousePosition;

                float rotation = Mathf.Atan2(player_screen_pos.x - mouse_pos.x, player_screen_pos.y - mouse_pos.y) + Mathf.PI + transform.rotation.eulerAngles.y / Mathf.Rad2Deg;

                Vector3 forward = new Vector3 
                {
                    x = (float)(Math.Cos(0.0) * Math.Sin(rotation)) + UnityEngine.Random.Range(stats.spread * -1 * Mathf.Deg2Rad, stats.spread * Mathf.Deg2Rad),
                    z = (float)(Math.Cos(0.0) * Math.Cos(rotation)) + UnityEngine.Random.Range(stats.spread * -1 * Mathf.Deg2Rad, stats.spread * Mathf.Deg2Rad)
                };

                Ray ray = new Ray(this.transform.position, forward);
                RaycastHit hit_object;
                // use raycast all
                if (Physics.Raycast(ray, out hit_object, 300f, 1 << 11))
                {
                    
                    Debug.DrawRay(ray.origin, ray.direction * hit_object.distance, Color.green, 1f);
                    MobScript mob = hit_object.transform.gameObject.GetComponent<MobScript>();
                    if (mob != null)
                    {
                        mob.hit(stats.damage);
                        Debug.Log(mob);
                    }
                }
                else
                {
                    Debug.DrawRay(this.transform.position, forward * 10, Color.red, 1f);
                }
            }
        }
    }


    void Update()
    {


        if (Input.GetMouseButton(0))
        {
            if (InvController.equipped_item != null)
            {
                //Debug.Log(InvController.equipped_item.item_data.name);
                switch (InvController.equipped_item.item_data.item_type)
                {
                    case ItemType.Gun:
                        shoot(InvController.equipped_item.item_data.gun_stats);
                        break;
                    case ItemType.Tool:
                        dig();
                        break;
                    case ItemType.Seeds:
                        plant(InvController.equipped_item.item_data.seed_type);
                        break;
                    default:
                        break;

                }
            }
        }
        /*
        if(gun_mode){
            if (Input.GetMouseButton(0)){
                
                Vector3 forward = new Vector3();

                forward.x = (float)(Math.Cos(0.0) * Math.Sin(Movement.rotation)); 
                forward.z = (float)(Math.Cos(0.0) * Math.Cos(Movement.rotation));
                //Debug.Log(forward);
                //Debug.Log(model_transform.forward);
                Ray ray = new Ray(this.transform.position, forward);    
                RaycastHit hit_object;
                if (Physics.Raycast(ray, out hit_object,300f, 1 << 11)){
                    Debug.DrawRay(ray.origin, ray.direction * hit_object.distance, Color.red, 1f);
                    MobScript mob = hit_object.transform.gameObject.GetComponent<MobScript>();
                    if(mob != null){
                        mob.hit(25);
                        Debug.Log(mob); 
                    }
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
        }*/

    }

}
