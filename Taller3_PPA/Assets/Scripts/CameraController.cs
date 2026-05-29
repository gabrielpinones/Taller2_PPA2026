using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Referencias")]
    public Transform player; 

    private float offsetZ; // La distancia de separación en el eje Z

    void Start()
    {

        offsetZ = transform.position.z - player.position.z;
    }

 
    void LateUpdate()
    {
        if (player != null)
        {
            // Mantiene las posiciones X e Y originales, pero actualiza la Z siguiendo al jugador
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, player.position.z + offsetZ);
            transform.position = newPosition;
        }
    }
}
