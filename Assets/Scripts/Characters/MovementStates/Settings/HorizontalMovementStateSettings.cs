using UnityEngine;

namespace Eco
{
    [System.Serializable]
    public class HorizontalMovementStateSettings : BaseMovementStateSettings
    {
        [SerializeField]
        protected float m_Speed = 5.0f; // Velocidad m�xima
        public float Speed { get => m_Speed; }

        [SerializeField]
        protected float m_Acceleration = 10.0f; // Qu� tan r�pido alcanza la velocidad m�xima
        public float Acceleration { get => m_Acceleration; }

        [SerializeField]
        protected float m_Deceleration = 15.0f; // Qu� tan r�pido se detiene
        public float Deceleration { get => m_Deceleration; }
    }


}