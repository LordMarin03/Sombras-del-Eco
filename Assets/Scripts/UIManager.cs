using UnityEngine;

namespace Eco
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [Header("Obelisco UI")]
        public GameObject obeliscoUI;
        public GameObject confirmacionSalirPanel;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void AbrirMenuObelisco()
        {
            Time.timeScale = 0;
            obeliscoUI.SetActive(true);
        }

        public void CerrarMenuObelisco()
        {
            Time.timeScale = 1;
            obeliscoUI.SetActive(false);
        }

        public void MostrarConfirmacionSalir()
        {
            confirmacionSalirPanel.SetActive(true);
        }

        public void CancelarSalida()
        {
            confirmacionSalirPanel.SetActive(false);
        }

        public void ConfirmarSalida()
        {
            Debug.Log("Volviendo al menú principal...");
            Time.timeScale = 1; // por si seguía pausado
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }

    }
}
