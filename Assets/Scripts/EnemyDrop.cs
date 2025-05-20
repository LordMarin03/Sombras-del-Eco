using UnityEngine;

namespace Eco
{
    public class EnemyDrop : MonoBehaviour
    {
        public InventoryItem[] possibleDrops;

        public void DropItem()
        {
            if (possibleDrops.Length == 0) return;

            int random = Random.Range(0, possibleDrops.Length);
            InventoryItem item = possibleDrops[random];

            InventoryManager.Instance.AddItem(item);
        }

        void OnDestroy()
        {
            DropItem(); // al morir, suelta un objeto
        }
    }
}
