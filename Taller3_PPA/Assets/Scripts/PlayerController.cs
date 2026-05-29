using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float forwardSpeed = 10f;
    public float laneDistance = 3f;  
    public float jumpForce = 7f; 

    private Rigidbody rb;
    private int currentLane = 1;
    private bool isGrounded = true; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // MODIFICACIÓN: Envolvemos el cambio de carril dentro de una comprobación de isGrounded
        // Así, el jugador solo puede usar "A" y "D" si está tocando el suelo.
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                currentLane++;
                if (currentLane > 2) currentLane = 2; 
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                currentLane--;
                if (currentLane < 0) currentLane = 0; 
            }
        }

        // El salto se mantiene igual, ya comprobaba isGrounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        
        // El disparo con la tecla F se implementará en una fase posterior.
    }

    void FixedUpdate()
    {
        Vector3 forwardMove = transform.forward * forwardSpeed * Time.fixedDeltaTime;

        // 4. Calcular la posición objetivo en el eje X (los 3 carriles)
        // Carril 0: -laneDistance | Carril 1: 0 | Carril 2: +laneDistance
        float targetXPos = (currentLane - 1) * laneDistance;
        Vector3 targetPosition = new Vector3(targetXPos, rb.position.y, rb.position.z + forwardMove.z);

        // Mover el Rigidbody suavemente hacia la nueva posición
        rb.MovePosition(targetPosition);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}