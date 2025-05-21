using System.Collections.Generic;
using UnityEngine;

namespace Eco
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager Instance;

        public List<InventoryItem> playerInventory = new List<InventoryItem>();
        public InventorySystem uiInventory;

        public int maxMedicinaDesbloqueada = 1;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void AddItem(InventoryItem item)
        {
            if (item == null) return;

            foreach (var existingItem in playerInventory)
            {
                if (existingItem.itemName == item.itemName && item.isConsumable)
                {
                    existingItem.stackCount++;
                    uiInventory.UpdateInventoryDisplay(playerInventory);
                    EquippedHUD.Instance?.AssignToHUDSlot(GetItemByName(item.itemName));
                    EquippedHUD.Instance?.UpdateLeftSlotCount();

                    if (item.itemName == "Lágrima del Eco")
                    {
                        maxMedicinaDesbloqueada = Mathf.Max(maxMedicinaDesbloqueada, existingItem.stackCount);
                    }

                    return;
                }
            }

            // Primera vez que lo recoge
            item.stackCount = 1;
            playerInventory.Add(item);
            uiInventory.UpdateInventoryDisplay(playerInventory);

            if (item.itemName == "Lágrima del Eco")
                maxMedicinaDesbloqueada = Mathf.Max(maxMedicinaDesbloqueada, 1);

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

        public void RestaurarMedicina()
        {
            var lagrima = playerInventory.Find(item => item.itemName == "Lágrima del Eco");
            if (lagrima != null)
            {
                lagrima.stackCount = maxMedicinaDesbloqueada;
                Debug.Log("Lágrimas restauradas a x" + maxMedicinaDesbloqueada);
                uiInventory?.UpdateInventoryDisplay(playerInventory);
                EquippedHUD.Instance?.UpdateLeftSlotCount();
            }
            else
            {
                Debug.LogWarning("No tienes ninguna medicina aún.");
            }
        }


        public void AumentarMaxMedicina()
        {
            maxMedicinaDesbloqueada = Mathf.Clamp(maxMedicinaDesbloqueada + 1, 1, 3);
            Debug.Log("¡Has aumentado tu capacidad de medicina a " + maxMedicinaDesbloqueada + "!");
        }
    }
}