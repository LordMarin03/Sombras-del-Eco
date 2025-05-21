using System.Collections.Generic;
using UnityEngine;

namespace Eco
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance;

        public List<InventoryItem> playerInventory = new List<InventoryItem>();
        public InventorySystem uiInventory;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void AddItem(InventoryItem item)
        {
            if (item == null)
            {
                Debug.LogWarning("Intentando añadir un ítem nulo.");
                return;
            }

            foreach (var existingItem in playerInventory)
            {
                if (existingItem.itemName == item.itemName && item.isConsumable)
                {
                    existingItem.stackCount++;
                    uiInventory.UpdateInventoryDisplay(playerInventory);
                    EquippedHUD.Instance?.AssignToHUDSlot(GetItemByName(item.itemName));
                    return;
                }
            }

            item.stackCount = 1;
            playerInventory.Add(item);
            uiInventory.UpdateInventoryDisplay(playerInventory);

            switch (item.itemType)
            {
                case ItemType.Weapon:
                    EquipmentManager.Instance?.Equip(item);
                    EquippedHUD.Instance?.EquipWeapon(item);
                    break;
                case ItemType.Consumable:
                case ItemType.Special:
                    EquippedHUD.Instance?.AssignToHUDSlot(item);
                    break;
            }
        }

        public void UseItem(int index)
        {
            if (index < 0 || index >= playerInventory.Count) return;

            InventoryItem item = playerInventory[index];
            if (item.isConsumable && item.stackCount > 0)
            {
                item.stackCount--;
                EquippedHUD.Instance?.UpdateLeftSlotCount();
                uiInventory.UpdateInventoryDisplay(playerInventory);
            }
        }

        public InventoryItem GetItemByName(string name)
        {
            return playerInventory.Find(i => i.itemName == name);
        }
    }
}