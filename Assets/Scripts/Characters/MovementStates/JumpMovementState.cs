using UnityEngine;

namespace Eco
{
    public class JumpMovementState : BaseMovementState
    {
        protected JumpMovementStateSettings m_Settings;

        protected Timer m_ReturnToIdleTimer = new Timer();

        public JumpMovementState(BaseCharacter character, JumpMovementStateSettings settings) : base(character)
        {
            m_Settings = settings;
        }

        public override void OnEnter()
        {
            m_Character.RootTransform.position += Vector3.up * m_Settings.JumpDistance;

            m_Character.PlayAnimation("Jump");

            m_ReturnToIdleTimer.Start(2.0f);
        }

        public override void OnUpdate()
        {
            if (m_ReturnToIdleTimer.StopIfElapsed())
            {
                m_Character.ChangeMovementState(BaseCharacter.GenericMovementStates.Idle);
            }
        }

        public override void OnExit()
        {

        }
    }
}


