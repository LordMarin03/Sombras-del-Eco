using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace Eco
{
    public class AudioSettings : MonoBehaviour
    {
        [Header("Mixer")]
        public AudioMixer audioMixer;

        [Header("Sliders")]
        public Slider masterSlider;
        public Slider musicSlider;
        public Slider sfxSlider;
        public Slider dialogSlider;

        void Start()
        {
            // Cargar valores guardados o usar valor por defecto (0.75f)
            float masterVol = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
            float musicVol = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
            float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
            float dialogVol = PlayerPrefs.GetFloat("DialogVolume", 0.75f);

            // Asignar a sliders
            masterSlider.value = masterVol;
            musicSlider.value = musicVol;
            sfxSlider.value = sfxVol;
            dialogSlider.value = dialogVol;

            // Aplicar directamente
            SetVolume("MasterVolume", masterVol);
            SetVolume("MusicVolume", musicVol);
            SetVolume("SFXVolume", sfxVol);
            SetVolume("DialogVolume", dialogVol);

            // Añadir listeners para que se actualicen en tiempo real
            masterSlider.onValueChanged.AddListener((value) => OnSliderChanged("MasterVolume", value));
            musicSlider.onValueChanged.AddListener((value) => OnSliderChanged("MusicVolume", value));
            sfxSlider.onValueChanged.AddListener((value) => OnSliderChanged("SFXVolume", value));
            dialogSlider.onValueChanged.AddListener((value) => OnSliderChanged("DialogVolume", value));
        }

        // Método llamado por todos los sliders
        private void OnSliderChanged(string parameterName, float value)
        {
            SetVolume(parameterName, value);
            PlayerPrefs.SetFloat(parameterName, value);
            PlayerPrefs.Save();
        }

        // Conversión de 0-1 (slider) a decibelios
        private void SetVolume(string exposedParam, float sliderValue)
        {
            float dB = Mathf.Log10(Mathf.Clamp(sliderValue, 0.001f, 1f)) * 20f;
            audioMixer.SetFloat(exposedParam, dB);
        }
    }
}