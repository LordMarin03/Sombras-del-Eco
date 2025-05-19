using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

namespace Eco
{
    public class LogoIntroSequence : MonoBehaviour
    {
        [Header("UI")]
        public Image logoHolder;            // Imagen donde se muestra el logo
        public CanvasGroup canvasGroup;     // Para el fade in/out
        public GameObject textoSaltar;      // Texto: "Pulsa cualquier tecla"

        [Header("Contenido")]
        public Sprite[] logos;              // Lista de logos
        public float durationPerLogo = 3f;  // Tiempo visible por logo
        public float fadeTime = 1f;         // Tiempo de fade
        public string sceneToLoad = "MainMenu"; // Escena a cargar después

        [Header("Audio")]
        public AudioSource musicaIntro;     // Música que se reinicia con cada logo

        private bool canSkip = false;

        void Start()
        {
            if (logos == null || logos.Length == 0)
            {
                Debug.LogError("⚠️ No se han asignado logos al array 'logos'.");
                return;
            }

            canvasGroup.alpha = 0f;
            if (textoSaltar != null)
                textoSaltar.SetActive(false);

            StartCoroutine(PlayLogos());
        }

        void Update()
        {
            if (canSkip && Input.anyKeyDown)
            {
                SkipToMenu();
            }
        }

        IEnumerator PlayLogos()
        {
            yield return new WaitForSeconds(0.5f); // pequeña pausa inicial

            for (int i = 0; i < logos.Length; i++)
            {
                logoHolder.sprite = logos[i];
                canvasGroup.alpha = 0f;

                // 🎵 Reiniciar la música con cada logo
                if (musicaIntro != null)
                {
                    musicaIntro.Stop();
                    musicaIntro.time = 0f;
                    musicaIntro.Play();
                }

                yield return StartCoroutine(FadeIn());

                if (i == 1 && textoSaltar != null)
                    textoSaltar.SetActive(true); // Mostrar el texto solo en el segundo logo

                if (i >= 1)
                    canSkip = true;

                yield return new WaitForSeconds(durationPerLogo);
                yield return StartCoroutine(FadeOut());
            }

            LoadMenu();
        }

        IEnumerator FadeIn()
        {
            float t = 0f;
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeTime);
                yield return null;
            }
            canvasGroup.alpha = 1f;
        }

        IEnumerator FadeOut()
        {
            float t = 0f;
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(1f, 0f, t / fadeTime);
                yield return null;
            }
            canvasGroup.alpha = 0f;
        }

        void SkipToMenu()
        {
            StopAllCoroutines();

            if (musicaIntro != null)
                musicaIntro.Stop();

            SceneManager.LoadScene(sceneToLoad);
        }

        void LoadMenu()
        {
            if (musicaIntro != null)
                musicaIntro.Stop();

            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
