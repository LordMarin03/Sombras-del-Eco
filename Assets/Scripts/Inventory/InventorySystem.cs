using UnityEngine;
using UnityEngine.UI;

namespace Eco
{
    public class InventorySystem : MonoBehaviour
    {
        public Sprite[] itemSprites;
        public Transform inventoryGrid;

        void Start()
        {
            for (int i = 0; i < inventoryGrid.childCount; i++)
            {
                Transform slot = inventoryGrid.GetChild(i);
                Image itemIcon = slot.Find("ItemIcon").GetComponent<Image>();

                if (i < itemSprites.Length)
                {
                    itemIcon.sprite = itemSprites[i];
                    itemIcon.enabled = true;
                }
                else
                {
                    itemIcon.enabled = false;
                }
            }
        }
    }
}
