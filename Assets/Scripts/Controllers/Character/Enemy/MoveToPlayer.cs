using System.Linq;

using UnityEngine;

namespace Controllers.Character.Enemy {
    public class MoveToPlayer : MonoBehaviour {
        [Header("Enemy Settings")]
        [SerializeField]
        private EnemyState[] subscribedStates = {EnemyState.Alerted};
        [SerializeField] private EnemyState desiredExitState = EnemyState.Attacking;

        [Header("Range")] [SerializeField] private float desiredRangeOfPlayer = 2f;

        [Header("State")] [SerializeField] private bool isPursuing = false;

        private BaseEnemy _enemy;


        private void Start() {
            _enemy = GetComponent<BaseEnemy>();
            if (!_enemy || _enemy == null) Debug.LogError("MoveToPlayer must be placed on an Enemy Character.");
        }


        private void Update() {
            if (!subscribedStates.Contains(_enemy.state)) {
                isPursuing = false;
                return;
            }

            var distanceFromPlayer = _enemy.DistanceFromPlayer();
            
            isPursuing = Mathf.Abs(distanceFromPlayer.x) > desiredRangeOfPlayer;
            if (isPursuing) {
                var horizontal = distanceFromPlayer.x > 0 ? 1 : -1;
                _enemy.Move(horizontal);
            } else {
                _enemy.Move(0);
                _enemy.RequestStateTransition(desiredExitState);
            }
        }
    }
}