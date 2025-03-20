using UnityEngine;

namespace Eco
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private int health = 30;
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void TakeDamage(int damage)
        {
            Debug.Log("Enemigo recibió daño: " + damage);
            health -= damage;

            if (health <= 0)
            {
                Debug.Log("Enemigo ha muerto: " + gameObject.name);
                Die();
            }
        }

        private void Die()
        {
            Debug.Log("Enemigo ha muerto");
            Destroy(gameObject);
        }


        private System.Collections.IEnumerator FlashDamageEffect()
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
        }
    }
}
