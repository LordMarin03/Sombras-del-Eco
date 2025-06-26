using UnityEngine;
using UnityEngine.SceneManagement;

namespace Eco
{
    public class PauseManager : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenuUI;
        [SerializeField] private GameObject settingsMenuUI;

        private bool isPaused = false;

        private void Awake()
        {
            // Asegura que el manager persista entre escenas, si quieres
            DontDestroyOnLoad(this.gameObject);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (settingsMenuUI.activeSelf)
                {
                    CloseSettings();
                    return;
                }

                if (isPaused)
                    Resume();
                else
                    Pause();
            }
        }

        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            settingsMenuUI.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }

        public void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }

        public void OpenSettings()
        {
            pauseMenuUI.SetActive(false);
            settingsMenuUI.SetActive(true);
        }

        public void CloseSettings()
        {
            settingsMenuUI.SetActive(false);
            pauseMenuUI.SetActive(true);
        }

        public void QuitToMainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }

    }
}
