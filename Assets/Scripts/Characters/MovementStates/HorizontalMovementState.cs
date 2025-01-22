using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Eco
{
    public class HorizontalMovementState : BaseMovementState
    {
        protected HorizontalMovementStateSettings m_Settings;

        // Referencia al Rigidbody2D asignada desde el editor
        protected Rigidbody2D m_Rigidbody;

        private float m_CurrentSpeed = 0.0f;

        public HorizontalMovementState(BaseCharacter character, HorizontalMovementStateSettings settings, Rigidbody2D rigidbody) : base(character)
        {
            m_Settings = settings;
            m_Rigidbody = rigidbody; // Inicializamos la referencia al Rigidbody2D
        }

        public override void OnEnter()
        {
            m_Character.PlayAnimation("Run"); // Asume que tienes una animación de "Run".
        }

        public override void OnUpdate()
        {
            InputManager input = Services.GetService<InputManager>();

            // Comprobar si se debe saltar
            if (input.JumpPressed())
            {
                m_Character.ChangeMovementState(BaseCharacter.GenericMovementStates.Jump);
                return;
            }

            // Comprobar si se debe hacer un dash
            if (input.DashPressed())
            {
                m_Character.ChangeMovementState(BaseCharacter.GenericMovementStates.Dash);
                return;
            }

            float horizontalInput = input.HorizontalMovement();

            // Si hay entrada horizontal
            if (Mathf.Abs(horizontalInput) > 0.1f)
            {
                // Incrementar la velocidad progresivamente hacia la máxima velocidad
                m_CurrentSpeed = Mathf.MoveTowards(m_CurrentSpeed, m_Settings.Speed, m_Settings.Acceleration * Services.GetService<MainManager>().DeltaTime);

                // Actualizar la dirección del movimiento
                m_Rigidbody.velocity = new Vector2(horizontalInput * m_CurrentSpeed, m_Rigidbody.velocity.y);
            }
            else
            {
                // Reducir la velocidad progresivamente hacia 0 cuando no hay entrada
                m_CurrentSpeed = Mathf.MoveTowards(m_CurrentSpeed, 0, m_Settings.Deceleration * Services.GetService<MainManager>().DeltaTime);

                // Mantener la última dirección hasta que la velocidad llegue a 0
                if (Mathf.Abs(m_CurrentSpeed) > 0.1f)
                {
                    m_Rigidbody.velocity = new Vector2(Mathf.Sign(m_Rigidbody.velocity.x) * m_CurrentSpeed, m_Rigidbody.velocity.y);
                }
                else
                {
                    // Detener completamente el movimiento cuando la velocidad sea casi 0
                    m_Rigidbody.velocity = new Vector2(0, m_Rigidbody.velocity.y);
                    m_Character.ChangeMovementState(BaseCharacter.GenericMovementStates.Idle);
                }
            }
        }

        public override void OnExit()
        {
            // Detener el movimiento horizontal cuando se salga del estado
            if (m_Rigidbody != null)
            {
                m_Rigidbody.velocity = new Vector2(0, m_Rigidbody.velocity.y);
            }
        }
    }
}
