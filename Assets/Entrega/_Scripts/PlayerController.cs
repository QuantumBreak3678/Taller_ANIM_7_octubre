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
    public float animationSmoothTime = 0.1f; // Valor de suavizado para las transiciones de animación

    private Vector3 v_velocity = Vector3.zero; // Variable para la velocidad vertical
    private float speedSmoothVelocity; // Variable de velocidad suavizada

    void Start()
    {
        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
        _charController = tempPlayer.GetComponent<CharacterController>();
        _animator = tempPlayer.transform.GetComponent<Animator>();

        Cursor.visible = false; 
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        // Configura el parámetro Speed del Blend Tree con suavizado
        float targetSpeed = 0f;

        if (inputZ != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                targetSpeed = Mathf.Sign(inputZ);
            }
            else
            {
                targetSpeed = Mathf.Sign(inputZ) * 0.5f;
            }
        }

        float smoothedSpeed = Mathf.SmoothDamp(_animator.GetFloat("Speed"), targetSpeed, ref speedSmoothVelocity, animationSmoothTime);
        float direction = inputX;

        _animator.SetFloat("Speed", smoothedSpeed);
        _animator.SetFloat("Direction", direction);
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
        float speed = Input.GetKey(KeyCode.LeftShift) ? moveSpeed * runSpeedMultiplier : moveSpeed;
        _charController.Move(v_movement * speed * Time.deltaTime);
        _charController.Move(v_velocity * Time.deltaTime);
    }
}
