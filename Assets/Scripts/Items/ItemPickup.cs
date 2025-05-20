using UnityEngine;

namespace Eco
{
    public class ItemPickup : MonoBehaviour
    {
        public InventoryItem itemToAdd;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && itemToAdd != null)
            {
                if (InventoryManager.Instance != null)
                {
                    InventoryManager.Instance.AddItem(itemToAdd);
                    Destroy(gameObject);
                }
                else
                {
                    Debug.LogWarning("No se encontró InventoryManager en escena.");
                }
            }
        }
    }
}
