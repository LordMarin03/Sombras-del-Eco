using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Eco
{
    public class IntroSequence : MonoBehaviour
    {
        public float tiempoEsperar = 4f; // segundos antes de avanzar
        public string escenaMenu = "MainMenu";

        private bool puedeSaltar = false;

        void Start()
        {
            Invoke("PermitirSalto", 1f); // evitar que se salte instantáneamente
            Invoke("CargarMenu", tiempoEsperar);
        }

        void Update()
        {
            if (puedeSaltar && Input.anyKeyDown)
            {
                CancelInvoke("CargarMenu");
                CargarMenu();
            }
        }

        void PermitirSalto()
        {
            puedeSaltar = true;
        }

        void CargarMenu()
        {
            SceneManager.LoadScene(escenaMenu);
        }
    }
}
