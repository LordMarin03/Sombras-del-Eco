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
        private PlayerCharacterController playerController;

        void Update()
        {
            if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
            {
                if (playerController != null)
                {
                    playerController.StartRest();
                    playerController.CurarAlMaximo();
                }

                InventoryManager.Instance?.RestaurarMedicina();
                Debug.Log("Has descansado en el Obelisco del Recuerdo.");

                PlayerPrefs.SetString("LastCheckpointScene", UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
                PlayerPrefs.Save();

                UIManager.Instance?.AbrirMenuObelisco();

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
                playerController = jugador.GetComponent<PlayerCharacterController>();

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
