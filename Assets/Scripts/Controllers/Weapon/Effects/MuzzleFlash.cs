using System.Collections;

using ComponentUtils;

using UnityEngine;

namespace Controllers.Weapon.Effects {
    public class MuzzleFlash : MonoBehaviour {
        public RandomSprite flashSprite;
        public RandomLight randomLight;
        public float fireRate = 0.1f;

        private Gun _gun;


        private void Awake() {
            _gun = GetComponent<Gun>();
        }


        private void OnEnable() {
            _gun.OnShoot += OnShoot;
            _gun.OnStopShoot += OnStopShoot;
        }


        private void OnDisable() {
            _gun.OnShoot -= OnShoot;
            _gun.OnStopShoot -= OnStopShoot;
        }


        private void OnShoot() {
            StartCoroutine(FlashSprite());
            StartCoroutine(FlashLight());
        }


        private void OnStopShoot() {
            flashSprite.spriteRenderer.enabled = false;
            randomLight.TargetLight.enabled = false;
        }


        private IEnumerator FlashSprite() {
            flashSprite.ApplyRandomSprite();
            flashSprite.spriteRenderer.enabled = true;

            yield return new WaitForSeconds(fireRate);
            
            flashSprite.spriteRenderer.enabled = false;
        }


        private IEnumerator FlashLight() {
            randomLight.ApplySettings();
            randomLight.TargetLight.enabled = true;

            yield return new WaitForSeconds(fireRate);

            randomLight.TargetLight.enabled = false;
        }
    }
}