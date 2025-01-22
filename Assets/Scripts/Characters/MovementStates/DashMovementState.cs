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

            m_ReturnToIdleTimer.Start(0.2f);
        }

        public override void OnExit()
        {

        }

        public override void OnUpdate()
        {
            InputManager input = Services.GetService<InputManager>();
            float horizontalInput = input.HorizontalMovement();

            // Si el temporizador ha terminado
            if (m_ReturnToIdleTimer.StopIfElapsed())
            {
                // Si hay entrada horizontal, cambiar a movimiento horizontal
                if (Mathf.Abs(horizontalInput) > 0.1f)
                {
                    m_Character.ChangeMovementState(BaseCharacter.GenericMovementStates.Horizontal);
                }
                else // Si no hay entrada, volver a Idle
                {
                    m_Character.ChangeMovementState(BaseCharacter.GenericMovementStates.Idle);
                }
            }
        }
    }
}

