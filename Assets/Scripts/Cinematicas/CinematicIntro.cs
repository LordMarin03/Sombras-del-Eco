using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

namespace Eco
{
    public class CinematicIntro : MonoBehaviour
    {
        [Header("UI")]
        public Image imagenCinematica;
        public CanvasGroup imagenCanvasGroup;

        public TextMeshProUGUI textoCinematica;
        public CanvasGroup textoCanvasGroup;

        [Header("Contenido")]
        public Sprite[] imagenes;
        [TextArea]
        public string[] textos;
        public AudioClip[] audiosVoz;
        public float fadeTime = 1f;
        public string escenaJuego = "SampleScene";

        [Header("Audio")]
        public AudioSource vozAudioSource;
        public AudioSource musicaFondo;
        public float tiempoPorLetra = 0.02f;

        void Start()
        {
            imagenCanvasGroup.alpha = 0f;
            textoCanvasGroup.alpha = 0f;

            if (musicaFondo != null)
            {
                musicaFondo.loop = true;
                musicaFondo.Play();
            }

            StartCoroutine(ReproducirCinematica());
        }

        IEnumerator ReproducirCinematica()
        {
            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < imagenes.Length; i++)
            {
                imagenCinematica.sprite = imagenes[i];
                string texto = (i < textos.Length) ? textos[i] : "";
                textoCinematica.text = "";

                if (i < audiosVoz.Length && vozAudioSource != null)
                {
                    vozAudioSource.Stop();
                    vozAudioSource.clip = audiosVoz[i];
                    vozAudioSource.Play();
                }

                Coroutine fadeInImg = StartCoroutine(FadeIn(imagenCanvasGroup));
                Coroutine fadeInTxt = StartCoroutine(FadeIn(textoCanvasGroup));
                Coroutine escribir = StartCoroutine(TypewriterEffect(texto));
                yield return fadeInImg;
                yield return fadeInTxt;
                yield return escribir;

                // Esperar a que termine el audio de voz antes de continuar
                if (vozAudioSource != null && vozAudioSource.clip != null)
                    yield return new WaitUntil(() => !vozAudioSource.isPlaying);

                Coroutine fadeOutImg = StartCoroutine(FadeOut(imagenCanvasGroup));
                Coroutine fadeOutTxt = StartCoroutine(FadeOut(textoCanvasGroup));
                yield return fadeOutImg;
                yield return fadeOutTxt;
            }

            if (musicaFondo != null)
                musicaFondo.Stop();

            SceneManager.LoadScene(escenaJuego);
        }

        IEnumerator FadeIn(CanvasGroup cg)
        {
            float t = 0f;
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                cg.alpha = Mathf.Lerp(0f, 1f, t / fadeTime);
                yield return null;
            }
            cg.alpha = 1f;
        }

        IEnumerator FadeOut(CanvasGroup cg)
        {
            float t = 0f;
            while (t < fadeTime)
            {
                t += Time.deltaTime;
                cg.alpha = Mathf.Lerp(1f, 0f, t / fadeTime);
                yield return null;
            }
            cg.alpha = 0f;
        }

        IEnumerator TypewriterEffect(string fullText)
        {
            textoCinematica.text = "";
            for (int i = 0; i < fullText.Length; i++)
            {
                textoCinematica.text += fullText[i];
                yield return new WaitForSeconds(tiempoPorLetra);
            }
        }
    }
}