using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Eco
{
    public class FullscreenSelector : MonoBehaviour
    {
        public TextMeshProUGUI estadoText;
        private bool isFullscreen;

        void Start()
        {
            isFullscreen = PlayerPrefs.GetInt("Fullscreen", Screen.fullScreen ? 1 : 0) == 1;
            UpdateText();
        }

        public void ToggleLeft()
        {
            isFullscreen = !isFullscreen;
            UpdateText();
        }

        public void ToggleRight()
        {
            isFullscreen = !isFullscreen;
            UpdateText();
        }

        public void ApplyFullscreen()
        {
            Screen.fullScreen = isFullscreen;
            PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
            PlayerPrefs.Save();
        }

        private void UpdateText()
        {
            estadoText.text = isFullscreen ? "Sí" : "No";
        }
    }
}