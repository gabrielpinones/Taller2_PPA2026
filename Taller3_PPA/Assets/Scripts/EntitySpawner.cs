using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [Header("Puntos de Aparición")]
    public Transform[] spawnPoints;

    [Header("Variantes de Prefabs")]
    public GameObject[] vehiculosPrefabs;       
    public GameObject[] otrosObstaculosPrefabs; 
    public GameObject[] enemigosPrefabs;        
    public GameObject[] monedasPrefabs;         

    void Start()
    {
        GenerarEntidadesMandatorias();
    }

    private void GenerarEntidadesMandatorias()
    {
        if (spawnPoints.Length < 3) return;

        // 1. Determinar cuántos objetos saldrán en esta plataforma (mínimo 1, máximo 2)
        int cantidadAGenerar = Random.Range(1, 3); 

        // 2. Lista de índices de carriles (0 = Izq, 1 = Cen, 2 = Der) para evitar duplicados
        List<int> carrilesDisponibles = new List<int> { 0, 1, 2 };

        for (int i = 0; i < cantidadAGenerar; i++)
        {
            // Seleccionar un carril al azar de los que quedan disponibles y removerlo de la lista
            int indiceLista = Random.Range(0, carrilesDisponibles.Count);
            int carrilElegido = carrilesDisponibles[indiceLista];
            carrilesDisponibles.RemoveAt(indiceLista);

            Transform puntoElegido = spawnPoints[carrilElegido];
            GameObject prefabElegido = null;

            // 3. Determinar la categoría del objeto por probabilidad
            float categoriaRandom = Random.value;

            if (categoriaRandom < 0.5f) // 50% Obstáculos
            {
                bool spawnVehiculo = (Random.value > 0.5f);

                if (spawnVehiculo && vehiculosPrefabs.Length > 0)
                {
                    prefabElegido = vehiculosPrefabs[Random.Range(0, vehiculosPrefabs.Length)];
                }
                else if (!spawnVehiculo && otrosObstaculosPrefabs.Length > 0)
                {
                    prefabElegido = otrosObstaculosPrefabs[Random.Range(0, otrosObstaculosPrefabs.Length)];
                }
                else if (vehiculosPrefabs.Length > 0)
                {
                    prefabElegido = vehiculosPrefabs[Random.Range(0, vehiculosPrefabs.Length)];
                }
                else if (otrosObstaculosPrefabs.Length > 0)
                {
                    prefabElegido = otrosObstaculosPrefabs[Random.Range(0, otrosObstaculosPrefabs.Length)];
                }
            }
            else if (categoriaRandom < 0.75f) // 25% Enemigos
            {
                if (enemigosPrefabs.Length > 0)
                {
                    prefabElegido = enemigosPrefabs[Random.Range(0, enemigosPrefabs.Length)];
                }
            }
            else // 25% Monedas
            {
                if (monedasPrefabs.Length > 0)
                {
                    prefabElegido = monedasPrefabs[Random.Range(0, monedasPrefabs.Length)];
                }
            }

            // 4. Instanciación y configuración de transformación
            if (prefabElegido != null)
            {
                Vector3 posicionFinal = puntoElegido.position;
                Quaternion rotacionFinal = puntoElegido.rotation;

                // Aplicar altura y rotación de choque si es un vehículo
                if (System.Array.IndexOf(vehiculosPrefabs, prefabElegido) > -1)
                {
                    posicionFinal.y += 1.5f;

                    float rotX = Random.Range(0, 4) * 90f; 
                    float rotZ = Random.Range(0, 4) * 90f;
                    float rotY = Random.Range(0f, 360f);   
                    
                    rotacionFinal = Quaternion.Euler(rotX, rotY, rotZ);
                }

                GameObject entidad = Instantiate(prefabElegido, posicionFinal, rotacionFinal);
                entidad.transform.SetParent(this.transform);
            }
        }
    }
}
