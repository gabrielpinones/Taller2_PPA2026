using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float speed = 3f; // Velocidad de caminata del zombie
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Rotar el modelo 180 grados en Y para que mire de frente al jugador
        transform.rotation = Quaternion.Euler(0, 180f, 0);
    }

    void FixedUpdate()
    {
        // Vector3.back mueve el objeto hacia -Z (dirección opuesta al Player)
        Vector3 movement = Vector3.back * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }
}
