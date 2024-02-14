using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelectVisibility : MonoBehaviour
{
    public GameObject[] guns;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= guns.Length - 1; i++){
            Debug.Log(guns[i].name);
            guns[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (InvController.equipped_item != null){
            for (int i = 0; i <= guns.Length - 1; i++){
                if (InvController.equipped_item.item_data.name.Equals(guns[i].name)){
                    guns[i].SetActive(true);
                    for (int j = 0; j <= guns.Length - 1;j++){
                        if(j != i){
                            guns[j].SetActive(false);
                        }             
                    }
                }
            }
        }
        //Debug.Log(InvController.equipped_item.item_data.name);
        
        if (InvController.equipped_item = null){
            for (int j = 0; j <= guns.Length - 1;j++){
                guns[j].SetActive(false);
                //Debug.Log("kys");
            }
        } 
    }
}
