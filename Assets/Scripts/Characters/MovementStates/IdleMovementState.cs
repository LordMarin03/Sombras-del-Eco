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
