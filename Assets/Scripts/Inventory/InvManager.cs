using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class InvManager : MonoBehaviour
{
    [SerializeField] GameObject inv_container;
    [SerializeField] Trash trash;

    void Start(){
        ToggleInv();
    }
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            ToggleInv();
        }
        if (Input.GetButtonDown("schmeezer"))
        {
            GetComponent<InvController>().InsertItemID(ItemList.schmeeze);   
        }
    }

    public void ToggleInv(){
        SetInvActive(!inv_container.activeSelf);
    }
    public void SetInvActive(bool active){
        if(!active){
            gameObject.GetComponent<InvController>().selected_item_grid = null;
            gameObject.GetComponent<InvController>().trashing = false;
            trash.Close();
        }
        inv_container.SetActive(active);
    }

    
}
