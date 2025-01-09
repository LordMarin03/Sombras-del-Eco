using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Eco
{
    public class DashMovementStateSettings : BaseMovementStateSettings
    {
        [SerializeField]
        protected float m_DashDistance = 3;

        public float DashDistance { get { return m_DashDistance; } }

        [SerializeField]
        protected float m_DashThreshold = 0.2f;

        public float DashThreshold { get { return m_DashThreshold; } }
    }
}


