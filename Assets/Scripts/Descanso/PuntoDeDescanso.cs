using UnityEngine;
using TMPro;

namespace Eco
{
    public class PuntoDeDescanso : MonoBehaviour
    {
        private bool jugadorCerca = false;

        [Header("Texto de interacción")]
        public GameObject textoInteraccion;

        private GameObject jugador;

        void Update()
        {
            if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
            {
                // Restaurar vida
                if (jugador != null)
                {
                    var controller = jugador.GetComponent<Eco.PlayerCharacterController>();
                    if (controller != null)
                    {
                        controller.CurarAlMaximo();
                    }
                }

                // Restaurar medicina
                InventoryManager.Instance.RestaurarMedicina();

                Debug.Log("Has descansado en el Obelisco del Recuerdo.");

                // Abrir menú del Obelisco
                UIManager.Instance.AbrirMenuObelisco();

                if (textoInteraccion != null)
                    textoInteraccion.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                jugadorCerca = true;
                jugador = other.gameObject;

                if (textoInteraccion != null)
                {
                    textoInteraccion.SetActive(true);
                    var textComponent = textoInteraccion.GetComponent<TextMeshProUGUI>();
                    if (textComponent != null)
                        textComponent.text = "Pulsa E para descansar";
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                jugadorCerca = false;
                jugador = null;

                if (textoInteraccion != null)
                    textoInteraccion.SetActive(false);
            }
        }
    }
}
