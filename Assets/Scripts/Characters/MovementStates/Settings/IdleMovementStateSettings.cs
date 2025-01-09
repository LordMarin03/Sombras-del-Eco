using UnityEngine;

namespace Eco
{
    public class IdleMovementStateSettings : BaseMovementStateSettings
    {
        [SerializeField]
        protected float m_CoughTime = 5;

        public float CoughTime { get => m_CoughTime; }
    }
}


