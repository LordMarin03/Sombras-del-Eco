using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Eco
{
    [CreateAssetMenu(fileName = "CharacterStats", menuName = "ScriptableObject/CharacterStats")]
    public class BaseCharacterStats : ScriptableObject
    {
        [Header("LAYERS")]

        [Tooltip("Layer del personatge")]
        [SerializeField]
        protected LayerMask m_CharacterLayer;
        public LayerMask CharacterLayer { get => m_CharacterLayer; }

        [Header("INPUT")]

        [Tooltip("Fa que tot l'input sigui binari (o 0 o 1). Fa que al controlar amb un gamepad el personatge no camini poc a poc")]
        [SerializeField]
        protected bool m_SnapInput = true;
        public bool SnapInput { get => m_SnapInput; }

        [Tooltip("Input minim per caminar dreta o esquerra per evitar el drift dels mandos"), Range(0.01f, 0.99f)]
        [SerializeField]
        protected float m_HorizontalDeadZoneThreshold = 0.1f;
        public float HorizontalDeadZoneThreshold { get => m_HorizontalDeadZoneThreshold; }

        [Header("MOVEMENT")]

        [Tooltip("Velocitat màxima horitzontal")]
        [SerializeField]
        protected float m_MaxSpeed = 7;
        public float MaxSpeed { get => m_MaxSpeed; }

        [Tooltip("Acceleració horitzontal")]
        [SerializeField]
        protected float m_Acceleration = 120;
        public float Acceleration { get => m_Acceleration; }

        [Tooltip("Deacceleració o fregament horitzontal un cop no té input i està al terra")]
        [SerializeField]
        protected float m_GroundDeceleration = 150;
        public float GroundDeceleration { get => m_GroundDeceleration; }

        [Tooltip("Deacceleració o fregament horitzontal un cop no té input i està a l'aire")]
        [SerializeField]
        protected float m_AirDeceleration = 3;
        public float AirDeceleration { get => m_AirDeceleration; }

        [Tooltip("Força constant que s'aplica quan està al terra que ajuda en terres inclinats"), Range(0f, -10f)]
        [SerializeField]
        protected float m_GroundingForce = -1.5f;
        public float GroundingForce { get => m_GroundingForce; }
        [Tooltip("La distància màxima per detectar si el personatge està tocant el terra o un sostre"), Range(0f, 0.5f)]
        [SerializeField]
        protected float m_GrounderDistance = 0.05f;
        public float GrounderDistance { get => m_GrounderDistance; }

        [Header("JUMP")]

        [Tooltip("La velocitat immediata que s'aplica al saltar")]
        [SerializeField]
        protected float m_JumpPower = 22;
        public float JumpPower { get => m_JumpPower; }

        [Tooltip("La velocitat immediata que s'aplica al fer doble salt")]
        [SerializeField]
        protected float m_DoubleJumpPower = 18;
        public float DoubleJumpPower { get => m_DoubleJumpPower; }

        [Tooltip("Velocitat màxima vertical al caure")]
        [SerializeField]
        protected float m_MaxFallSpeed = 18;
        public float MaxFallSpeed { get => m_MaxFallSpeed; }

        [Tooltip("Gravetat a l'aire")]
        [SerializeField]
        protected float m_FallAcceleration = 2.5f;
        public float FallAcceleration { get => m_FallAcceleration; }

        [Tooltip("Multiplicador de gravetat al deixar d'apretar el salt abans d'acabar-lo")]
        [SerializeField]
        protected float m_JumpEndEarlyGravityModifier = 2.3f;
        public float JumpEndEarlyGravityModifier { get => m_JumpEndEarlyGravityModifier; }


        [Tooltip("El temps màxim per realitzar un coyote jump. Permet saltar just després de caure per una plataforma")]
        [SerializeField]
        protected float m_CoyoteTime = 0.08f;
        public float CoyoteTime { get => m_CoyoteTime; }

        [Tooltip("El temps màxim que es fa el buffer del salt. Permet saltar encara que l'input sigui just abans de tocar el terra")]
        [SerializeField]
        protected float m_JumpBuffer = 0.2f;
        public float JumpBuffer { get => m_JumpBuffer; }
    }
}


