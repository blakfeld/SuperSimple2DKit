using UnityEngine;

namespace Controllers.Weapon {
    public class HitByProjectileMessage {
        public const string MessageName = "HitByProjectile";

        public RaycastHit2D Hit { get; }
        public int Damage { get; }

        public HitByProjectileMessage(RaycastHit2D hit, int damage) {
            Hit = hit;
            Damage = damage;
        }
    }

    public class Projectile : MonoBehaviour {
        [Header("Stats")] [SerializeField] private int damage = 10;
        [SerializeField] private int range = 100;

        public int Damage => damage;
        public int Range => range;
    }
}