using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Eco
{
    public class Samurai : BaseCharacter
    {
        [Header("Settings")]

        [SerializeField]
        protected IdleMovementStateSettings m_IdleSettings;

        [SerializeField]
        protected JumpMovementStateSettings m_JumpSettings;

        [SerializeField]
        protected DashMovementStateSettings m_DashSettings;

        [SerializeField]
        protected HorizontalMovementStateSettings m_HorizontalSettings;

        [SerializeField]
        protected Rigidbody2D m_Rigidbody; // Referencia al Rigidbody2D del personaje asignada desde el editor

        protected override void InitializeMovementStates()
        {
            m_MovementStates.Add(GenericMovementStates.Idle, new IdleMovementState(this, m_IdleSettings));
            m_MovementStates.Add(GenericMovementStates.Jump, new JumpMovementState(this, m_JumpSettings));
            m_MovementStates.Add(GenericMovementStates.Dash, new DashMovementState(this, m_DashSettings));
            m_MovementStates.Add(GenericMovementStates.Horizontal, new HorizontalMovementState(this, m_HorizontalSettings, m_Rigidbody)); // Pasamos el Rigidbody2D
        }

        protected void Start()
        {
            ChangeMovementState(GenericMovementStates.Idle);
        }
    }
}
