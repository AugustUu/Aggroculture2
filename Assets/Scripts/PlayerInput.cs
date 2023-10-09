using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput1 : MonoBehaviour
{
    // Start is called before the first frame update

    public InputActionAsset player_input;

    void Start()
    {
        player_input.Enable();
    }

    void OnDestroy(){
        player_input.Disable();
    }
}
