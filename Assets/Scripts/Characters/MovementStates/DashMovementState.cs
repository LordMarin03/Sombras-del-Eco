using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Eco
{
    public class DashMovementState : BaseMovementState
    {
        protected DashMovementStateSettings m_Settings;

        protected Timer m_ReturnToIdleTimer = new Timer();

        public DashMovementState(BaseCharacter character, DashMovementStateSettings settings) : base(character)
        {
            m_Settings = settings;
        }

        public override void OnEnter()
        {
            InputManager inputManager = Services.GetService<InputManager>();

            float horizontalMovement = inputManager.HorizontalMovement();

            if (Mathf.Abs(horizontalMovement) <= m_Settings.DashThreshold)
            {
                horizontalMovement = 1.0f;
            }

            m_Character.RootTransform.position += horizontalMovement * m_Settings.DashDistance * Vector3.right;

            m_ReturnToIdleTimer.Start(2.0f);
        }

        public override void OnExit()
        {

        }

        public override void OnUpdate()
        {
            if (m_ReturnToIdleTimer.StopIfElapsed())
            {
                m_Character.ChangeMovementState(BaseCharacter.GenericMovementStates.Idle);
            }
        }
    }
}

