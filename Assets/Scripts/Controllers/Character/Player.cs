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


        private static Player _instance;

        public static Player Instance {
            get {
                if (_instance == null) {
                    _instance = FindObjectOfType<Player>();
                }

                return _instance;
            }
        }

        protected override void Start() {
            base.Start();

            _gunManager = GetComponent<GunManager>();
        }


        protected override void Update() {
            base.Update();

            HandleMoveInput();
            HandleShootInput();
        }


        private void ApplyFallForgiveness() {
            if (!grounded) {
                var shouldForgiveFall = _fallForgivenessCounter < fallForgiveness && isJumping;
                if (shouldForgiveFall) {
                    _fallForgivenessCounter += Time.deltaTime;
                } else if (_animator) {
                    _animator.SetBool(Grounded, false);
                }
            } else {
                _fallForgivenessCounter = 0;
                if (_animator) _animator.SetBool(Grounded, true);
            }
        }


        private void HandleMoveInput() {
            ApplyFallForgiveness();
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