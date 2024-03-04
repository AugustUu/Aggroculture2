using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class MobSpawn : MonoBehaviour
{
    [SerializeField]
    public GameObject rat;
    public GameObject boar;
    public GameObject wolf;
    bool spawned_wave = false;
    int minutes;
    // Start is called before the first frame update
    void Start()
    {
        int minutes = (int)TimeCycle.minutes;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Q)){
            for (int i = 0; i < 1; i++)
            {
                float spawn_magnitude = Random.Range(50f, 100f);
                float spawn_angle = Random.Range(0f, Mathf.PI * 2);
                Vector3 spawn_pos = new Vector3(Mathf.Cos(spawn_angle), 0, Mathf.Sin(spawn_angle));
                spawn_pos *= spawn_magnitude;
                Vector3 position = GameObject.Find("Player").transform.position;
                position += spawn_pos;
                position.y = 0;
                Instantiate(rat, position, GameObject.Find("Player").transform.rotation);
            }
        }
        //please to be balanced, small mult is the natural spawns and should be less than multiplier because they are consistantly spawning throughtout the day
        int smallmulti = TimeCycle.days;
        //please to be balanced, multiplier should be biggerthan small mult because it is the big wave, we might want to limit natural spawns so theyt dont spawn while the big wave is active
        int multiplier = TimeCycle.days;  
        if (TimeCycle.hours == 1 && spawned_wave == false)
        {
            Debug.Log("dao");
            for (int i = 0; i < multiplier; i++)
            {
                float spawn_magnitude = Random.Range(50f, 100f);
                float spawn_angle = Random.Range(0f, Mathf.PI * 2);
                Vector3 spawn_pos = new Vector3(Mathf.Cos(spawn_angle), 0, Mathf.Sin(spawn_angle));
                spawn_pos *= spawn_magnitude;
                Vector3 position = GameObject.Find("Player").transform.position;
                position += spawn_pos;
                position.y = 0;
                if(i%10 == 0){
                    Instantiate(wolf, position, GameObject.Find("Player").transform.rotation);
                }
                else if(i%5 == 0){
                    Instantiate(boar, position, GameObject.Find("Player").transform.rotation);
                }
                else{
                    Instantiate(rat, position, GameObject.Find("Player").transform.rotation);
                }
            }

            spawned_wave = true;
        }
        if (spawned_wave && TimeCycle.hours != 0)
        {
            spawned_wave = false;
        }

        
        if (TimeCycle.minutes != minutes)
        {
            for (int i = 0; i < smallmulti; i++)
            {
                float spawn_magnitude = Random.Range(50f, 100f);
                float spawn_angle = Random.Range(0f, Mathf.PI * 2);
                Vector3 spawn_pos = new Vector3(Mathf.Cos(spawn_angle), 0, Mathf.Sin(spawn_angle));
                spawn_pos *= spawn_magnitude;
                Vector3 position = GameObject.Find("Player").transform.position;
                position += spawn_pos;
                position.y = 0;
                minutes = (int)TimeCycle.minutes;
                if(i%10 == 0){
                    Instantiate(wolf, position, GameObject.Find("Player").transform.rotation);
                }
                else if(i%5 == 0){
                    Instantiate(boar, position, GameObject.Find("Player").transform.rotation);
                }
                else{
                    Instantiate(rat, position, GameObject.Find("Player").transform.rotation);
                }
            }

        }
    }
}
