using Controllers.Weapon;

using UnityEngine;

namespace Controllers {
    public class EnemyController : MonoBehaviour {
        [Header("Stats")] [SerializeField] private int health = 10;

        [SerializeField] private int maxHealth = 10;

        private void Update() {
            if (health <= 0) {
                Die();
            }
        }

        private void Die() {
            Destroy(gameObject);
        }

        public void HitByRaycastProjectile(HitByProjectileMessage msg) {
            health -= msg.Damage;
        }
    }
}