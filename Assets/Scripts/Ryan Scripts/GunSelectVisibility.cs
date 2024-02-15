using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelectVisibility : MonoBehaviour
{
    public GameObject[] tools;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= tools.Length - 1; i++){
            Debug.Log(tools[i].name);
            tools[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (InvController.equipped_item != null){
            Debug.Log(InvController.equipped_item.item_data.name);
            for (int i = 0; i <= tools.Length - 1; i++){
                if (InvController.equipped_item.item_data.name.Equals(tools[i].name)){
                    tools[i].SetActive(true);
                    for (int j = 0; j <= tools.Length - 1;j++){
                        if(j != i){
                            tools[j].SetActive(false);
                        }             
                    }
                }
            }
        }
        
        if (InvController.equipped_item == null){
            for (int j = 0; j <= tools.Length - 1;j++){
                tools[j].SetActive(false);
            }
        }
    }
}
