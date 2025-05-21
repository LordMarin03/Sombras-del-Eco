using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace Eco
{
    public class InventoryNavigator : MonoBehaviour
    {
        [Header("Grid y navegación")]
        public Transform inventoryGrid;
        public int columns = 4;

        [Header("Panel de detalle")]
        public GameObject itemDetailPanel;
        public Image itemImage;
        public TextMeshProUGUI itemNameText;
        public TextMeshProUGUI itemDescriptionText;

        private int currentIndex = 0;
        private int totalSlots;

        private enum InventoryState
        {
            InventoryOnly,
            ViewingDetails
        }

        private InventoryState currentState = InventoryState.InventoryOnly;

        void Start()
        {
            totalSlots = inventoryGrid.childCount;
            HighlightSlot(currentIndex);
            if (itemDetailPanel != null)
                itemDetailPanel.SetActive(false);
        }

        void Update()
        {
            switch (currentState)
            {
                case InventoryState.InventoryOnly:
                    HandleNavigation();

                    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))
                    {
                        if (IsValidIndex(currentIndex))
                        {
                            OpenDetail(currentIndex);
                            currentState = InventoryState.ViewingDetails;
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        CloseInventory();
                    }
                    break;

                case InventoryState.ViewingDetails:

                    if (Input.GetKeyDown(KeyCode.C)) // Consumir ítem
                    {
                        Debug.Log("Tecla C pulsada para consumir.");
                        if (IsValidIndex(currentIndex))
                        {
                            InventoryItem item = InventoryManager.Instance.playerInventory[currentIndex];
                            Debug.Log("Ítem seleccionado: " + item.itemName + " | Consumible: " + item.isConsumable);

                            if (item.isConsumable)
                            {
                                InventoryManager.Instance.UseItem(currentIndex);
                                itemDetailPanel.SetActive(false);
                                currentState = InventoryState.InventoryOnly;
                            }
                            else
                            {
                                Debug.Log("Este objeto no es consumible.");
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Índice inválido al intentar consumir ítem.");
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.Q)) // Equipar ítem
                    {
                        if (IsValidIndex(currentIndex))
                        {
                            InventoryItem item = InventoryManager.Instance.playerInventory[currentIndex];
                            if (item.itemType == ItemType.Weapon)
                            {
                                EquipmentManager.Instance.Equip(item);
                                itemDetailPanel.SetActive(false);
                                currentState = InventoryState.InventoryOnly;
                            }
                            else
                            {
                                Debug.Log("Este objeto no es un arma.");
                            }
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        itemDetailPanel.SetActive(false);
                        currentState = InventoryState.InventoryOnly;
                    }

                    break;
            }
        }

        void HandleNavigation()
        {
            int previousIndex = currentIndex;

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if ((currentIndex + 1) % columns != 0 && currentIndex + 1 < totalSlots)
                    currentIndex++;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (currentIndex % columns != 0)
                    currentIndex--;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (currentIndex + columns < totalSlots)
                    currentIndex += columns;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (currentIndex - columns >= 0)
                    currentIndex -= columns;
            }

            if (previousIndex != currentIndex)
                HighlightSlot(currentIndex);
        }

        void HighlightSlot(int index)
        {
            for (int i = 0; i < totalSlots; i++)
            {
                Transform slot = inventoryGrid.GetChild(i);
                Transform frame = slot.Find("SelectionFrame");

                if (frame != null)
                    frame.gameObject.SetActive(i == index);
            }
        }

        void OpenDetail(int index)
        {
            var items = InventoryManager.Instance.playerInventory;

            if (!IsValidIndex(index)) return;

            InventoryItem item = items[index];
            itemDetailPanel.SetActive(true);
            itemImage.sprite = item.largeIcon != null ? item.largeIcon : item.icon;
            itemNameText.text = item.itemName;
            itemDescriptionText.text = item.description;
        }

        public void CloseInventory()
        {
            inventoryGrid.gameObject.SetActive(false);
            itemDetailPanel.SetActive(false);
            Time.timeScale = 1f;

            GameObject hud = GameObject.Find("HUD");
            if (hud != null) hud.SetActive(true);
        }

        bool IsValidIndex(int index)
        {
            var items = InventoryManager.Instance.playerInventory;
            return index >= 0 && index < items.Count && items[index] != null;
        }
    }
}
