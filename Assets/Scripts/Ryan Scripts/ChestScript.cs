using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    private bool opened = false;
    public Animator animator;

    public ItemList[] dropTable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void openChest(){
        if (!opened){
            animator.SetTrigger("Open");
            //Destroy(gameObject.GetComponent<interactable>());
        }
        opened = true;
    }
}
