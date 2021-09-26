using Cinemachine;

using UnityEngine;

namespace Controllers.Weapon {
    public class Gun : Weapon {
        public delegate void ShootEvent();
        public event ShootEvent OnShoot;

        public delegate void StopShootEvent();
        public event StopShootEvent OnStopShoot;

        [Header("References")]
        [SerializeField]
        private Transform gripTransform;
        public Transform GripTransform => gripTransform.transform;

        [Header("Projectile")]
        [SerializeField]
        private GameObject projectile;
        [SerializeField] private Transform projectileSpawn;
        [SerializeField] private float fireRate = 0.1f;
        [SerializeField] private bool automaticFireEnabled;

        [Header("Effects")] [SerializeField] private bool cameraShakeEnabled = true;
        private CinemachineImpulseSource _impulseSource;

        [Header("State")] public bool isFiring;

        public Transform AimTransform => projectileSpawn;
        private float _fireRateTimer;


        private void Start() {
            _impulseSource = GetComponent<CinemachineImpulseSource>();
            if (!_impulseSource && cameraShakeEnabled) {
                Debug.LogError("Gun required a CinemachineImpulseSource component for Camera shake.");
            }
        }


        private void Update() {
            if (_fireRateTimer > 0.0f) {
                _fireRateTimer -= Time.deltaTime;
            }
        }


        public void Shoot() {
            if (_fireRateTimer > 0f) return;
            isFiring = true;

            // Instantiate(projectile, projectileSpawn);
            // projectileSpawn.DetachChildren();

            _fireRateTimer = fireRate;
            OnShoot?.Invoke();
        }


        public void ShootAutomatic() {
            if (automaticFireEnabled) Shoot();
        }


        public void StopShoot() {
            isFiring = false;
            OnStopShoot?.Invoke();
        }
    }
}