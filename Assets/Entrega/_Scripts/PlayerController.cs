using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _charController;
    private Animator _animator;

    private float inputX;
    private float inputZ;
    public float moveSpeed;
    public float runSpeedMultiplier = 2.0f; // Multiplicador de velocidad para correr
    public float rotationSpeed = 100.0f; // Velocidad de rotación
    public float gravity;

    private bool isRunning = false; // Variable para rastrear si el personaje está corriendo
    
    private Vector3 v_velocity = Vector3.zero;

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

        // Configura los parámetros del Blend Tree
        float speed = Mathf.Abs(inputZ);
        float direction = inputZ > 0 ? inputX : -inputX; // Ajusta la dirección para caminar hacia atrás

        _animator.SetFloat("Speed", speed);
        _animator.SetFloat("Direction", direction);

        // Configura las animaciones de correr si se está corriendo
        if (isRunning)
        {
            _animator.SetFloat("Speed", 1.0f);
            _animator.SetFloat("Direction", 1.0f);
        }
    }
    private void FixedUpdate()
    {
        if (_charController.isGrounded)
        {
            // Reinicia la velocidad vertical cuando está en el suelo
            if (v_velocity.y < 0f)
            {
                v_velocity.y = -2f;
            }
        }
        else
        {
            // Aplica la gravedad cuando está en el aire
            v_velocity.y -= gravity * Time.deltaTime;
        }

        // Calcula el movimiento del personaje
        Vector3 v_movement = transform.forward * inputZ;

        // Aplica la rotación del personaje
        transform.Rotate(Vector3.up * inputX * (rotationSpeed * Time.deltaTime));

        // Aplica el movimiento al CharacterController
        _charController.Move(v_movement * moveSpeed * Time.deltaTime);
        _charController.Move(v_velocity * Time.deltaTime);
    }
}
