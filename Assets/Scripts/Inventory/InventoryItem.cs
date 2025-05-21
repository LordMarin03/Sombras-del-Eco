using UnityEngine;

namespace Eco
{
    public enum ItemType
    {
        Weapon,
        Consumable,
        Key,
        Special
    }

    [System.Serializable]
    public class InventoryItem
    {
        public string itemName;
        public Sprite icon;
        public string description;
        public Sprite largeIcon;
        public bool isConsumable;
        public ItemType itemType;

        [HideInInspector]
        public int stackCount = 0;
    }
}