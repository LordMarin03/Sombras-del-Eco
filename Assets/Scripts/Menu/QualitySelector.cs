using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Eco
{
    public class QualitySelector : MonoBehaviour
    {
        public TextMeshProUGUI estadoText;
        private int currentIndex;

        void Start()
        {
            currentIndex = PlayerPrefs.GetInt("Quality", QualitySettings.GetQualityLevel());
            UpdateText();
        }

        public void ToggleLeft()
        {
            currentIndex = (currentIndex - 1 + QualitySettings.names.Length) % QualitySettings.names.Length;
            UpdateText();
        }

        public void ToggleRight()
        {
            currentIndex = (currentIndex + 1) % QualitySettings.names.Length;
            UpdateText();
        }

        public void ApplyQuality()
        {
            QualitySettings.SetQualityLevel(currentIndex);
            PlayerPrefs.SetInt("Quality", currentIndex);
            PlayerPrefs.Save();
        }

        private void UpdateText()
        {
            estadoText.text = QualitySettings.names[currentIndex];
        }
    }
}