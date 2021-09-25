using Controllers.Character;

using UnityEngine;

namespace Triggers {
    public class PatrolTrigger : MonoBehaviour {

        private PatrolBoundary _boundary;


        private void Start() {
            _boundary = GetComponentInParent<PatrolBoundary>();
        }
        
        private void OnTriggerEnter2D(Collider2D other) {
            if (!_boundary || _boundary == null) return;
            _boundary.onHitPatrolBoundary?.Invoke();
        }
    }
}