using Eco;
using UnityEngine;

namespace Eco
{
    public class CombatManager : MonoBehaviour
    {
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private float attackRange = 3.0f;

        [SerializeField] private int attackDamage = 10;
        [SerializeField] private Transform attackPoint;

        private PlayerCharacterController player;

        private void Start()
        {
            player = FindObjectOfType<PlayerCharacterController>();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1")) // Botón de ataque
            {
                PerformAttack();
            }
        }

        private void PerformAttack()
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            Debug.Log("Enemigos detectados: " + hitEnemies.Length);

            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("Intentando golpear: " + enemy.name);

                if (enemy.TryGetComponent<EnemyController>(out EnemyController enemyController))
                {
                    Debug.Log("Golpeando a: " + enemy.name);
                    enemyController.TakeDamage(attackDamage);
                }
            }
        }



        private void OnDrawGizmosSelected()
        {
            if (attackPoint == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}
