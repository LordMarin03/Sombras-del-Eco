using UnityEngine;
using UnityEngine.SceneManagement;

namespace Eco
{
    public class IntroSceneController : MonoBehaviour
    {
        [SerializeField] private float tiempoDeIntro = 20f;

        private void Start()
        {
            Invoke(nameof(CargarEscenaPrincipal), tiempoDeIntro);
        }

        private void CargarEscenaPrincipal()
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
