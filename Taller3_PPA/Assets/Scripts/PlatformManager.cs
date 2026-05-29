using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gestiona la creación y destrucción de plataformas para simular un camino infinito.
/// </summary>
public class PlatformManager : MonoBehaviour
{
    [Header("Configuración de Plataformas")]
    public GameObject platformPrefab; // Arrastra tu Prefab de Suelo aquí
    public Transform player;          // Arrastra a tu Jugador aquí
    public int initialPlatforms = 15; // Cantidad de plataformas simultáneas (mín 10, máx 30 según el documento)
    public float platformLength = 20f;// Reemplaza por lo que mide TU plataforma en el eje Z

    private float spawnZ = 0f;        // La posición Z donde aparecerá la siguiente plataforma
    private float safeZone = 25f;     // Distancia de margen antes de borrar la plataforma que quedó atrás
    
    // Una "Cola" (Queue) es perfecta para esto: el primero en entrar es el primero en salir (FIFO)
    private Queue<GameObject> activePlatforms = new Queue<GameObject>();

    void Start()
    {
        // Generar las plataformas iniciales al arrancar la partida
        for (int i = 0; i < initialPlatforms; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        // Comprobar si el jugador ha avanzado lo suficiente para requerir una nueva plataforma.
        // Restamos (initialPlatforms * platformLength) para calcular dónde está el inicio lógico de la pista.
        if (player.position.z > (spawnZ - (initialPlatforms * platformLength) + safeZone))
        {
            SpawnPlatform();
            DeletePlatform();
        }
    }

    /// <summary>
    /// Instancia una plataforma al final del camino actual.
    /// </summary>
    private void SpawnPlatform()
    {
        // Instanciamos el Prefab en la posición actual de spawnZ
        GameObject go = Instantiate(platformPrefab, new Vector3(0, 0, spawnZ), Quaternion.identity);
        
        // Lo añadimos a la cola para llevar el registro
        activePlatforms.Enqueue(go);
        
        // Sumamos la longitud de la plataforma para que la siguiente aparezca justo al borde de esta
        spawnZ += platformLength; 
    }

    /// <summary>
    /// Elimina la plataforma más antigua (la que quedó atrás).
    /// </summary>
    private void DeletePlatform()
    {
        // Sacamos la plataforma más antigua de la cola
        GameObject oldPlatform = activePlatforms.Dequeue();
        
        // Destruimos el objeto del juego. Sus "hijos" (enemigos, obstáculos) se destruirán con ella
        Destroy(oldPlatform);
    }
}