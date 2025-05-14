using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Eco
{
    public class SettingsTabs : MonoBehaviour
    {
        public GameObject panelGraficos;
        public GameObject panelAudio;
        public GameObject panelControles;

        public void ShowGraficos()
        {
            panelGraficos.SetActive(true);
            panelAudio.SetActive(false);
            panelControles.SetActive(false);
        }

        public void ShowAudio()
        {
            panelGraficos.SetActive(false);
            panelAudio.SetActive(true);
            panelControles.SetActive(false);
        }

        public void ShowControles()
        {
            panelGraficos.SetActive(false);
            panelAudio.SetActive(false);
            panelControles.SetActive(true);
        }
    }
}