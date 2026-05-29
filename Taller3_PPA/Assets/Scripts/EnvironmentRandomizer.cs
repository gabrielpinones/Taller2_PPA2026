using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentRandomizer : MonoBehaviour
{
    [Header("Contenedores de Decoración")]
    public Transform contenedorIzquierdo;
    public Transform contenedorDerecho;

    void Start()
    {
        ConfigurarLado(contenedorIzquierdo);
        ConfigurarLado(contenedorDerecho);
    }

    private void ConfigurarLado(Transform contenedor)
    {
        // Validación de seguridad
        if (contenedor == null || contenedor.childCount == 0) return;

        // 1. Desactivar todos los edificios del contenedor
        for (int i = 0; i < contenedor.childCount; i++)
        {
            contenedor.GetChild(i).gameObject.SetActive(false);
        }

        // 2. Elegir y activar solo un edificio aleatorio
        int indiceAleatorio = Random.Range(0, contenedor.childCount);
        contenedor.GetChild(indiceAleatorio).gameObject.SetActive(true);

    }
}