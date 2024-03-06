using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelectVisibility : MonoBehaviour
{
    public GameObject[] tools;
    public Animator animator;
    public bool seeEquippedItem;
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
            if (seeEquippedItem)
            {
                Debug.Log(InvController.equipped_item.item_data.name);
            }
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
            bool holdTool = InvController.equipped_item.item_data.item_type.ToString().Equals("Gun") || InvController.equipped_item.item_data.item_type.ToString().Equals("Tool");
            if (holdTool)
            {
                animator.SetBool("HoldingTool", true);
            }
            if (!holdTool)
            {
                animator.SetBool("HoldingTool", false);
            }
        }
        if (InvController.equipped_item == null){
            animator.SetBool("HoldingTool", false);
            for (int j = 0; j <= tools.Length - 1;j++){
                tools[j].SetActive(false);
            }
        }
    }
}
