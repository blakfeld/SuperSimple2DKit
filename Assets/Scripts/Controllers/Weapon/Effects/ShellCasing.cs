using System.Collections;

using Controllers.Character;

using UnityEngine;

namespace Controllers.Weapon.Effects {
    public class ShellCasing : PhysicsObject {
        [SerializeField] private float expireDelay = 0.5f;
        
        private Rigidbody2D _body2D;
        private BoxCollider2D _collider2D;

        public void Start() {
            _body2D = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<BoxCollider2D>();
            StartCoroutine(DisablePhysics());
        }

        
        private IEnumerator DisablePhysics() {
            yield return new WaitForSeconds(expireDelay);

            if (!grounded) {
                StartCoroutine(DisablePhysics());
                yield break;
            }
            
            _body2D.bodyType = RigidbodyType2D.Static;
            _collider2D.enabled = false;
        }
    }
}