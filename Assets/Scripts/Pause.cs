using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    // Update is called once per frame
    public static bool is_paused = false;
    static bool force_paused = false;
    public InvManager inv_manager;
    static InvManager inv_manager_static; //I HATE DOING THIS

    void Start(){
        inv_manager_static = inv_manager;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !force_paused){
            if(!is_paused){
                PauseGame(true, false);
            }
            else{
                PauseGame(false, false);
            }
        }
    }

    public static void ForcePause(bool pause, bool inv){
        force_paused = pause;
        PauseGame(pause, inv);
    }

    private static void PauseGame(bool pause, bool inv){
        if(pause){
            Time.timeScale = 0;
            is_paused = true;
            inv_manager_static.InvTempToggle(inv);
        }
        else{
            Time.timeScale = 1;
            is_paused = false;
            inv_manager_static.SetInvActive(inv_manager_static.inv_active);
        }
    }
}
