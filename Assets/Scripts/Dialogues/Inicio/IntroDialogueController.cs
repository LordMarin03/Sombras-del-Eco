using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Eco;
using Exo;

namespace Eco
{
    public class IntroDialogueController : MonoBehaviour
    {
        [Header("UI")]
        public CanvasGroup dialogueCanvas;
        public TextMeshProUGUI dialogueText;

        [Header("Efecto m�quina de escribir")]
        public float typingSpeed = 0.02f;
        public float autoAdvanceDelay = 2.5f;
        public AudioSource typeSound;

        [Header("L�neas de di�logo")]
        [TextArea(3, 6)]
        public string[] lines = new string[]
        {
        "Silencio... otra vez ese eco� dentro de m�.",
        "No s� qui�n fui. No s� qu� soy.",
        "Pero algo... algo me llama desde las sombras.",
        "El Susurro dice que debo avanzar. Que hay algo m�s all�",
        "...aunque cada paso que doy, siento que me alejo de m� mismo.",
        "Sea redenci�n o condena� no pienso detenerme."
        };

        private int currentLine = 0;
        private bool isTyping = false;
        private PlayerCharacterController player;

        void Start()
        {
            player = FindObjectOfType<PlayerCharacterController>();
            if (player != null)
                player.enabled = false;

            dialogueCanvas.alpha = 1f;
            dialogueCanvas.blocksRaycasts = true;

            StartCoroutine(TypeLine());
        }

        IEnumerator TypeLine()
        {
            isTyping = true;
            dialogueText.text = "";

            if (typeSound != null)
            {
                typeSound.loop = true;
                typeSound.Play();
            }

            foreach (char letter in lines[currentLine])
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

            if (typeSound != null)
            {
                typeSound.Stop();
                typeSound.loop = false;
            }

            isTyping = false;

            yield return new WaitForSeconds(autoAdvanceDelay);

            currentLine++;
            if (currentLine < lines.Length)
            {
                StartCoroutine(TypeLine());
            }
            else
            {
                EndDialogue();
            }
        }

        void EndDialogue()
        {
            dialogueCanvas.alpha = 0f;
            dialogueCanvas.blocksRaycasts = false;

            if (player != null)
                player.enabled = true;

            gameObject.SetActive(false);
        }
    }
}