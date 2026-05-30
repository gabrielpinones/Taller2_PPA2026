using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [Header("Puntos de Aparición")]
    public Transform[] spawnPoints;

    [Header("Variantes de Prefabs")]
    public GameObject[] vehiculosPrefabs;       // Autos, camionetas
    public GameObject[] otrosObstaculosPrefabs; // Cajas, basureros, grifos
    public GameObject[] enemigosPrefabs;        // Zombies
    public GameObject[] monedasPrefabs;         // Monedas

    void Start()
    {
        GenerarEntidadAleatoria();
    }

    private void GenerarEntidadAleatoria()
    {
        if (spawnPoints.Length == 0) return;

        // 80% de probabilidad de generar algo en esta plataforma
        if (Random.value <= 0.8f)
        {
            Transform puntoElegido = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject prefabElegido = null;

            // Determinar la categoría principal
            float categoriaRandom = Random.value;

            if (categoriaRandom < 0.5f) // 50% de probabilidad de Obstáculo
            {
                // Decidir internamente si será un vehículo (50%) o un elemento urbano (50%)
                bool spawnVehiculo = (Random.value > 0.5f);

                if (spawnVehiculo && vehiculosPrefabs.Length > 0)
                {
                    prefabElegido = vehiculosPrefabs[Random.Range(0, vehiculosPrefabs.Length)];
                }
                else if (!spawnVehiculo && otrosObstaculosPrefabs.Length > 0)
                {
                    prefabElegido = otrosObstaculosPrefabs[Random.Range(0, otrosObstaculosPrefabs.Length)];
                }
                // Fallbacks en caso de que uno de los arreglos esté vacío en el Inspector
                else if (vehiculosPrefabs.Length > 0)
                {
                    prefabElegido = vehiculosPrefabs[Random.Range(0, vehiculosPrefabs.Length)];
                }
                else if (otrosObstaculosPrefabs.Length > 0)
                {
                    prefabElegido = otrosObstaculosPrefabs[Random.Range(0, otrosObstaculosPrefabs.Length)];
                }
            }
            else if (categoriaRandom < 0.75f) 
            {
                if (enemigosPrefabs.Length > 0)
                {
                    prefabElegido = enemigosPrefabs[Random.Range(0, enemigosPrefabs.Length)];
                }
            }
            else 
            {
                if (monedasPrefabs.Length > 0)
                {
                    prefabElegido = monedasPrefabs[Random.Range(0, monedasPrefabs.Length)];
                }
            }

            if (prefabElegido != null)
            {
                GameObject entidad = Instantiate(prefabElegido, puntoElegido.position, puntoElegido.rotation);
                entidad.transform.SetParent(this.transform);
            }
        }
    }
}
