using System;
using System.IO;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Playables;
using static UnityEngine.ParticleSystem;

public class PlayerInteraction : MonoBehaviour
{
    public new Camera camera;
    public GameObject placeable;
    public InvController inv_controller;


    public ParticleSystem particle_system;
    public static int plots = 0;
    public static int max_plots = 10;
    public Transform farmParent;

    public AudioSource source;
    public AudioClip clip;
    public AudioClip digging;

    public void dig()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit_object;


        if (Physics.Raycast(ray, out hit_object, 300f, 1 << 7)){
            Debug.Log("babagaboosh");
            FarmlandScript script = hit_object.collider.gameObject.GetComponent<FarmlandScript>();
            if(script != null){
                if(script.plant == SeedType.None){
                    Destroy(script.gameObject);
                    plots--;
                }
                else if(script.growth < Utils.plant_list[(int) script.plant].grow_time){
                    script.RemovePlant();
                }
            }
            
        }
        else if (Physics.Raycast(ray, out hit_object, 300f, 1 << 6) && plots < max_plots)
        {
            Debug.Log("bagabadoosh");
            if (hit_object.normal != Vector3.up) // dont place on walls
                return;

            Vector3 scale = placeable.transform.localScale;

            Vector3 position = new Vector3(
                Mathf.Round(hit_object.point.x / scale.x) * scale.x,
                hit_object.point.y,
                Mathf.Round(hit_object.point.z / scale.z) * scale.z
            );

            if (!Physics.CheckBox(position, placeable.transform.localScale / 2.01f, Quaternion.identity, ~((1 << 6) | (1 << 11))))
            {
                GameObject farmland = Instantiate(placeable, position, Quaternion.identity,farmParent);
                source.PlayOneShot(digging);
                // FarmlandScript script = farmland.GetComponent<FarmlandScript>();
                    
                plots++;
                
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
                if(script.plant == SeedType.None){
                    script.Plant(seed_type);
                }
            }
            Debug.DrawRay(ray.origin, ray.direction * hit_object.distance, Color.blue, 0.5f);
        }
    }

    public void Harvest()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit_object;

        if (Physics.Raycast(ray, out hit_object, 300f, 1 << 7))
        {
            FarmlandScript script = hit_object.collider.gameObject.GetComponent<FarmlandScript>();
            if(script != null){
                if(script.plant != SeedType.None){
                    if(script.growth >= Utils.plant_list[(int) script.plant].grow_time){
                        if(inv_controller.InsertItemID(Utils.plant_list[(int) script.plant].produce_item)){
                            script.RemovePlant();
                        };
                    }
                }
            }
            Debug.DrawRay(ray.origin, ray.direction * hit_object.distance, Color.blue, 0.5f);
        }
    }

    double lastShot = 0;
    public void shoot(GunStats stats)
    {
        clip = stats.gunsound;
        double dealy = 1.0 / stats.firerate;
        if (Time.timeSinceLevelLoadAsDouble - lastShot >= dealy)
        {
            
            lastShot = Time.timeSinceLevelLoadAsDouble;
            source.PlayOneShot(clip);
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

                particle_system.transform.forward = forward;
                particle_system.transform.position = this.transform.position;

                EmitParams particle = new EmitParams();

                particle_system.Emit(particle,1);
                
                Ray ray = new Ray(this.transform.position, forward);
                RaycastHit[] hits = Physics.RaycastAll(this.transform.position, forward,300f);
                // use raycast all
                foreach (var hit in hits)
                {
                    if (hit.transform.gameObject.layer == 11)
                    {
                        MobScript mob = hit.transform.gameObject.GetComponent<MobScript>();
                        if (mob != null)
                        {
                            mob.hit(stats.damage);
                            Debug.Log(mob);
                        }
                    }
                    else
                    {
                        return;
                    }
                    // do stuff here
                }

       
                /*
                if ()
                {
                    
                    Debug.DrawRay(ray.origin, ray.direction * hit_object.distance, Color.green, 1f);
                    MobScript mob = hit_object.transform.gameObject.GetComponent<MobScript>();
                    if (mob != null)
                    {
                        mob.hit(stats.damage);
                        // Debug.Log(mob);
                    }
                }
                else
                {
                    Debug.DrawRay(this.transform.position, forward * 10, Color.red, 1f);
                }*/
            }
        }
    }


    void Update()
    {

        if(!Pause.is_paused){
            if (Input.GetMouseButton(0))
            {
                if (InvController.equipped_item != null)
                {
                    if(Input.GetMouseButtonDown(0)){
                        switch (InvController.equipped_item.item_data.item_type)
                    {
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
                    //Debug.Log(InvController.equipped_item.item_data.name);
                    switch (InvController.equipped_item.item_data.item_type)
                    {
                        case ItemType.Gun:
                            shoot(InvController.equipped_item.item_data.gun_stats);
                            
                            break;
                        default:
                            break;

                    }
                }
            }
            else if(Input.GetMouseButton(1)){
                Harvest(); // should probably end up as blanket interact script
            }
        }
        

    }

}
