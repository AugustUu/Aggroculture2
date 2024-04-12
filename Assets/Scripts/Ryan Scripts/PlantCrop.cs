using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlantCrop : MonoBehaviour
{
    public UnityEngine.Object[] plantModels;
    private bool planted = false;
    Transform plantSpawn;
    float randRotation;
    int tempGrow;
    // Start is called before the first frame update
    void Start()
    {
        plantModels = Resources.LoadAll("Plants", typeof(GameObject));
        randRotation = UnityEngine.Random.Range(0,360);
        foreach (var t in plantModels)
        {
            Debug.Log(t.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        FarmlandScript script = gameObject.GetComponent<FarmlandScript>();
        foreach (var t in plantModels)
        {
            if (script.plant.ToString().Equals(t.name)){
                if (!planted){
                    plantSpawn = Instantiate(t.GetComponent<Transform>().Find("Stage 1"), new Vector3 (this.transform.position.x, 1, this.transform.position.z), Quaternion.Euler(-90, randRotation, 0),gameObject.GetComponent<Transform>());
                    plantSpawn.transform.localScale = new Vector3(350, 350, 350);
                }
                planted = true;
            }
        }
        if (tempGrow != script.growth){
            growStage();
        }
        tempGrow = script.growth;
    }

    public void growStage(){
        FarmlandScript script = gameObject.GetComponent<FarmlandScript>();
        if (script.growth == (int)(Utils.plant_list[(int) script.plant].grow_time / 3)){
            foreach (var t in plantModels)
            {
                if (script.plant.ToString().Equals(t.name)){
                    //Destroy(plantSpawn.GetComponent<GameObject>(),2.0f);
                    Transform plantSpawn2 = Instantiate(t.GetComponent<Transform>().Find("Stage 2"), new Vector3 (this.transform.position.x, 1, this.transform.position.z), Quaternion.Euler(-90, randRotation, 0),gameObject.GetComponent<Transform>());
                    plantSpawn2.transform.localScale = new Vector3(350, 350, 350);
                    Destroy(this.transform.GetChild(1).GameObject());
                }
            }
        }
        if (script.growth == (int)(Utils.plant_list[(int) script.plant].grow_time * 2 / 3)){
            foreach (var t in plantModels)
            {
                if (script.plant.ToString().Equals(t.name)){
                    plantSpawn = Instantiate(t.GetComponent<Transform>().Find("Stage 3"), new Vector3 (this.transform.position.x, 1, this.transform.position.z), Quaternion.Euler(-90, randRotation, 0),gameObject.GetComponent<Transform>());
                    plantSpawn.transform.localScale = new Vector3(350, 350, 350);
                    Destroy(this.transform.GetChild(1).GameObject());
                }
            }
        }
        if (script.growth == Utils.plant_list[(int) script.plant].grow_time){
            foreach (var t in plantModels)
            {
                if (script.plant.ToString().Equals(t.name)){
                    plantSpawn = Instantiate(t.GetComponent<Transform>().Find("Stage 4"), new Vector3 (this.transform.position.x, 1, this.transform.position.z), Quaternion.Euler(-90, randRotation, 0),gameObject.GetComponent<Transform>());
                    plantSpawn.transform.localScale = new Vector3(350, 350, 350);
                    Destroy(this.transform.GetChild(1).GameObject());
                }
            }
        }
    }
    public void shovelPlant(){
        Destroy(this.transform.GetChild(1).GameObject());
        planted = false;
    }
}
