using UnityEngine;

/*
 * Aim bone's belonging to a Character at a target.
 */

namespace Controllers.Character {
    public class AimManager : MonoBehaviour {
        [Header("Targeting")]
        [SerializeField] private Transform targetTransform;
        [SerializeField] private bool targetMouse = true;
        [SerializeField] private Transform pivotBone;
        
        [Header("Animation")]
        [Range(0, 1)] [SerializeField] private float animWeight;

        private BaseCharacter _baseCharacter;
        private GunManager _gunManager;

        private void Start() {
            _baseCharacter = GetComponent<BaseCharacter>();
            if (_baseCharacter == null) {
                Debug.LogError("AimManager must be attached to a Character.");
            }
            
            _gunManager = GetComponent<GunManager>();
            if (_gunManager == null) {
                Debug.LogError("GunManager is required for AimManager");
            }
        }

        private void Update() {
            if (_gunManager.EquippedGun == null) return;

            _baseCharacter.isFacingRight = _baseCharacter.ScreenPoint.x < Input.mousePosition.x;
            
            var aimTransform = _gunManager.EquippedGun.AimTransform;
            var target = targetMouse ? MousePosition : targetTransform.position;
            AimAt(aimTransform, target);
        }

        private static Vector3 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);

        private void AimAt(Transform aimTransform, Vector3 targetPos) {
            // It's a 2D game - Zero out z.
            targetPos.z = 0f;
            
            var targetDir = targetPos - aimTransform.position;
            var rotDir = aimTransform.right * (_baseCharacter.isFacingRight ? 1 : -1);
            var aimAt = Quaternion.FromToRotation(rotDir, targetDir);
            var blendedRot = Quaternion.Slerp(Quaternion.identity, aimAt, animWeight);
            
            pivotBone.rotation *= blendedRot;
        }
    }
}