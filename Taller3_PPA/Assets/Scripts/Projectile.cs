using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Configuración de la Bala")]
    public float speed = 25f;    
    public float lifeTime = 3f;  

     public AudioClip muerteEnemigoClip;

    void Start()
    {
        Destroy(gameObject, lifeTime); 
    }

    void Update()
    {
        
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
         if (other.CompareTag("Enemy"))
        {
            
            EnemyController enemigo = other.    GetComponentInParent<EnemyController>();
            if (enemigo != null)
            enemigo.Morir();
            else
            Destroy(other.gameObject); 

            Destroy(gameObject);
        }
    else if (other.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}