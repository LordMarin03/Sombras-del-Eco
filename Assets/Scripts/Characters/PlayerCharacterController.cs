using Eco;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Eco
{
    public class PlayerCharacterController : BaseCharacterController
    {
        protected bool m_PreviousQueriesStartInColliders;

        protected float m_HorizontalInput;
        protected bool m_JumpDown;
        protected bool m_JumpHeld;

        protected bool m_WantsToJump;
        protected Timer m_JumpBufferTimer = new Timer();

        protected bool m_JumpWasCanceled;
        protected bool m_DoubleJumped;
        protected Timer m_CoyoteTimeTimer = new Timer();

        protected bool m_Grounded;
        protected Vector2 m_Velocity;

        [SerializeField] private int maxHealth = 150;
        private int currentHealth;

        [SerializeField] private HealthBar healthBar;

        protected void Awake()
        {
            m_PreviousQueriesStartInColliders = Physics2D.queriesStartInColliders;
            currentHealth = maxHealth;
        }

        protected void Start()
        {
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            Debug.Log("Jugador recibió daño: " + damage + ". Vida restante: " + currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log("Jugador ha muerto");
            // Aquí puedes agregar lógica de respawn o reinicio de nivel
        }

        public void CurarAlMaximo()
        {
            currentHealth = maxHealth;
            healthBar.SetHealth(currentHealth);
            Debug.Log("Vida restaurada al máximo.");
        }
        public void Curar(int cantidad)
        {
            currentHealth += cantidad;
            currentHealth = Mathf.Min(currentHealth, maxHealth);
            healthBar.SetHealth(currentHealth);
            Debug.Log($"Has recuperado {cantidad} de vida. Vida actual: {currentHealth}");
        }

        protected void Update()
        {
            GetInput();

            if (m_JumpBufferTimer.StopIfElapsed())
            {
                m_WantsToJump = false;
            }
        }

        protected void FixedUpdate()
        {
            float fixedDeltaTime = Services.GetService<MainManager>().FixedDeltaTime;

            CheckCollisions();
            HandleJump();
            HandleDirection(fixedDeltaTime);
            HandleGravity(fixedDeltaTime);
            ApplyMovement();
        }

        protected void GetInput()
        {
            InputManager inputManager = Services.GetService<InputManager>();
            if (inputManager != null)
            {
                m_HorizontalInput = inputManager.HorizontalInput;
                m_JumpDown = inputManager.JumpDown;
                m_JumpHeld = inputManager.JumpHeld;

                if (m_BaseStats.SnapInput)
                {
                    if (Mathf.Abs(m_HorizontalInput) < m_BaseStats.HorizontalDeadZoneThreshold)
                    {
                        m_HorizontalInput = 0;
                    }
                    else
                    {
                        m_HorizontalInput = Mathf.Sign(m_HorizontalInput);
                    }
                }

                if (m_JumpDown)
                {
                    m_WantsToJump = true;
                    m_JumpBufferTimer.Stop();
                    m_JumpBufferTimer.Start(m_BaseStats.JumpBuffer);
                }
            }
        }

        protected void CheckCollisions()
        {
            Physics2D.queriesStartInColliders = false;

            bool groundHit = Physics2D.CapsuleCast(m_CapsuleCollider.bounds.center, m_CapsuleCollider.size, m_CapsuleCollider.direction, 0, Vector2.down, m_BaseStats.GrounderDistance, ~m_BaseStats.CharacterLayer);
            bool ceilingHit = Physics2D.CapsuleCast(m_CapsuleCollider.bounds.center, m_CapsuleCollider.size, m_CapsuleCollider.direction, 0, Vector2.up, m_BaseStats.GrounderDistance, ~m_BaseStats.CharacterLayer);

            if (ceilingHit)
            {
                m_Velocity.y = Mathf.Min(0, m_Velocity.y);
            }

            if (!m_Grounded && groundHit)
            {
                m_Grounded = true;
                m_JumpWasCanceled = false;
                m_DoubleJumped = false;
                m_CoyoteTimeTimer.Stop();
            }
            else if (m_Grounded && !groundHit)
            {
                m_Grounded = false;
                m_CoyoteTimeTimer.Stop();
                m_CoyoteTimeTimer.Start(m_BaseStats.CoyoteTime);
            }

            Physics2D.queriesStartInColliders = m_PreviousQueriesStartInColliders;
        }

        protected void HandleJump()
        {
            if ((m_Grounded || (m_CoyoteTimeTimer.IsStarted() && !m_CoyoteTimeTimer.IsElapsed())) && m_WantsToJump)
            {
                Jump(m_BaseStats.JumpPower);
            }
            else if (!m_JumpWasCanceled && !m_Grounded && m_Velocity.y > 0 && !m_JumpHeld && !m_DoubleJumped)
            {
                m_JumpWasCanceled = true;
            }
            else if (!m_Grounded && m_WantsToJump && !m_DoubleJumped)
            {
                m_DoubleJumped = true;
                Jump(m_BaseStats.DoubleJumpPower);
            }
        }

        protected void Jump(float aJumpPower)
        {
            m_Velocity.y = aJumpPower;
            m_WantsToJump = false;
            m_Grounded = false;
        }

        protected void HandleDirection(float aFixedDeltaTime)
        {
            if (m_HorizontalInput != 0)
            {
                m_Velocity.x = m_Velocity.x + (m_BaseStats.Acceleration * aFixedDeltaTime * m_HorizontalInput);
                m_Velocity.x = Mathf.Clamp(m_Velocity.x, -m_BaseStats.MaxSpeed, m_BaseStats.MaxSpeed);
            }
            else
            {
                float deceleration = m_Grounded ? m_BaseStats.GroundDeceleration : m_BaseStats.AirDeceleration;
                m_Velocity.x = Mathf.Lerp(m_Velocity.x, 0, deceleration * aFixedDeltaTime);
            }
        }

        protected void HandleGravity(float aFixedDeltaTime)
        {
            if (m_Grounded && m_Velocity.y <= 0.0f)
            {
                m_Velocity.y = m_BaseStats.GroundingForce;
            }
            else
            {
                float inAirGravity = m_JumpWasCanceled && m_Velocity.y > 0
                    ? m_BaseStats.FallAcceleration * m_BaseStats.JumpEndEarlyGravityModifier
                    : m_BaseStats.FallAcceleration;

                m_Velocity.y = Mathf.Lerp(m_Velocity.y, -m_BaseStats.MaxFallSpeed, inAirGravity * aFixedDeltaTime);
            }
        }

        protected void ApplyMovement()
        {
            m_RigidBody.velocity = m_Velocity;
        }
    }
}
