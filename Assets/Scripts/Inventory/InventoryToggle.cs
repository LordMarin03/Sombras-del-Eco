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

            if (isOpen && Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleInventory();
            }
        }

        public void ToggleInventory()
        {
            isOpen = !isOpen;

            if (inventoryPanel != null)
            {
                inventoryPanel.SetActive(isOpen);


                if (isOpen)
                {
                    var inv = InventoryManager.Instance;
                    if (inv != null && inv.uiInventory != null)
                    {
                        inv.uiInventory.UpdateInventoryDisplay(inv.playerInventory);
                    }
                }
            }

            if (hudPanel != null)
                hudPanel.SetActive(!isOpen);

            Time.timeScale = isOpen ? 0f : 1f;
        }
    }
}
