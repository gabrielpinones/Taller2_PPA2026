using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Configuración de la Bala")]
    public float speed = 25f;     // Rápida: favorece al jugador (zombies van a ~3)
    public float lifeTime = 3f;   // Se autodestruye si no impacta nada

    void Start()
    {
        Destroy(gameObject, lifeTime); // Limpieza de seguridad
    }

    void Update()
    {
        
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Obstacle"))
        {
            // TODO audio: sonido de enemigo al morir
            Destroy(other.gameObject); // Destruye enemigo u obstáculo
            Destroy(gameObject);       // La bala se destruye al impactar
        }
    }
}