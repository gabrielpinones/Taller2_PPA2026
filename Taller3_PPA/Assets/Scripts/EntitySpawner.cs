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
        
    }
    public void Generar(bool soloMonedas = false)
    {
    GenerarEntidadesMandatorias(soloMonedas);
    }

    private void GenerarEntidadesMandatorias(bool soloMonedas)
{
    if (spawnPoints.Length < 3) return;

    int cantidadAGenerar = Random.Range(1, 3);
    List<int> carrilesDisponibles = new List<int> { 0, 1, 2 };

    for (int i = 0; i < cantidadAGenerar; i++)
    {
        int indiceLista = Random.Range(0, carrilesDisponibles.Count);
        int carrilElegido = carrilesDisponibles[indiceLista];
        carrilesDisponibles.RemoveAt(indiceLista);

        Transform puntoElegido = spawnPoints[carrilElegido];
        GameObject prefabElegido = null;

        if (soloMonedas)
        {
            // Zona segura de inicio: solo monedas (no matan al jugador)
            if (monedasPrefabs.Length > 0)
                prefabElegido = monedasPrefabs[Random.Range(0, monedasPrefabs.Length)];
        }
        else
        {
            float categoriaRandom = Random.value;

            if (categoriaRandom < 0.5f) // 50% Obstáculos
            {
                bool spawnVehiculo = (Random.value > 0.5f);
                if (spawnVehiculo && vehiculosPrefabs.Length > 0)
                    prefabElegido = vehiculosPrefabs[Random.Range(0, vehiculosPrefabs.Length)];
                else if (!spawnVehiculo && otrosObstaculosPrefabs.Length > 0)
                    prefabElegido = otrosObstaculosPrefabs[Random.Range(0, otrosObstaculosPrefabs.Length)];
                else if (vehiculosPrefabs.Length > 0)
                    prefabElegido = vehiculosPrefabs[Random.Range(0, vehiculosPrefabs.Length)];
                else if (otrosObstaculosPrefabs.Length > 0)
                    prefabElegido = otrosObstaculosPrefabs[Random.Range(0, otrosObstaculosPrefabs.Length)];
            }
            else if (categoriaRandom < 0.75f) // 25% Enemigos
            {
                if (enemigosPrefabs.Length > 0)
                    prefabElegido = enemigosPrefabs[Random.Range(0, enemigosPrefabs.Length)];
            }
            else // 25% Monedas
            {
                if (monedasPrefabs.Length > 0)
                    prefabElegido = monedasPrefabs[Random.Range(0, monedasPrefabs.Length)];
            }
        }

        if (prefabElegido != null)
        {
            Vector3 posicionFinal = puntoElegido.position;
            Quaternion rotacionFinal = puntoElegido.rotation;

            bool esVehiculo = System.Array.IndexOf(vehiculosPrefabs, prefabElegido) > -1;
            bool esMoneda   = System.Array.IndexOf(monedasPrefabs, prefabElegido) > -1;

            if (esVehiculo)
            {
                float rotY = (Random.value > 0.5f) ? 0f : 180f;
                if (Random.value < 0.3f)
                {
                    bool flipEnX = (Random.value > 0.5f);
                    rotacionFinal = Quaternion.Euler(flipEnX ? 180f : 0f, rotY, flipEnX ? 0f : 180f);
                }
                else
                {
                    rotacionFinal = Quaternion.Euler(0f, rotY, 0f);
                }
            }
            else if (esMoneda)
            {
                posicionFinal.y = 1f; // Moneda flotando, recolectable al correr
            }

            GameObject entidad = Instantiate(prefabElegido, posicionFinal, rotacionFinal);
            entidad.transform.SetParent(this.transform, true);

            // Apoyar vehículos sobre el suelo según su tamaño real
            if (esVehiculo)
            {
                Renderer[] renders = entidad.GetComponentsInChildren<Renderer>();
                if (renders.Length > 0)
                {
                    Bounds b = renders[0].bounds;
                    for (int r = 1; r < renders.Length; r++) b.Encapsulate(renders[r].bounds);
                    float offsetY = puntoElegido.position.y - b.min.y;
                    entidad.transform.position += Vector3.up * offsetY;
                }
            }
        }
    }
}
}

        
    
        