using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
public class MobSpawn : MonoBehaviour
{

    [Serializable]
    public class Mob
    {
        public GameObject game_object;
        public int day_weight = 0;
        public int night_weight = 0;
    }
    
    
    public List<Mob> mob_list;
    public GameObject boss;
    int minutes;

    public Transform mobParent;
    // Start is called before the first frame update
    void Start()
    {
        minutes = (int)TimeCycle.minutes;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){ // debug
            for (int i = 0; i < 1; i++)
            {
                SpawnMob(true, 1);
            }
        }
        /*if (spawned_wave && TimeCycle.hours != 0)
        {
            spawned_wave = false;
        }*/

        
        if(TimeCycle.minutes != minutes)
        {
            if(TimeCycle.hours >= 5 && TimeCycle.hours <= 19){
                SpawnMob(true, 0.1 * TimeCycle.days);
            }
            else{
                SpawnMob(false, 0.2 * TimeCycle.days);
                if(TimeCycle.hours == 20 && TimeCycle.minutes == 0){
                    RandomSpawn(boss);
                }
            }
            minutes = (int)TimeCycle.minutes;
        }
    }

    public void SpawnMob(bool day, double success_rate)
    {
        float rand = Random.value;
        while(rand <= success_rate){
            success_rate -= 1;
            int spawn_type = GetRandomWeightedIndex(day);
            RandomSpawn(mob_list[spawn_type].game_object);
        }
    }

    public void RandomSpawn(GameObject mob){
        float spawn_magnitude = Random.Range(50f, 100f);
        float spawn_angle = Random.Range(0f, Mathf.PI * 2);
        Vector3 spawn_pos = new Vector3(Mathf.Cos(spawn_angle), 0, Mathf.Sin(spawn_angle));
        spawn_pos *= spawn_magnitude;
        Vector3 position = GameObject.Find("Player").transform.position;
        position += spawn_pos;
        position.y = 0;
        Instantiate(mob, position, GameObject.Find("Player").transform.rotation, mobParent);
    }

    public int GetRandomWeightedIndex(bool day)
    {
        List<int> weights = new List<int>(mob_list.Count);
        foreach (var mob in mob_list)
        {
            if (day)
            {
                weights.Add(mob.day_weight);
            }
            else
            {
                weights.Add(mob.night_weight);
            }
        }
        
        int sum = 0;
        for(int i = 0; i < weights.Count; i++)
        {
            sum += weights[i];
        }
    
        float r = Random.value;
        float s = 0f;
    
        for(int i = 0; i < weights.Count; i++)
        {
            s += (float)weights[i] / sum;
            if (s >= r) return i;
        }
    
        return -1;
    }
}
