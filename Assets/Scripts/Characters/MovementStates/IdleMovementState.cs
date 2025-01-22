using UnityEngine;

namespace Eco
{
    public class IdleMovementState : BaseMovementState
    {
        protected IdleMovementStateSettings m_Settings;

        protected Timer m_CoughTimer = new Timer();

        public IdleMovementState(BaseCharacter character, IdleMovementStateSettings settings) : base(character)
        {
            m_Settings = settings;
        }

        public override void OnEnter()
        {
            m_CoughTimer.Start(m_Settings.CoughTime);
            m_Character.PlayAnimation("Idle");

            // Detener el movimiento horizontal
            Rigidbody2D rb = m_Character.RootGameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }

        public override void OnUpdate()
        {
            if (m_CoughTimer.StopIfElapsed())
            {
                Cough();
            }

            InputManager inputManager = Services.GetService<InputManager>();

            if (inputManager.JumpPressed())
            {
                m_Character.ChangeMovementState(BaseCharacter.GenericMovementStates.Jump);
                return;
            }

            if (inputManager.DashPressed())
            {
                m_Character.ChangeMovementState(BaseCharacter.GenericMovementStates.Dash);
                return;
            }
            float horizontalInput = Services.GetService<InputManager>().HorizontalMovement();

            if (Mathf.Abs(horizontalInput) > 0.1f) // Si hay movimiento horizontal
            {
                m_Character.ChangeMovementState(BaseCharacter.GenericMovementStates.Horizontal);
                return;
            }
        }

        public override void OnExit()
        {
            m_CoughTimer.Stop();
        }

        protected void Cough()
        {
            Debug.Log("Hmmmmm");

            m_CoughTimer.Start(m_Settings.CoughTime);
        }
    }
}
