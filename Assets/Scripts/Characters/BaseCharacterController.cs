using UnityEngine;

namespace Eco
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
    public class BaseCharacterController : MonoBehaviour
    {
        [SerializeField]
        protected Rigidbody2D m_RigidBody;

        [SerializeField]
        protected CapsuleCollider2D m_CapsuleCollider;

        [SerializeField]
        protected BaseCharacterStats m_BaseStats;
    }
}


