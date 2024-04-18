using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    // Update is called once per frame
    public static bool is_paused = false;
    public InvManager inv_manager;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            if(!is_paused){
                Time.timeScale = 0;
                is_paused = true;
                inv_manager.SetInvActive(false);
            }
            else{
                Time.timeScale = 1;
                is_paused = false;
            }
        }
    }
}
