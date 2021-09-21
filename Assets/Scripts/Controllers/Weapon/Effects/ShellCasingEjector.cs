using UnityEngine;

namespace Controllers.Weapon.Effects {
    public class ShellCasingEjector : MonoBehaviour {
        [Header("References")]
        [SerializeField]
        private GameObject toEject;
        [SerializeField] private Transform spawnTransform;

        [Header("Ejection Properties")]
        [SerializeField]
        private float ejectionThrust = 100.0f;
        [SerializeField] private float randomVariance = 100.0f;

        private Character.BaseCharacter _character;
        private Gun _gun;

        private void Awake() {
            _character = GetComponentInParent<Character.BaseCharacter>();
            _gun = GetComponent<Gun>();
        }

        private void OnEnable() {
            _gun.OnShoot += Eject;
        }

        private void Eject() {
            if (_character == null) return;

            var ejected = Instantiate(toEject, spawnTransform);
            spawnTransform.DetachChildren();

            var currTransform = transform;
            var ejectedRigidBody = ejected.GetComponent<Rigidbody2D>();

            var rightVecMultiplier = _character.isFacingRight ? -1 : 1;
            var rightVec = currTransform.right * rightVecMultiplier;
            
            var upIntensity = Random.Range(1.0f, 1.5f);
            var rightIntensity = Random.Range(0.5f, 1f);
            var forceDirection = ((currTransform.up * upIntensity) + (rightVec * rightIntensity));

            var thrust = Random.Range(ejectionThrust - randomVariance, ejectionThrust + randomVariance);
            ejectedRigidBody.AddForce(forceDirection * thrust);
        }
    }
}