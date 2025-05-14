using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Eco
{
    public class GraphicsSettingsUI : MonoBehaviour
    {
        public TMP_Dropdown resolutionDropdown;
        public Toggle fullscreenToggle;
        public Toggle vsyncToggle;
        public TMP_Dropdown qualityDropdown;

        private Resolution[] resolutions;

        void Start()
        {
            // Rellenar el dropdown con las resoluciones disponibles
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();

            int currentResolutionIndex = 0;
            var options = new System.Collections.Generic.List<string>();

            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + "x" + resolutions[i].height;
                options.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionIndex", currentResolutionIndex);
            resolutionDropdown.RefreshShownValue();

            // Cargar los valores previos guardados
            fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", Screen.fullScreen ? 1 : 0) == 1;
            vsyncToggle.isOn = PlayerPrefs.GetInt("VSync", QualitySettings.vSyncCount) == 1;
            qualityDropdown.value = PlayerPrefs.GetInt("Quality", QualitySettings.GetQualityLevel());

            ApplySettings(); // Aplicar los valores guardados
        }

        public void ApplySettings()
        {
            // Resolución
            Resolution res = resolutions[resolutionDropdown.value];
            Screen.SetResolution(res.width, res.height, fullscreenToggle.isOn);

            // Pantalla completa
            Screen.fullScreen = fullscreenToggle.isOn;

            // VSync
            QualitySettings.vSyncCount = vsyncToggle.isOn ? 1 : 0;

            // Calidad
            QualitySettings.SetQualityLevel(qualityDropdown.value);

            // Guardar valores
            PlayerPrefs.SetInt("ResolutionIndex", resolutionDropdown.value);
            PlayerPrefs.SetInt("Fullscreen", fullscreenToggle.isOn ? 1 : 0);
            PlayerPrefs.SetInt("VSync", vsyncToggle.isOn ? 1 : 0);
            PlayerPrefs.SetInt("Quality", qualityDropdown.value);
            PlayerPrefs.Save();
        }
    }
}