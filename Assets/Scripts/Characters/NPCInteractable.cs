using UnityEngine;
using TMPro;
using System.Collections;

namespace Eco
{
    public class NPCInteractable : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject interactionPrompt;
        [SerializeField] private GameObject dialogueUI;
        [SerializeField] private TextMeshProUGUI dialogueText;

        [Header("Diálogo")]
        [SerializeField][TextArea] private string[] dialogueLines;

        [Header("Recompensa")]
        [SerializeField] private InventoryItem itemToGive;

        [Header("Animación")]
        [SerializeField] private Animator animator;

        [Header("HUD y efectos")]
        [SerializeField] private GameObject hudCanvas;
        [SerializeField] private float typingSpeed = 0.02f;

        private int currentLine = 0;
        private bool isPlayerInRange = false;
        private bool isTalking = false;
        private bool isTyping = false;
        private Coroutine typingCoroutine;

        private void Update()
        {
            if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
            {
                if (!isTalking)
                {
                    StartDialogue();
                }
                else
                {
                    if (isTyping)
                    {
                        CompleteLine();
                    }
                    else
                    {
                        ContinueDialogue();
                    }
                }
            }
        }

        private void StartDialogue()
        {
            isTalking = true;

            if (dialogueUI != null)
                dialogueUI.SetActive(true);

            if (hudCanvas != null)
                hudCanvas.SetActive(false);

            currentLine = 0;

            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            typingCoroutine = StartCoroutine(TypeLine(dialogueLines[currentLine]));

            if (interactionPrompt != null)
                interactionPrompt.SetActive(false);

            animator?.SetTrigger("Talk");
        }

        private void ContinueDialogue()
        {
            currentLine++;

            if (currentLine < dialogueLines.Length)
            {
                if (typingCoroutine != null)
                    StopCoroutine(typingCoroutine);

                typingCoroutine = StartCoroutine(TypeLine(dialogueLines[currentLine]));
            }
            else
            {
                EndDialogue();
                GiveItem();
            }
        }

        private IEnumerator TypeLine(string line)
        {
            isTyping = true;
            dialogueText.text = "";

            foreach (char c in line)
            {
                dialogueText.text += c;
                yield return new WaitForSeconds(typingSpeed);
            }

            isTyping = false;
        }

        private void CompleteLine()
        {
            if (typingCoroutine != null)
                StopCoroutine(typingCoroutine);

            dialogueText.text = dialogueLines[currentLine];
            isTyping = false;
        }

        private void EndDialogue()
        {
            if (dialogueUI != null)
                dialogueUI.SetActive(false);

            if (hudCanvas != null)
                hudCanvas.SetActive(true);

            isTalking = false;
            isTyping = false;

            animator?.SetTrigger("Idle");
        }

        private void GiveItem()
        {
            if (itemToGive != null)
            {
                InventoryItem copy = Instantiate(itemToGive);
                InventoryManager.Instance.AddItem(copy);
                Debug.Log("Item entregado: " + copy.itemName);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerInRange = true;

                if (interactionPrompt != null)
                    interactionPrompt.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerInRange = false;

                if (interactionPrompt != null)
                    interactionPrompt.SetActive(false);

                if (isTalking)
                    EndDialogue();
            }
        }
    }
}
