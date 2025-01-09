using UnityEngine;

namespace Eco
{
    public class JumpMovementStateSettings : BaseMovementStateSettings
    {
        [SerializeField]
        protected float m_JumpDistance = 10;

        public float JumpDistance { get => m_JumpDistance; }
    }
}


