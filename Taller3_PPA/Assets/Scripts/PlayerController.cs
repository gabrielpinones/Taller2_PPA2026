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
        if (isGrounded)
        {
            // Validamos que exista carril disponible y que NO haya obstáculos en esa dirección
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (currentLane < 2 && !HayObstaculoEnDireccion(Vector3.right))
                {
                    currentLane++;
                }
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (currentLane > 0 && !HayObstaculoEnDireccion(Vector3.left))
                {
                    currentLane--;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        Vector3 forwardMove = transform.forward * forwardSpeed * Time.fixedDeltaTime;

        float targetXPos = (currentLane - 1) * laneDistance;
        Vector3 targetPosition = new Vector3(targetXPos, rb.position.y, rb.position.z + forwardMove.z);

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
        else if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Obstacle"))
        {
            if (GameManager.Instance != null) GameManager.Instance.GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            if (GameManager.Instance != null) GameManager.Instance.AddCoin();
            Destroy(other.gameObject);
        }
    }


    private bool HayObstaculoEnDireccion(Vector3 direccion)
    {
        // Se eleva el origen del rayo 1 unidad para que no detecte el suelo por error
        Vector3 origen = transform.position + Vector3.up * 1f;
        float radioEsfera = 0.4f; // Grosor del jugador simulado
        
        // Lanzamos la proyección a una distancia máxima equivalente a laneDistance
        if (Physics.SphereCast(origen, radioEsfera, direccion, out RaycastHit hit, laneDistance))
        {
            if (hit.collider.CompareTag("Obstacle") || hit.collider.CompareTag("Enemy"))
            {
                return true; 
            }
        }
        return false;
    }
}