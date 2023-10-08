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
        _animator = tempPlayer.transform.GetChild(0).GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical"); 
    }

    private void FixedUpdate()
    {
        v_movement = _charController.transform.forward * inputZ; 
        
        _charController.transform.Rotate(Vector3.up * inputX * (100f * Time.deltaTime));

        _charController.Move(v_movement * moveSpeed * Time.deltaTime);
    }
}