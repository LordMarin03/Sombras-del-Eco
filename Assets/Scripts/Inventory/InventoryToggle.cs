using UnityEngine;

namespace Eco
{
    public class InventoryToggle : MonoBehaviour
    {
        public GameObject inventoryPanel;
        public GameObject hudPanel;

        private bool isOpen = false;

        void Start()
        {
            if (inventoryPanel != null)
                inventoryPanel.SetActive(false);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                ToggleInventory();
            }

            if (Input.GetKeyDown(KeyCode.Escape) && isOpen)
            {
                ToggleInventory();
            }
        }

        public void ToggleInventory()
        {
            isOpen = !isOpen;

            if (inventoryPanel != null)
                inventoryPanel.SetActive(isOpen);

            if (hudPanel != null)
                hudPanel.SetActive(!isOpen);

            Time.timeScale = isOpen ? 0f : 1f;
        }
    }
}
