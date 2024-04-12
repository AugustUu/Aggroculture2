using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class NewBehaviourScript : MonoBehaviour
{
    Light flashlight;
    // Start is called before the first frame update
    void Start()
    {
        flashlight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Flashlight")){
            flashlight.enabled = !flashlight.enabled;
        }

        if(Input.GetKeyDown(KeyCode.P)){
            if(Time.timeScale != 0){
                Time.timeScale = 0;
            }
            else{
                Time.timeScale = 1;
            }
        }
    }
}