using Eco;
using UnityEngine;

namespace Eco
{
    public class CombatManager : MonoBehaviour
    {
        public static CombatManager Instance;

        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private float attackRange = 3.0f;
        [SerializeField] private Transform attackPoint;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        /// <summary>
        /// Llamado por el jugador para ejecutar un ataque con daño personalizado.
        /// </summary>
        public void RealizarAtaque(int damage)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            Debug.Log("Enemigos detectados: " + hitEnemies.Length);

            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("Intentando golpear: " + enemy.name);

                if (enemy.TryGetComponent<EnemyController>(out EnemyController enemyController))
                {
                    Debug.Log("Golpeando a: " + enemy.name);
                    enemyController.TakeDamage(damage);
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
