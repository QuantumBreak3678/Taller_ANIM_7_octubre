using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _charController;
    private Animator _animator;

    private float inputX;
    private float inputZ;
    private Vector3 v_movement; 
    private Vector3 v_velocity;
    public float moveSpeed;
    public float gravity; 

    void Start()
    {
        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
        _charController = tempPlayer.GetComponent<CharacterController>();
        _animator = tempPlayer.transform.GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        if (inputZ == 0)
        {
            _animator.SetBool("isRun", false);

            if (inputX > 0 || inputX < 0)
            {
                _animator.SetBool("isWalk", true);
            }
            else
            {
                _animator.SetBool("isWalk", false);
            }
        }
        else
        {
            _animator.SetBool("isRun", true);
        }
    }

    private void FixedUpdate()
    {
        if (_charController.isGrounded)
        {
            v_velocity.y = 0f;
        }
        else
        {
            v_velocity.y -= gravity * Time.deltaTime; 
        }
        v_movement = _charController.transform.forward * inputZ; 
        
        _charController.transform.Rotate(Vector3.up * inputX * (100f * Time.deltaTime));

        _charController.Move(v_movement * moveSpeed * Time.deltaTime);
        _charController.Move(v_velocity);
    }
}