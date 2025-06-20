using UnityEngine;
using System.Collections;

namespace Eco
{
    public class ProceduralSpawner : MonoBehaviour
    {
        [Header("Prefabs a instanciar")]
        [SerializeField] private GameObject[] prefabs;

        [Header("Parámetros de generación")]
        [SerializeField] private int cantidad = 5;
        [SerializeField] private Vector2 zonaMin;
        [SerializeField] private Vector2 zonaMax;
        [SerializeField] private float retardoEntreSpawns = 0.3f;
        [SerializeField] private float distanciaMinimaJugador = 3f;

        private Transform jugador;

        private void Start()
        {
            GameObject jugadorGO = GameObject.FindGameObjectWithTag("Player");
            if (jugadorGO != null)
                jugador = jugadorGO.transform;

            StartCoroutine(GenerarObjetosConRetardo());
        }

        private IEnumerator GenerarObjetosConRetardo()
        {
            int generados = 0;

            while (generados < cantidad)
            {
                Vector2 posicion = new Vector2(
                    Random.Range(zonaMin.x, zonaMax.x),
                    Random.Range(zonaMin.y, zonaMax.y)
                );

                if (jugador != null && Vector2.Distance(jugador.position, posicion) < distanciaMinimaJugador)
                {
                    // Si está demasiado cerca del jugador, saltamos este intento
                    yield return null;
                    continue;
                }

                int index = Random.Range(0, prefabs.Length);
                Instantiate(prefabs[index], posicion, Quaternion.identity);

                generados++;
                yield return new WaitForSeconds(retardoEntreSpawns);
            }
        }
    }
}
