using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
public class MobSpawn : MonoBehaviour
{
    [SerializeField]
    public GameObject rat;
    public GameObject boar;
    public GameObject wolf;
    List<GameObject> mob_list = new List<GameObject>();
    bool spawned_wave = false;
    int minutes;
    int[] default_weights = {7, 2, 1};
    int[] wave_weights = {1, 1, 1};
    int natural_spawn_amount = 1;
    int wave_spawn_amount = 15;
    int old_days = TimeCycle.days;

    public Transform mobParent;
    // Start is called before the first frame update
    void Start()
    {
        minutes = (int)TimeCycle.minutes;
        mob_list.Add(rat);
        mob_list.Add(boar);
        mob_list.Add(wolf);
    }

    // Update is called once per frame
    void Update()
    {
        if(old_days != TimeCycle.days){
            spawned_wave = false;
            old_days = TimeCycle.days;
        }
        if(Input.GetKeyDown(KeyCode.Q)){ // debug
            for (int i = 0; i < 1; i++)
            {
                float spawn_magnitude = Random.Range(50f, 100f);
                float spawn_angle = Random.Range(0f, Mathf.PI * 2);
                Vector3 spawn_pos = new Vector3(Mathf.Cos(spawn_angle), 0, Mathf.Sin(spawn_angle));
                spawn_pos *= spawn_magnitude;
                Vector3 position = GameObject.Find("Player").transform.position;
                position += spawn_pos;
                position.y = 0;
                Instantiate(rat, position, GameObject.Find("Player").transform.rotation, mobParent);
            }
        }
        
        if (TimeCycle.hours == 1 && spawned_wave == false)
        {
            for (int i = 0; i < wave_spawn_amount * Math.Pow(1.1, TimeCycle.days); i++)
            {
                SpawnMob(wave_weights, 1);
            }
            spawned_wave = true;
        }
        /*if (spawned_wave && TimeCycle.hours != 0)
        {
            spawned_wave = false;
        }*/

        
        if(TimeCycle.minutes != minutes)
        {
            for (int i = 0; i < natural_spawn_amount * TimeCycle.days; i++)
            {
                SpawnMob(default_weights, 0.1 * TimeCycle.days);
                minutes = (int)TimeCycle.minutes;
            }

        }
    }

    public void SpawnMob(int[] weights, double success_rate)
    {
        float rand = Random.value;
        while(rand <= success_rate){
            success_rate -= 1;
            int spawn_type = GetRandomWeightedIndex(weights);
            float spawn_magnitude = Random.Range(50f, 100f);
            float spawn_angle = Random.Range(0f, Mathf.PI * 2);
            Vector3 spawn_pos = new Vector3(Mathf.Cos(spawn_angle), 0, Mathf.Sin(spawn_angle));
            spawn_pos *= spawn_magnitude;
            Vector3 position = GameObject.Find("Player").transform.position;
            position += spawn_pos;
            position.y = 0;
            Instantiate(mob_list[spawn_type], position, GameObject.Find("Player").transform.rotation, mobParent);
        }
        
    }

    public int GetRandomWeightedIndex(int[] weights)
    {
        if(weights == null || weights.Length == 0) return -1;

        int sum = 0;
        for(int i = 0; i < weights.Length; i++)
        {
            sum += weights[i];
        }
    
        float r = Random.value;
        float s = 0f;
    
        for(int i = 0; i < weights.Length; i++)
        {
            s += (float)weights[i] / sum;
            if (s >= r) return i;
        }
    
        return -1;
    }
}
