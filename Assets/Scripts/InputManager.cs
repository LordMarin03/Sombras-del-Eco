using UnityEngine;

namespace Eco
{
    public class InputManager : MonoBehaviour
    {
        protected void Awake()
        {
            Services.RegisterService(this);
        }

        public bool JumpPressed()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        public bool DashPressed()
        {
            return Input.GetKeyDown(KeyCode.LeftShift);
        }

        public float HorizontalMovement()
        {
            float horizontalMovement = 0.0f;

            if (Input.GetKey(KeyCode.D))
            {
                horizontalMovement += 1.0f;
            }

            if (Input.GetKey(KeyCode.A))
            {
                horizontalMovement -= 1.0f;
            }

            return horizontalMovement;
        }
    }
}


