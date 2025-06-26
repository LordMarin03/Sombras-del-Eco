using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Eco
{
    public class MainMenuUI : MonoBehaviour
    {
        public GameObject mainButtons;
        public GameObject settingsPanel;

        // Subpaneles dentro del menú de ajustes
        public GameObject panelGraficos;
        public GameObject panelAudio;
        public GameObject panelControles;

        public void PlayGame()
        {
            SceneManager.LoadScene("IntroCinematica");
        }


        public void QuitGame()
        {
            Application.Quit();
        }

        public void OpenSettings()
        {
            mainButtons.SetActive(false);
            settingsPanel.SetActive(true);

            // Mostrar solo el panel de gráficos al entrar
            panelGraficos.SetActive(true);
            panelAudio.SetActive(false);
            panelControles.SetActive(false);
        }

        public void BackToMainMenu()
        {
            settingsPanel.SetActive(false);
            mainButtons.SetActive(true);
        }
    }
}