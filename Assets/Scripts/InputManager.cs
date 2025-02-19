using UnityEngine;

namespace Eco
{
    public class InputManager : MonoBehaviour
    {

        protected float m_HorizontalInput;
        public float HorizontalInput {  get => m_HorizontalInput; }

        protected float m_VerticalInput;
        public float VerticalInput { get => m_VerticalInput; }

        protected bool m_JumpDown;
        public bool JumpDown { get => m_JumpDown; }

        protected bool m_JumpHeld;
        public bool JumpHeld { get => m_JumpHeld; }



        protected void Awake()
        {
            Services.RegisterService(this);
        }

        protected void Update()
        {
            m_HorizontalInput = Input.GetAxis("Horizontal");
            m_VerticalInput = Input.GetAxis("Vertical");

            m_JumpDown = Input.GetButtonDown("Jump");
            m_JumpHeld = Input.GetButton("Jump");
        }
    }
}


