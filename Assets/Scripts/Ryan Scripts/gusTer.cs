using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gusTer : MonoBehaviour
{
    private bool opened = false;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void kys(){
        if (!opened){
            animator.SetTrigger("Open");
            //Destroy(gameObject.GetComponent<interactable>());
            Debug.Log("kys");
        }
        opened = true;
    }
}
