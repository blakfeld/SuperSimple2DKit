using System.Collections;
using System.Linq;

using UnityEngine;

namespace Controllers.Character.Enemy {
    public class PatrolManager : MonoBehaviour {
        [Header("References")]
        [SerializeField]
        private PatrolBoundary patrolBoundary;

        [Header("Enemy Settings")]
        [SerializeField]
        private EnemyState[] subscribedStates = {EnemyState.Idle};

        [Header("Patrol Settings")]
        [SerializeField] private float turnDelay = 0.5f;

        [Header("State")] [SerializeField] private bool patrolRight = true;
        [SerializeField] private bool canMove = true;
        [SerializeField] private bool isMoving = false;

        private float _recoveryTimer;
        private BaseEnemy _enemy;


        private void Awake() {
            _enemy = GetComponent<BaseEnemy>() ?? GetComponentInParent<BaseEnemy>();
            if (_enemy == null) Debug.LogError("PatrolManager must belong to a Enemy component!");
        }


        private void OnEnable() {
            _enemy.OnWallCollision += OnWallCollision;
            if (patrolBoundary != null) patrolBoundary.onHitPatrolBoundary += OnBoundaryCollision;
        }


        private void OnDisable() {
            _enemy.OnWallCollision -= OnWallCollision;
            if (patrolBoundary != null) patrolBoundary.onHitPatrolBoundary -= OnBoundaryCollision;
        }


        private void Update() {
            if (_recoveryTimer > 0) _recoveryTimer -= Time.deltaTime;
            if (!subscribedStates.Contains(_enemy.state)) {
                if (isMoving) {
                    _enemy.Move(0);
                    isMoving = false;
                }

                return;
            }

            var dirMultiplier = patrolRight ? 1.0f : -1.0f;
            var horizontal = (canMove ? 1 : 0) * dirMultiplier;
            isMoving = horizontal != 0;
            _enemy.Move(horizontal);
        }


        private void OnWallCollision(RaycastHit2D hit, bool onRight) {
            OnBoundaryCollision();
        }


        private void OnBoundaryCollision() {
            if (_recoveryTimer > 0) return;
            _recoveryTimer += turnDelay + 0.500f;
            
            StartCoroutine(ReversePatrol());
        }
        

        private IEnumerator ReversePatrol() {
            canMove = false;

            yield return new WaitForSeconds(turnDelay);

            patrolRight = !patrolRight;
            canMove = true;
        }
    }
}