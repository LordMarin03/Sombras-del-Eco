using TMPro;
using UnityEngine;

namespace Eco
{
    public class DebugSpeedUI : MonoBehaviour
    {
        public PlayerCharacterController player;
        private TextMeshProUGUI text;

        private void Start()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (player == null || text == null) return;

            float input = Mathf.Abs(player.HorizontalInput);
            float speed = input * player.BaseStats.MaxSpeed;

            text.text = $"Input: {input:F2} | Speed: {speed:F2}";
        }
    }
}
