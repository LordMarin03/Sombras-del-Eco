using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Eco
{
    public class VSyncSelector : MonoBehaviour
    {
        public TextMeshProUGUI estadoText;
        private bool vsyncEnabled;

        void Start()
        {
            vsyncEnabled = PlayerPrefs.GetInt("VSync", QualitySettings.vSyncCount) == 1;
            UpdateText();
        }

        public void ToggleLeft()
        {
            vsyncEnabled = !vsyncEnabled;
            UpdateText();
        }

        public void ToggleRight()
        {
            vsyncEnabled = !vsyncEnabled;
            UpdateText();
        }

        public void ApplyVSync()
        {
            QualitySettings.vSyncCount = vsyncEnabled ? 1 : 0;
            PlayerPrefs.SetInt("VSync", vsyncEnabled ? 1 : 0);
            PlayerPrefs.Save();
        }

        private void UpdateText()
        {
            estadoText.text = vsyncEnabled ? "Activado" : "Desactivado";
        }
    }
}