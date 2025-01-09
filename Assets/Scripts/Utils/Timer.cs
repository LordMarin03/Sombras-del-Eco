using UnityEngine;

namespace Eco
{
    public class Timer
    {
        protected bool m_IsStarted;

        protected float m_FinalTime;

        protected bool m_IsPaused;

        protected float m_RemainingTimeOnPause;

        public bool IsStarted() => m_IsStarted;

        public bool IsPaused() => m_IsPaused;

        public void Start(float duration)
        {
            if (!m_IsStarted)
            {
                m_FinalTime = Services.GetService<MainManager>().Time + duration;

                m_IsStarted = true;
                m_IsPaused = false;

                m_RemainingTimeOnPause = 0;
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogWarning($"Trying to start a timer that was already started");
            }
#endif // UNITY_EDITOR
        }

        public void Pause()
        {
            if (m_IsStarted)
            {
                m_RemainingTimeOnPause = m_FinalTime - Services.GetService<MainManager>().Time;

                m_IsStarted = true;
                m_IsPaused = true;

                m_FinalTime = 0;
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogWarning($"Trying to pause a timer that was not started");
            }
#endif // UNITY_EDITOR
        }

        public void Resume()
        {
            if (m_IsPaused)
            {
                m_FinalTime = Services.GetService<MainManager>().Time + m_RemainingTimeOnPause;

                m_IsStarted = true;
                m_IsPaused = false;

                m_RemainingTimeOnPause = 0;
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogWarning($"Trying to resume a timer that was not paused");
            }
#endif // UNITY_EDITOR
        }

        public void Stop()
        {
            if (m_IsStarted)
            {
                m_FinalTime = 0;

                m_IsStarted = false;
                m_IsPaused = false;

                m_RemainingTimeOnPause = 0;
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogWarning($"Trying to stop a timer that was not started");
            }
#endif // UNITY_EDITOR
        }

        public bool IsElapsed()
        {
            return m_IsStarted && Services.GetService<MainManager>().Time >= m_FinalTime;
        }

        public bool StopIfElapsed()
        {
            bool isElapsed = IsElapsed();

            if (isElapsed)
            {
                Stop();
            }

            return isElapsed;
        }
    }
}


