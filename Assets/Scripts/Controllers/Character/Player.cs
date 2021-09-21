using UnityEngine;

/*
 * Player Class.
 *
 * Everything player specific lives here. Attempt to limit all Input queries to this class.
 */

namespace Controllers.Character {
    public class Player : BaseCharacter {
        private static readonly int Grounded = Animator.StringToHash("grounded");

        [Header("Movement")] [SerializeField] private float fallForgiveness = .2f;
        private float _fallForgivenessCounter;

        private GunManager _gunManager;


        protected override void Start() {
            base.Start();

            _gunManager = GetComponent<GunManager>();
        }


        protected override void Update() {
            base.Update();

            HandleMoveInput();
            HandleShootInput();
        }


        private void CheckIsGrounded() {
            if (!grounded) {
                var shouldForgiveFall = _fallForgivenessCounter < fallForgiveness && isJumping;
                if (shouldForgiveFall) {
                    _fallForgivenessCounter += Time.deltaTime;
                } else {
                    animator.SetBool(Grounded, false);
                }
            } else {
                _fallForgivenessCounter = 0;
                animator.SetBool(Grounded, true);
            }
        }


        private void HandleMoveInput() {
            CheckIsGrounded();
            Move(Input.GetAxis("Horizontal"));
            if (Input.GetButtonDown("Jump")) {
                Jump();
            }
        }


        private void HandleShootInput() {
            if (_gunManager == null && _gunManager.CanShoot) return;

            if (Input.GetMouseButtonDown(0)) {
                _gunManager.EquippedGun.Shoot();
            } else if (Input.GetMouseButtonUp(0)) {
                _gunManager.EquippedGun.StopShoot();
            } else if (Input.GetMouseButton(0)) {
                _gunManager.EquippedGun.ShootAutomatic();
            }
        }
    }
}