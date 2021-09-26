using System.Collections;

using Controllers.Character;

using UnityEngine;

namespace Controllers.Weapon.Effects {
    public class ShellCasing : MonoBehaviour {
        [SerializeField] private float expireDelay = 0.5f;
        
        private Rigidbody2D _body2D;
        private BoxCollider2D _collider2D;

        public void Awake() {
            _body2D = GetComponent<Rigidbody2D>();
            _collider2D = GetComponent<BoxCollider2D>();
            
            StartCoroutine(DisablePhysics());
        }

        
        private IEnumerator DisablePhysics() {
            yield return new WaitForSeconds(expireDelay);
            
            _body2D.simulated = false;
            _collider2D.enabled = false;
        }
    }
}