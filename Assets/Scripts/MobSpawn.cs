using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class MobSpawn : MonoBehaviour
{
    [SerializeField]
    public GameObject mob_base;
    bool spawned_wave = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Q)){
            for (int i = 0; i < 10; i++)
            {
                float spawn_magnitude = Random.Range(50f, 100f);
                float spawn_angle = Random.Range(0f, Mathf.PI * 2);
                Vector3 spawn_pos = new Vector3(Mathf.Cos(spawn_angle), 0, Mathf.Sin(spawn_angle));
                spawn_pos *= spawn_magnitude;
                Vector3 position = GameObject.Find("Player").transform.position;
                position += spawn_pos;
                position.y = 0;
                Instantiate(mob_base, position, GameObject.Find("Player").transform.rotation);
            }
        }

        /*
        int multiplier = TimeCycle.days * Random.Range(1, 4);
        if (TimeCycle.hours == 1 && spawned_wave == false)
        {
            for (int i = 0; i < multiplier; i++)
            {
                float spawn_magnitude = Random.Range(50f, 100f);
                float spawn_angle = Random.Range(0f, Mathf.PI * 2);
                Vector3 spawn_pos = new Vector3(Mathf.Cos(spawn_angle), 0, Mathf.Sin(spawn_angle));
                spawn_pos *= spawn_magnitude;
                Vector3 position = GameObject.Find("Player").transform.position;
                position += spawn_pos;
                position.y = 0;
                Instantiate(mob_base, position, GameObject.Find("Player").transform.rotation);
            }

            spawned_wave = true;
        }
        if (spawned_wave && TimeCycle.hours != 0)
        {
            spawned_wave = false;
        }*/
    }
}
