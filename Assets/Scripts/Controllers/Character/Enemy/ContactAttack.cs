using System.Collections;
using System.Linq;

using UnityEngine;

namespace Controllers.Character.Enemy {
    public class ContactAttack : MonoBehaviour {
        [Header("References")]
        [SerializeField]
        private GameObject attackVolume;

        [Header("Enemy Settings")]
        [SerializeField]
        private EnemyState[] subscribedStates = {EnemyState.Attacking};
        private EnemyState exitState = EnemyState.Alerted;

        [Header("Settings")] [SerializeField] private float attackDuration = 0.1f;
        [SerializeField] private float range = 3f;
        [SerializeField] private float attackCooldown = 0.25f;

        [Header("State")] [SerializeField] private bool isAttacking;
        [SerializeField] private bool canAttack = true;

        private BaseEnemy _enemy;
        private Coroutine _exitStateRoutine;


        private void Start() {
            _enemy = GetComponent<BaseEnemy>();
            if (!_enemy || _enemy == null) Debug.LogError("ContactAttack must be placed on an Enemy Character");

            attackVolume.SetActive(false);
        }


        private void Update() {
            if (!subscribedStates.Contains(_enemy.state)) return;
            if (canAttack && !isAttacking) StartCoroutine(Attack());

            var distFromPlayer = _enemy.DistanceFromPlayer();
            if (Mathf.Abs(distFromPlayer.x) > range || Mathf.Abs(distFromPlayer.y) > range) {
                _exitStateRoutine = StartCoroutine(TransitionToExitState());
            } else if (_exitStateRoutine != null) {
                StopCoroutine(_exitStateRoutine);
            }
        }


        private IEnumerator Attack() {
            attackVolume.SetActive(true);
            
            yield return new WaitForSeconds(attackDuration);

            attackVolume.SetActive(false);
            StartCoroutine(Cooldown());
        }


        private IEnumerator Cooldown() {
            canAttack = false;
            
            yield return new WaitForSeconds(attackCooldown);

            canAttack = true;
        }


        private IEnumerator TransitionToExitState() {
            yield return new WaitForSeconds(0.5f);

            _enemy.RequestStateTransition(EnemyState.Alerted);
        }
    }
}