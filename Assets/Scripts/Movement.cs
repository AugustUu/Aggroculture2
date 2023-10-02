using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{



    public CharacterController controller;
    private Vector3 player_velocity;
    private bool grounded_player;


    public float player_speed = 2.0f;

    public new Camera camera;

    private float rotation = 0;
    private float mouse_start = 0;


    private void Start()
    {
    }

    void Update()
    {
        grounded_player = controller.isGrounded;
        if (grounded_player && player_velocity.y < 0)
        {
            player_velocity.y = 0f;
        }

        Vector3 player_screen_pos = camera.WorldToScreenPoint(this.transform.position);
        Vector3 mouse_pos = Input.mousePosition;

        if (Input.GetButtonDown("Aim"))
        {
            mouse_start = mouse_pos.x;
        }
        else if (Input.GetButton("Aim"))
        {
            float mouse_pos_reletive = mouse_pos.x - mouse_start;
            rotation = Mathf.Atan2(player_screen_pos.x - mouse_pos_reletive, player_screen_pos.x - mouse_pos_reletive);
            this.transform.rotation = Quaternion.Euler(0, (float)(rotation * 180f * Math.PI), 0);
            
            Debug.Log(player_screen_pos - mouse_pos);
        }



        //transform.rotation.y += 1;

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * player_speed;
        controller.Move(move);

    }

}
