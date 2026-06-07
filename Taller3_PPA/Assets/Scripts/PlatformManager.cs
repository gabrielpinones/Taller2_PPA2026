using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [Header("Configuración de Plataformas")]
    public GameObject platformPrefab; 
    public Transform player; 
    public int initialPlatforms = 20; 
    public float platformLength = 20f;

    private float spawnZ = 0f;        // La posición Z donde aparecerá la siguiente plataforma
    private float safeZone = 25f;     // Distancia de margen antes de borrar la plataforma que quedó atrás

    public int safeStartPlatforms = 2; // primeras N plataformas: solo monedas
    private int platformsSpawned = 0;
    

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

        if (player.position.z > (spawnZ - (initialPlatforms * platformLength) + safeZone))
        {
            SpawnPlatform();
            DeletePlatform();
        }
    }

    private void SpawnPlatform()
    {

         GameObject go = Instantiate(platformPrefab, new Vector3(0, 0, spawnZ), Quaternion.identity);

        EntitySpawner spawner = go.GetComponentInChildren<EntitySpawner>();
        if (spawner != null)
        {
            bool zonaSegura = (platformsSpawned < safeStartPlatforms);
            spawner.Generar(zonaSegura); // las primeras solo monedas
        }
        platformsSpawned++;

        activePlatforms.Enqueue(go);
        spawnZ += platformLength;
    }


    private void DeletePlatform()
    {
        // Sacamos la plataforma más antigua de la cola
        GameObject oldPlatform = activePlatforms.Dequeue();
        

        Destroy(oldPlatform);
    }
}