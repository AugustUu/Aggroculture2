using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{



    public CharacterController controller;
    public new Camera camera;

    public Transform model_transform;
    public float player_speed = 10.0f;

    public int sens = 10;

    private Vector3 player_velocity;
    private bool grounded_player;

    public static float rotation = 0;
    private float mouse_start = 0;

    public Animator animator;

    private void Start()
    {
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        
        grounded_player = controller.isGrounded;
        if (grounded_player && player_velocity.y < 0)
        {
            player_velocity.y = 0f;
        }

        Vector3 player_screen_pos = camera.WorldToScreenPoint(transform.position);
        Vector3 mouse_pos = Input.mousePosition;

        if (Input.GetButtonDown("Drag"))
        {
            mouse_start = mouse_pos.x;

        }
        else if (Input.GetButton("Drag"))
        {
            transform.Rotate(new Vector3(0, (mouse_pos.x - mouse_start) / sens, 0));
            mouse_start = mouse_pos.x;
        }

        if (Input.GetButton("Aim") || Input.GetMouseButton(0))
        {
            rotation = Mathf.Atan2(player_screen_pos.x - mouse_pos.x, player_screen_pos.y - mouse_pos.y);
            rotation += transform.rotation.eulerAngles.y / Mathf.Rad2Deg + Mathf.PI;
        }
        else if (direction != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
            rotation = Mathf.Atan2(direction.x, direction.z);
            rotation += transform.rotation.eulerAngles.y / Mathf.Rad2Deg;
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetBool("IsDead", true);
        }




        model_transform.rotation = Quaternion.Lerp(model_transform.rotation, Quaternion.Euler(0, (float)(rotation * Mathf.Rad2Deg), 0), Time.deltaTime * 8);
        Vector3 move = direction * Time.deltaTime * player_speed;
        
        controller.Move(this.transform.TransformDirection(move));

    }

}