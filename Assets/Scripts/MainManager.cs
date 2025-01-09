using UnityEngine;

namespace Eco
{
    public class MainManager : MonoBehaviour
    {
        protected float m_Time;
        public float Time { get => m_Time; }

        protected float m_DeltaTime;
        public float DeltaTime { get => m_DeltaTime; }

        protected void Awake()
        {
            GameObject.DontDestroyOnLoad(this);

            Services.RegisterService(this);
        }

        protected void Update()
        {
            m_Time = UnityEngine.Time.time;

            m_DeltaTime = UnityEngine.Time.deltaTime;
        }
    }
}


