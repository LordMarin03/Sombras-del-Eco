using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Eco
{
    public class MainMenuUI : MonoBehaviour
    {
        public GameObject settingsPanel;
        public GameObject mainButtons;

        public void OpenSettings()
        {
            settingsPanel.SetActive(true);
            mainButtons.SetActive(false);
        }

        public void BackToMenu()
        {
            settingsPanel.SetActive(false);
            mainButtons.SetActive(true);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void PlayGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }
    }
}