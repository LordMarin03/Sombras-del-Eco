using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Eco
{
    public abstract class BaseCharacter : MonoBehaviour
    {
        [SerializeField]
        protected GameObject m_RootGameObject;
        public GameObject RootGameObject { get => m_RootGameObject; }

        [SerializeField]
        protected Transform m_RootTransform;
        public Transform RootTransform { get => m_RootTransform; }

        [SerializeField]
        protected Animator m_Animator;
        public Animator Animator { get => m_Animator; }

        public enum GenericMovementStates
        {
            None = -1,

            Idle = 0,

            Walk = 10,
            Run = 11,
            Jump = 15,
            Dash = 20,
            Horizontal = 30,

            Attack = 50
        }

        protected Dictionary<GenericMovementStates, BaseMovementState> m_MovementStates = new Dictionary<GenericMovementStates, BaseMovementState>();

        protected GenericMovementStates m_CurrentMovementState = GenericMovementStates.None;

        protected abstract void InitializeMovementStates();

        protected void Awake()
        {
            InitializeMovementStates();
        }

        protected void Update()
        {
            if (m_CurrentMovementState != GenericMovementStates.None)
            {
                BaseMovementState currentMovementState = m_MovementStates[m_CurrentMovementState];
                currentMovementState.OnUpdate();
            }
        }

        public void ChangeMovementState(GenericMovementStates movementState)
        {
            if (m_CurrentMovementState != GenericMovementStates.None)
            {
                BaseMovementState currentMovementState = m_MovementStates[m_CurrentMovementState];
                currentMovementState.OnExit();
            }

            m_CurrentMovementState = movementState;

            if (m_CurrentMovementState != GenericMovementStates.None)
            {
                BaseMovementState currentMovementState = m_MovementStates[m_CurrentMovementState];
                currentMovementState.OnEnter();

                Debug.Log($"Changing to state {movementState}");
            }
        }

        public void PlayAnimation(string animationName)
        {
            m_Animator.Play(animationName, 0, 0.0f);
        }
    }
}


