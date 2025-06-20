using UnityEngine;
using UnityEngine.SceneManagement;

namespace Eco
{
    public class GameOverUI : MonoBehaviour
    {
        public void Reintentar()
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("LastCheckpointScene", SceneManager.GetActiveScene().name));
        }

        public void VolverAlMenu()
        {
            SceneManager.LoadScene("MainMenu"); // Asegúrate de tener esta escena en tu proyecto
        }
    }
}
