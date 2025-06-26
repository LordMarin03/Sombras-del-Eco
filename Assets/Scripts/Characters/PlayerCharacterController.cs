using Eco;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        private Vector3 originalSpriteScale;


        public float HorizontalInput => m_HorizontalInput;
        public BaseCharacterStats BaseStats => m_BaseStats;

        private InputManager m_InputManager;

        [SerializeField] private int maxHealth = 150;
        private int currentHealth;
        [SerializeField] private HealthBar healthBar;
        private Animator m_Animator;

        [Header("Visual Flip")]
        [SerializeField] private Transform spriteRoot;



        private bool m_IsAttacking = false;
        private bool m_IsHanging = false;
        private bool m_IsRolling = false;
        private bool m_IsInvulnerable = false;
        private bool m_IsResting = false;

        [SerializeField] private float attack1Duration = 0.4f;
        [SerializeField] private float attack2Duration = 0.8f;
        [SerializeField] private int attack1Damage = 10;
        [SerializeField] private int attack2Damage = 20;

        [SerializeField] private float rollSpeed = 12f;
        [SerializeField] private float rollDuration = 0.4f;

        protected void Awake()
        {
            m_PreviousQueriesStartInColliders = Physics2D.queriesStartInColliders;
            currentHealth = maxHealth;
            originalSpriteScale = spriteRoot.localScale;


            if (spriteRoot != null)
                m_Animator = spriteRoot.GetComponent<Animator>();
            else
                Debug.LogError("spriteRoot no asignado en el inspector.");
        }


        protected void Start()
        {
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            m_InputManager = Services.GetService<InputManager>();
        }

        protected void Update()
        {
            if (m_IsResting) return; 
            GetInput();
            if (m_JumpBufferTimer.StopIfElapsed()) m_WantsToJump = false;
        }

        protected void FixedUpdate()
        {
            if (m_IsResting) return;

            float fixedDeltaTime = Services.GetService<MainManager>().FixedDeltaTime;
            CheckCollisions();
            HandleJump();
            HandleDirection(fixedDeltaTime);
            HandleGravity(fixedDeltaTime);
            ApplyMovement();
            UpdateAnimator();
            UpdateFacingDirection();
        }

        protected void GetInput()
        {
            if (m_IsResting) return; 

            InputManager inputManager = Services.GetService<InputManager>();
            if (inputManager != null)
            {
                m_HorizontalInput = inputManager.HorizontalInput;
                m_JumpDown = inputManager.JumpDown;
                m_JumpHeld = inputManager.JumpHeld;

                if (m_BaseStats.SnapInput)
                    m_HorizontalInput = Mathf.Abs(m_HorizontalInput) < m_BaseStats.HorizontalDeadZoneThreshold ? 0 : Mathf.Sign(m_HorizontalInput);

                if (m_JumpDown)
                {
                    m_WantsToJump = true;
                    m_JumpBufferTimer.Stop();
                    m_JumpBufferTimer.Start(m_BaseStats.JumpBuffer);
                }

                if (Input.GetButtonDown("Fire1") && !m_IsAttacking)
                    StartCoroutine(PerformAttack("Attack1", attack1Duration, attack1Damage));
                else if (Input.GetButtonDown("Fire2") && !m_IsAttacking)
                    StartCoroutine(PerformAttack("Attack2", attack2Duration, attack2Damage));

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    m_Animator.SetTrigger("Elixir");
                    StartCoroutine(LimitarMovimiento(1.0f));
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    m_Animator.SetTrigger("Protection");
                    StartCoroutine(LimitarMovimiento(1.5f));
                }

                if (Input.GetKeyDown(KeyCode.LeftShift) && !m_IsAttacking && m_Grounded)
                    StartCoroutine(RealizarEsquiva());

                if (m_IsHanging && Input.GetKeyDown(KeyCode.S))
                {
                    m_IsHanging = false;
                    m_Animator.SetBool("IsHanging", false);
                }
            }
        }

        protected void HandleDirection(float aFixedDeltaTime)
        {
            if (m_IsAttacking || m_IsHanging || m_IsResting) return;

            if (m_HorizontalInput != 0)
            {
                m_Velocity.x += m_BaseStats.Acceleration * aFixedDeltaTime * m_HorizontalInput;
                m_Velocity.x = Mathf.Clamp(m_Velocity.x, -m_BaseStats.MaxSpeed * m_BaseStats.SpeedMultiplier, m_BaseStats.MaxSpeed * m_BaseStats.SpeedMultiplier);
            }
            else
            {
                float deceleration = m_Grounded ? m_BaseStats.GroundDeceleration : m_BaseStats.AirDeceleration;
                m_Velocity.x = Mathf.Lerp(m_Velocity.x, 0, deceleration * aFixedDeltaTime);
            }
        }

        protected void HandleJump()
        {
            if ((m_Grounded || (m_CoyoteTimeTimer.IsStarted() && !m_CoyoteTimeTimer.IsElapsed())) && m_WantsToJump)
                Jump(m_BaseStats.JumpPower);
            else if (!m_JumpWasCanceled && !m_Grounded && m_Velocity.y > 0 && !m_JumpHeld && !m_DoubleJumped)
                m_JumpWasCanceled = true;
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

            // Activar la animación
            m_Animator.SetTrigger("Jump");
        }


        protected void HandleGravity(float deltaTime)
        {
            if (m_IsHanging)
            {
                m_Velocity.y = 0;
                return;
            }

            if (m_Grounded && m_Velocity.y <= 0)
                m_Velocity.y = m_BaseStats.GroundingForce;
            else
            {
                float gravity = m_JumpWasCanceled && m_Velocity.y > 0
                    ? m_BaseStats.FallAcceleration * m_BaseStats.JumpEndEarlyGravityModifier
                    : m_BaseStats.FallAcceleration;

                m_Velocity.y = Mathf.Lerp(m_Velocity.y, -m_BaseStats.MaxFallSpeed, gravity * deltaTime);
            }
        }

        protected void ApplyMovement()
        {
            m_RigidBody.velocity = m_Velocity;
        }

        protected void CheckCollisions()
        {
            Physics2D.queriesStartInColliders = false;
            bool groundHit = Physics2D.CapsuleCast(m_CapsuleCollider.bounds.center, m_CapsuleCollider.size, m_CapsuleCollider.direction, 0,
                Vector2.down, m_BaseStats.GrounderDistance, ~m_BaseStats.CharacterLayer);
            bool ceilingHit = Physics2D.CapsuleCast(m_CapsuleCollider.bounds.center, m_CapsuleCollider.size, m_CapsuleCollider.direction, 0,
                Vector2.up, m_BaseStats.GrounderDistance, ~m_BaseStats.CharacterLayer);

            if (ceilingHit) m_Velocity.y = Mathf.Min(0, m_Velocity.y);
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

        private IEnumerator PerformAttack(string triggerName, float duration, int damage)
        {
            m_IsAttacking = true;

            m_Animator.SetTrigger(triggerName);

            if (CombatManager.Instance != null)
                CombatManager.Instance.RealizarAtaque(damage);

            yield return new WaitForSeconds(duration); // Espera el tiempo completo de la animación

            m_IsAttacking = false;
        }


        private IEnumerator LimitarMovimiento(float duration)
        {
            m_Animator.SetBool("IsSlowMoving", true);
            m_BaseStats.SetSpeedMultiplier(0.3f);
            yield return new WaitForSeconds(duration);
            m_BaseStats.ResetSpeedMultiplier();
            m_Animator.SetBool("IsSlowMoving", false);
        }

        private IEnumerator BloquearMovimiento(float duration)
        {
            m_IsAttacking = true;
            yield return new WaitForSeconds(duration);
            m_IsAttacking = false;
        }

        private IEnumerator RealizarEsquiva()
        {
            m_IsAttacking = true;       // Bloquea otras acciones
            m_IsInvulnerable = true;

            m_Animator.SetTrigger("Roll");

            float direccion = Mathf.Sign(spriteRoot.localScale.x); // Usamos la dirección del sprite
            m_RigidBody.velocity = new Vector2(direccion * rollSpeed, m_RigidBody.velocity.y);

            yield return new WaitForSeconds(rollDuration);

            m_IsAttacking = false;
            m_IsInvulnerable = false;
        }




        public void StartRest()
        {
            if (m_IsResting) return;

            m_IsResting = true;
            m_Velocity = Vector2.zero;
            m_Animator.SetTrigger("Rest");
            StartCoroutine(FinalizarDescanso(2.5f));
        }

        private IEnumerator FinalizarDescanso(float duracion)
        {
            m_IsAttacking = true;
            yield return new WaitForSeconds(duracion);
            m_IsResting = false;
            m_IsAttacking = false;
        }


        private void UpdateAnimator()
        {
            if (m_Animator == null) return;

            m_Animator.SetBool("IsRunning", Mathf.Abs(m_HorizontalInput) > 0.1f);
            m_Animator.SetBool("IsGrounded", m_Grounded);
        }





        private void UpdateFacingDirection()
        {
            if (spriteRoot == null) return;

            if (m_HorizontalInput > 0.1f)
                spriteRoot.localScale = new Vector3(Mathf.Abs(originalSpriteScale.x), originalSpriteScale.y, originalSpriteScale.z);
            else if (m_HorizontalInput < -0.1f)
                spriteRoot.localScale = new Vector3(-Mathf.Abs(originalSpriteScale.x), originalSpriteScale.y, originalSpriteScale.z);
        }


        public void ActivarCuelgue(Vector2 posicion)
        {
            m_IsHanging = true;
            m_RigidBody.velocity = Vector2.zero;
            transform.position = posicion;
            m_Animator.SetBool("IsHanging", true);
        }

        public void Curar(int cantidad)
        {
            currentHealth += cantidad;
            currentHealth = Mathf.Min(currentHealth, maxHealth);
            healthBar.SetHealth(currentHealth);
        }

        public void CurarAlMaximo()
        {
            currentHealth = maxHealth;
            healthBar.SetHealth(maxHealth);
        }

        public void TakeDamage(int daño)
        {
            if (m_IsInvulnerable) return;
            currentHealth -= daño;
            healthBar.SetHealth(currentHealth);
            if (currentHealth <= 0) Die();
            else
            {
                m_Animator.SetTrigger("Hurt");
                StartCoroutine(BloquearMovimiento(0.3f));
            }
        }

        private void Die()
        {
            m_Animator.SetBool("IsDead", true);
            StartCoroutine(ProcesoDeMuerte());
        }

        private IEnumerator ProcesoDeMuerte()
        {
            m_IsAttacking = true;
            m_RigidBody.velocity = Vector2.zero;
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("GameOver");
        }

        public void BloquearMovimientoTemporal(float segundos)
        {
            StartCoroutine(BloquearMovimiento(segundos));
        }

        public void RecogerItemConAnimacion(InventoryItem item, GameObject objeto)
        {
            StartCoroutine(AnimacionRecogidaItem(item, objeto));
        }

        private IEnumerator AnimacionRecogidaItem(InventoryItem item, GameObject objeto)
        {
            m_IsAttacking = true;
            m_Animator.SetTrigger("Take");
            yield return new WaitForSeconds(0.6f);

            if (InventoryManager.Instance != null)
            {
                InventoryManager.Instance.AddItem(item);
                Debug.Log("Has recogido: " + item.itemName);
            }

            Destroy(objeto);
            m_IsAttacking = false;
        }

    }
}
