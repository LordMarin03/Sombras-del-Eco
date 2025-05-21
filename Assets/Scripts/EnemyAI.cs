using Eco;
using UnityEngine;

namespace Eco
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 2.0f;
        [SerializeField] private float attackRange = 1.0f;
        [SerializeField] private int attackDamage = 10;
        [SerializeField] private float attackCooldown = 1.5f;

        private Transform player;
        private float nextAttackTime = 0f;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            if (player != null)
            {
                FollowPlayer();
                TryToAttack();
            }
        }

        private void FollowPlayer()
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer > attackRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
        }

        private void TryToAttack()
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
            {
                nextAttackTime = Time.time + attackCooldown;
                AttackPlayer();
            }
        }

        private void AttackPlayer()
        {
            if (player.TryGetComponent<PlayerCharacterController>(out PlayerCharacterController playerController))
            {
                playerController.TakeDamage(attackDamage);
            }
        }
    }
}
