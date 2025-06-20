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
                PlayerCharacterController player = other.GetComponent<PlayerCharacterController>();
                if (player != null)
                {
                    player.RecogerItemConAnimacion(itemToAdd, gameObject);
                }
            }
        }
    }
}
