using System;

using Cinemachine;

using UnityEditor.U2D;

using UnityEngine;

namespace Controllers.Weapon.Effects {
    public class CameraShake : MonoBehaviour {
        private CinemachineImpulseSource _impulseSource;
        private Gun _gun;


        private void Awake() {
            _gun = GetComponent<Gun>();
            _impulseSource = GetComponent<CinemachineImpulseSource>();
            if (!_impulseSource) {
                Debug.LogError("Gun required a CinemachineImpulseSource component for Camera shake.");
            }
        }


        private void OnEnable() {
            _gun.OnShoot += OnShoot;
        }


        private void OnDisable() {
            _gun.OnShoot -= OnShoot;
        }


        private void OnShoot() {
            _impulseSource.GenerateImpulse();
        }
    }
}