using UnityEngine;

namespace Eco
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [Header("Sonidos")]
        public AudioClip sonidoCuracion;

        private AudioSource audioSource;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            audioSource = GetComponent<AudioSource>();
        }

        public void ReproducirSonidoCuracion()
        {
            if (sonidoCuracion != null)
            {
                audioSource.PlayOneShot(sonidoCuracion);
            }
        }
    }
}
