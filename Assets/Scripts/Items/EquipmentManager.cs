using UnityEngine;

namespace Eco
{
    public class EquipmentManager : MonoBehaviour
    {
        public static EquipmentManager Instance;

        public InventoryItem equippedItem;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void Equip(InventoryItem item)
        {
            equippedItem = item;
            Debug.Log("Has equipado: " + item.itemName);

            // Aqu� podr�as cambiar el sprite del personaje, activar habilidades, etc.
        }
    }
}
