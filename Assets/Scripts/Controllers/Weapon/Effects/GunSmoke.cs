using System.Collections;

using UnityEngine;

namespace Controllers.Weapon.Effects {
    public class GunSmoke : MonoBehaviour {
        [Header("References")]
        [SerializeField]
        private ParticleSystem smokeParticles;

        [Header("Settings")] [SerializeField] private float startDelay = 1f;
        [SerializeField] private float endDelay = 0.5f;

        private Coroutine _playCoroutine;
        private Coroutine _stopCoroutine;

        private Gun _gun;

        public void Awake() {
            _gun = GetComponent<Gun>();
        }

        public void OnEnable() {
            _gun.OnShoot += PlayGunSmokeEffect;
            _gun.OnStopShoot += StopGunSmokeEffect;
        }


        public void OnDisable() {
            _gun.OnShoot -= PlayGunSmokeEffect;
            _gun.OnStopShoot -= StopGunSmokeEffect;
        }

        public void PlayGunSmokeEffect() {
            if (_stopCoroutine != null) {
                StopCoroutine(_playCoroutine);
            }

            _playCoroutine = StartCoroutine(PlayEffect());
        }

        public void StopGunSmokeEffect() {
            if (_playCoroutine != null) {
                StopCoroutine(_playCoroutine);
            }

            _stopCoroutine = StartCoroutine(StopEffect());
        }

        private IEnumerator PlayEffect() {
            yield return new WaitForSeconds(startDelay);
            smokeParticles.Play();
        }

        private IEnumerator StopEffect() {
            yield return new WaitForSeconds(endDelay);
            smokeParticles.Stop();
        }
    }
}