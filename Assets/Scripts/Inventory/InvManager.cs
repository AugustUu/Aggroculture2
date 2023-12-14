using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class InvManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            ToggleInv();
        }
        if (Input.GetButtonDown("schmeezer"))
        {
            GetComponent<InvController>().InsertItemID(0);   
        }
    }

    public void ToggleInv(){
        SetInvActive(!gameObject.transform.GetChild(0).gameObject.activeSelf);
    }
    public void SetInvActive(bool active){
        gameObject.transform.GetChild(0).gameObject.SetActive(active);
    }
}
