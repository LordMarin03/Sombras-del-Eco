using UnityEngine;

namespace Eco
{
    [System.Serializable]
    public class HorizontalMovementStateSettings : BaseMovementStateSettings
    {
        [SerializeField]
        protected float m_Speed = 5.0f; // Velocidad máxima
        public float Speed { get => m_Speed; }

        [SerializeField]
        protected float m_Acceleration = 10.0f; // Qué tan rápido alcanza la velocidad máxima
        public float Acceleration { get => m_Acceleration; }

        [SerializeField]
        protected float m_Deceleration = 15.0f; // Qué tan rápido se detiene
        public float Deceleration { get => m_Deceleration; }
    }


}