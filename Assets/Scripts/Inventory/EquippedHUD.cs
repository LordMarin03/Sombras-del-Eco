using UnityEngine;
using UnityEngine.UI;

namespace Eco
{
    public class EquippedHUD : MonoBehaviour
    {
        public static EquippedHUD Instance;

        [Header("Slots visuales")]
        public Image weaponSlot;
        public Image leftSlot;
        public Image topSlot;
        public Image bottomSlot;

        [Header("Datos de objetos asignados")]
        public InventoryItem leftItem;
        public InventoryItem topItem;
        public InventoryItem bottomItem;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void EquipWeapon(InventoryItem item)
        {
            if (item != null && weaponSlot != null)
            {
                weaponSlot.sprite = item.icon;
                weaponSlot.enabled = true;
                Debug.Log("Arma equipada en HUD: " + item.itemName);
            }
        }

        public void AssignToHUDSlot(InventoryItem item)
        {
            if (item == null) return;

            if ((leftItem == null || item.itemName.Contains("Lágrima")) && leftSlot.sprite == null)
            {
                leftItem = item;
                leftSlot.sprite = item.icon;
                leftSlot.enabled = true;
                Debug.Log("Objeto asignado a slot izquierdo");
                return;
            }

            if (topItem == null && topSlot.sprite == null)
            {
                topItem = item;
                topSlot.sprite = item.icon;
                topSlot.enabled = true;
                Debug.Log("Objeto asignado a slot superior");
                return;
            }

            if (bottomItem == null && bottomSlot.sprite == null)
            {
                bottomItem = item;
                bottomSlot.sprite = item.icon;
                bottomSlot.enabled = true;
                Debug.Log("Objeto asignado a slot inferior");
                return;
            }

            Debug.Log("No hay hueco libre para asignar el objeto al HUD");
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) // Slot izquierdo (1)
            {
                UseHUDItem(leftItem, "izquierdo");
                leftItem = null;
                leftSlot.sprite = null;
                leftSlot.enabled = false;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2)) // Slot superior (2)
            {
                UseHUDItem(topItem, "superior");
                topItem = null;
                topSlot.sprite = null;
                topSlot.enabled = false;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3)) // Slot inferior (3)
            {
                UseHUDItem(bottomItem, "inferior");
                bottomItem = null;
                bottomSlot.sprite = null;
                bottomSlot.enabled = false;
            }
        }

        private void UseHUDItem(InventoryItem item, string slotName)
        {
            if (item == null) return;

            Debug.Log("Usaste el objeto en el slot " + slotName + ": " + item.itemName);

            if (item.itemName.Contains("Lágrima") || item.isConsumable)
            {
                Debug.Log("Aplicando efecto curativo...");
                // player.Heal(50); // tu lógica de curación
            }
            else
            {
                Debug.Log("Este objeto no tiene acción en HUD aún.");
            }
        }
    }
}
