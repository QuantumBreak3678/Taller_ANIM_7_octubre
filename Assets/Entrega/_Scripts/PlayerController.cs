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
    public float runSpeedMultiplier = 2.0f; // Multiplicador de velocidad para correr
    public float gravity;

    private bool isRunning = false; // Variable para rastrear si el personaje está corriendo

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

        // Verifica si la tecla Shift está presionada
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        // Configura las animaciones según las entradas de movimiento
        if (inputZ != 0)
        {
            if (isRunning)
            {
                _animator.SetBool("isRun", true);
                _animator.SetBool("isWalk", false);
            }
            else
            {
                _animator.SetBool("isRun", false);
                _animator.SetBool("isWalk", true);
            }
        }
        else
        {
            _animator.SetBool("isRun", false);
            _animator.SetBool("isWalk", false);
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

        // Aplica el multiplicador de velocidad si el personaje está corriendo
        float speed = isRunning ? moveSpeed * runSpeedMultiplier : moveSpeed;

        _charController.transform.Rotate(Vector3.up * inputX * (100f * Time.deltaTime));

        _charController.Move(v_movement * speed * Time.deltaTime);
        _charController.Move(v_velocity);
    }
}
