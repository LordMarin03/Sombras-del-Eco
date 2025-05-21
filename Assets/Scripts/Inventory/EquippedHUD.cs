using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Eco
{
    public class EquippedHUD : MonoBehaviour
    {
        public static EquippedHUD Instance;

        public Image weaponSlot;
        public Image leftSlot;
        public Image topSlot;
        public Image bottomSlot;

        public TextMeshProUGUI leftSlotCountText;
        public InventoryItem leftItem;
        public InventoryItem topItem;
        public InventoryItem bottomItem;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void EquipWeapon(InventoryItem item)
        {
            if (item != null)
            {
                weaponSlot.sprite = item.icon;
                weaponSlot.enabled = true;
            }
        }

        public void AssignToHUDSlot(InventoryItem item)
        {
            if (item.itemName.Contains("Lágrima"))
            {
                leftItem = item;
                leftSlot.sprite = item.icon;
                leftSlot.enabled = true;
                UpdateLeftSlotCount();
                return;
            }

            if (topItem == null)
            {
                topItem = item;
                topSlot.sprite = item.icon;
                topSlot.enabled = true;
            }
            else if (bottomItem == null)
            {
                bottomItem = item;
                bottomSlot.sprite = item.icon;
                bottomSlot.enabled = true;
            }
        }

        public void UpdateLeftSlotCount()
        {
            if (leftItem == null) return;
            leftSlotCountText.text = "x" + leftItem.stackCount;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (leftItem != null && leftItem.stackCount > 0)
                {
                    Debug.Log("Te curas usando una Lágrima del Eco.");

                    // Consumir ítem
                    leftItem.stackCount--;
                    UpdateLeftSlotCount();

                    // Curar al jugador
                    GameObject jugador = GameObject.FindGameObjectWithTag("Player");
                    if (jugador != null)
                    {
                        var controller = jugador.GetComponent<Eco.PlayerCharacterController>();
                        if (controller != null)
                        {
                            controller.Curar(50);
                        }
                    }

                    // Reproducir sonido de curación
                    AudioManager.Instance?.ReproducirSonidoCuracion();
                }
            }
        }
    }
}
