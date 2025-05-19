using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Eco
{
    public class ResolutionSelector : MonoBehaviour
    {
        public TextMeshProUGUI resolutionText;
        private Resolution[] resolutions;
        private int currentIndex = 0;

        void Start()
        {
            resolutions = Screen.resolutions;
            currentIndex = PlayerPrefs.GetInt("ResolutionIndex", GetCurrentResolutionIndex());
            UpdateText();
        }

        public void NextResolution()
        {
            currentIndex = (currentIndex + 1) % resolutions.Length;
            UpdateText();
        }

        public void PreviousResolution()
        {
            currentIndex = (currentIndex - 1 + resolutions.Length) % resolutions.Length;
            UpdateText();
        }

        public void ApplyResolution()
        {
            Resolution res = resolutions[currentIndex];
            Screen.SetResolution(res.width, res.height, Screen.fullScreen);
            PlayerPrefs.SetInt("ResolutionIndex", currentIndex);
            PlayerPrefs.Save();
        }

        private void UpdateText()
        {
            resolutionText.text = resolutions[currentIndex].width + "x" + resolutions[currentIndex].height;
        }

        private int GetCurrentResolutionIndex()
        {
            for (int i = 0; i < resolutions.Length; i++)
            {
                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    return i;
                }
            }
            return 0;
        }
    }
}