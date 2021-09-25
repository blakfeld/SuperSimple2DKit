using Controllers.Character;

using UnityEngine;

namespace Triggers {
    public class AttackVolume : MonoBehaviour {
        [SerializeField] public int damage = 10;

        [SerializeField] private LayerMask layerMask;


        private void OnTriggerEnter2D(Collider2D other) {
            var actionableCollision = layerMask == (layerMask | (1 << other.gameObject.layer));
            Debug.Log("LayerMask: " + layerMask + " Other Layer: " + other.gameObject.layer + " Player Layer: " + LayerMask.NameToLayer("Player") + " " + actionableCollision);
            if (!actionableCollision) return;

            Player.Instance.Damage(damage);
        }
    }
}