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

    [CreateAssetMenu(fileName = "NewInventoryItem", menuName = "Eco/Inventory Item")]
    public class InventoryItem : ScriptableObject
    {
        public string itemName;
        public Sprite icon;
        [TextArea]
        public string description;
        public Sprite largeIcon;
        public bool isConsumable;
        public ItemType itemType;

        [HideInInspector]
        public int stackCount = 0;
    }
}
