using System.Collections.Generic;
using System.Linq;

using UnityEditor;

using UnityEngine;

namespace Controllers.Character.Enemy {
    public enum EnemyState {
        Alerted,
        Attacking,
        Idle,
        Suspicious
    }

    public class BaseEnemy : BaseCharacter {
        public delegate void WallCollisionEvent(RaycastHit2D hit, bool onRight);
        public event WallCollisionEvent OnWallCollision;

        [Header("World Detection")]
        [SerializeField]
        private float wallDetectionOffset;
        [SerializeField] private float wallDetectionRange = 1f;
        [SerializeField] private LayerMask wallLayerMask;

        [Header("Player Detection")]
        [SerializeField]
        private int alertRadius;
        [SerializeField] private float alertCooldown = 1f;
        [SerializeField] private int suspicionRadius;
        [SerializeField] private float suspicionCooldown = 2f;

        private float _alertTimer;
        private float _suspicionTimer;

        [Header("State")] public EnemyState state = EnemyState.Idle;


        private static readonly IReadOnlyDictionary<EnemyState, EnemyState[]> _validStateTransitions =
            new Dictionary<EnemyState, EnemyState[]> {
                {EnemyState.Alerted, new[] {EnemyState.Attacking, EnemyState.Idle, EnemyState.Suspicious}},
                {EnemyState.Attacking, new[] {EnemyState.Alerted, EnemyState.Suspicious}},
                {EnemyState.Idle, new[] {EnemyState.Alerted, EnemyState.Suspicious}},
                {EnemyState.Suspicious, new[] {EnemyState.Alerted, EnemyState.Idle}},
            };


        private void OnDrawGizmosSelected() {
            var currTransform = transform;
            Handles.DrawWireDisc(currTransform.position, currTransform.forward, alertRadius);
            Handles.DrawWireDisc(currTransform.position, currTransform.forward, suspicionRadius);
        }


        protected override void Update() {
            base.Update();

            if (_alertTimer > 0) _alertTimer -= Time.deltaTime;
            if (_suspicionTimer > 0) _suspicionTimer -= Time.deltaTime;

            if (state != EnemyState.Attacking) {
                var distFromPlayer = DistanceFromPlayer();
                if (distFromPlayer.x < alertRadius) {
                    RequestStateTransition(EnemyState.Alerted);
                    _alertTimer += alertCooldown;
                    _suspicionTimer += suspicionCooldown;
                } else if (_alertTimer <= 0) {
                    RequestStateTransition(EnemyState.Suspicious);
                }

                if (state != EnemyState.Alerted) {
                    if (distFromPlayer.x < suspicionRadius) {
                        RequestStateTransition(EnemyState.Suspicious);
                        _suspicionTimer += suspicionCooldown;
                    } else if (_suspicionTimer <= 0) {
                        RequestStateTransition(EnemyState.Idle);
                    }
                }
            }

            HandleWallCollisions();
        }


        public Vector2 DistanceFromPlayer() {
            var playerPos = Player.Instance.gameObject.transform.position;
            return new Vector2(playerPos.x - transform.position.x, playerPos.y - transform.position.y);
        }


        private void HandleWallCollisions() {
            var leftRaySrc = new Vector2(transform.position.x, transform.position.y - wallDetectionOffset);
            var leftHit = Physics2D.Raycast(leftRaySrc, Vector2.left, wallDetectionRange, wallLayerMask);
            Debug.DrawRay(leftRaySrc, Vector2.left * wallDetectionRange, Color.yellow);

            if (leftHit.collider != null) OnWallCollision?.Invoke(leftHit, false);

            var rightRaySrc = new Vector2(transform.position.x, transform.position.y + wallDetectionOffset);
            var rightHit = Physics2D.Raycast(rightRaySrc, Vector2.right, wallDetectionRange, wallLayerMask);
            Debug.DrawRay(rightRaySrc, Vector2.right * wallDetectionRange, Color.blue);

            if (rightHit.collider != null) OnWallCollision?.Invoke(rightHit, true);
        }


        public bool RequestStateTransition(EnemyState newState) {
            if (!_validStateTransitions[state].Contains(newState)) return false;
            state = newState;
            return true;
        }
    }
}