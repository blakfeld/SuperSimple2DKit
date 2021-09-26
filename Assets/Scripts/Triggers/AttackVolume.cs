using Controllers.Character;

using UnityEngine;

namespace Triggers {
    public class AttackVolume : MonoBehaviour {
        [SerializeField] public int damage = 10;
        [SerializeField] private LayerMask layerMask;


        private void OnTriggerEnter2D(Collider2D other) {
            var actionableCollision = layerMask == (layerMask | (1 << other.gameObject.layer));
            if (!actionableCollision) return;

            Player.Instance.Damage(damage);
        }
    }
}