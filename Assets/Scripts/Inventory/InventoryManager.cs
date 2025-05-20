using System.Collections.Generic;
using UnityEngine;

namespace Eco
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance;

        [Header("Inventario del jugador")]
        public List<InventoryItem> playerInventory = new List<InventoryItem>();

        [Header("UI del inventario")]
        public InventorySystem uiInventory;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                // DontDestroyOnLoad(gameObject); // si quieres que persista
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            if (uiInventory != null)
                uiInventory.UpdateInventoryDisplay(playerInventory);
        }

        public void AddItem(InventoryItem item)
        {
            if (item == null)
            {
                Debug.LogWarning("Intentando añadir un ítem nulo.");
                return;
            }

            playerInventory.Add(item);
            Debug.Log("Objeto recogido: " + item.itemName);

            if (uiInventory != null)
                uiInventory.UpdateInventoryDisplay(playerInventory);

            switch (item.itemType)
            {
                case ItemType.Weapon:
                    if (EquipmentManager.Instance != null)
                        EquipmentManager.Instance.Equip(item);

                    if (EquippedHUD.Instance != null)
                        EquippedHUD.Instance.EquipWeapon(item);
                    break;

                case ItemType.Consumable:
                case ItemType.Special:
                    if (EquippedHUD.Instance != null)
                        EquippedHUD.Instance.AssignToHUDSlot(item);
                    break;
            }
        }

        public void UseItem(int index)
        {
            if (index < 0 || index >= playerInventory.Count)
            {
                Debug.LogWarning("Índice inválido al usar ítem.");
                return;
            }

            InventoryItem item = playerInventory[index];
            Debug.Log("Usaste: " + item.itemName);

            if (item.isConsumable)
            {
                playerInventory.RemoveAt(index);
                if (uiInventory != null)
                    uiInventory.UpdateInventoryDisplay(playerInventory);
            }
        }
    }
}
