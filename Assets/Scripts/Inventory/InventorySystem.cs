using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace Eco
{
    public class InventorySystem : MonoBehaviour
    {
        public Transform inventoryGrid;

        public void UpdateInventoryDisplay(List<InventoryItem> items)
        {
            for (int i = 0; i < inventoryGrid.childCount; i++)
            {
                Transform slot = inventoryGrid.GetChild(i);
                Image itemIcon = slot.Find("ItemIcon")?.GetComponent<Image>();
                TextMeshProUGUI nameText = slot.Find("ItemNameText")?.GetComponent<TextMeshProUGUI>();

                if (itemIcon == null || nameText == null) continue;

                if (i < items.Count && items[i] != null)
                {
                    itemIcon.sprite = items[i].icon;
                    itemIcon.enabled = true;
                    nameText.text = "x" + items[i].stackCount;
                    nameText.enabled = true;
                }
                else
                {
                    itemIcon.enabled = false;
                    nameText.text = "";
                    nameText.enabled = false;
                }
            }
        }
    }
}