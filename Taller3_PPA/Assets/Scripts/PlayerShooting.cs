using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Configuración de Disparo")]
    public GameObject projectilePrefab; // Prefab de la bala
    public Transform firePoint;         // Desde dónde sale la bala
    public float fireCooldown = 0.5f;   // Intervalo mínimo entre disparos

    private float nextFireTime = 0f;

    void Update()
    {
        // Si el juego está pausado/terminado (timeScale 0), no dispara
        if (Time.timeScale == 0f) return;

        if (Input.GetKeyDown(KeyCode.F) && Time.time >= nextFireTime)
        {
            Disparar();
            nextFireTime = Time.time + fireCooldown;
        }
    }

    private void Disparar()
    {
        Vector3 origen = (firePoint != null) ? firePoint.position : transform.position;
        Quaternion rot = (firePoint != null) ? firePoint.rotation : transform.rotation;

        Instantiate(projectilePrefab, origen, rot);
        // TODO audio: sonido de disparo
    }
}