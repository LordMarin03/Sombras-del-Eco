using UnityEngine;

namespace Eco
{
    public class ZonaDeCuelgue : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                var controller = other.GetComponent<PlayerCharacterController>();
                if (controller != null)
                {
                    controller.ActivarCuelgue(transform.position);
                }
            }
        }
    }
}
