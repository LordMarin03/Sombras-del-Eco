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

        protected override void InitializeMovementStates()
        {
            m_MovementStates.Add(GenericMovementStates.Idle, new IdleMovementState(this, m_IdleSettings));
            m_MovementStates.Add(GenericMovementStates.Jump, new JumpMovementState(this, m_JumpSettings));
            m_MovementStates.Add(GenericMovementStates.Dash, new DashMovementState(this, m_DashSettings));
        }

        protected void Start()
        {
            ChangeMovementState(GenericMovementStates.Idle);
        }
    }
}


