using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCrop : MonoBehaviour
{
    public Object[] textures;
    private bool gh = false;
    // Start is called before the first frame update
    void Start()
    {
        textures = Resources.LoadAll("Plants", typeof(GameObject));
 
        foreach (var t in textures)
        {
            //Debug.Log(t.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        FarmlandScript script = gameObject.GetComponent<FarmlandScript>();
        foreach (var t in textures)
        {
            if (script.plant.ToString().Equals(t.name)){
                Debug.Log("kys");
                if (!gh){
                    Instantiate(t, new Vector3 (this.transform.position.x, 1, this.transform.position.z), Quaternion.Euler(0, 0, 0),gameObject.GetComponent<Transform>());
                }
                gh=true;
            }
        }
        
        //Instantiate(mob_list[spawn_type], position, GameObject.Find("Player").transform.rotation, mobParent);
    }
}
