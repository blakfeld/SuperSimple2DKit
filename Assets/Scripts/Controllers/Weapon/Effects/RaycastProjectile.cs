using System.Collections;

using UnityEngine;

namespace Controllers.Weapon.Effects {
    public class RaycastProjectile : Projectile {
        [Header("References")]
        [SerializeField]
        private LineRenderer line;
        [SerializeField] private LayerMask layerMask;

        [Header("Projectile")]
        [SerializeField]
        private float lineDuration = 0.1f;

        private Gun _gun;

        
        private void Awake() {
            _gun = GetComponent<Gun>();
        }


        private void OnEnable() {
            _gun.OnShoot += OnShoot;
        }
        
        private void OnDisable() {
            _gun.OnShoot -= OnShoot;
        }


        private void OnShoot() {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            var currPosition = _gun.AimTransform.position;
            var rayDir = mousePos - currPosition;
            var startPos = currPosition;
            var endPos = startPos + (rayDir * 1000f);

            var hit = Physics2D.Raycast(startPos, rayDir, Range, layerMask);
            if (hit) {
                hit.transform.SendMessage(
                    HitByProjectileMessage.MessageName,
                    new HitByProjectileMessage(hit, Damage),
                    SendMessageOptions.DontRequireReceiver
                );
                endPos = hit.point;
            }

            
            Debug.DrawLine(startPos, endPos, Color.red, 0.1f);
            StartCoroutine(DrawLine(startPos, endPos));
        }


        private IEnumerator DrawLine(Vector3 startPoint, Vector3 endPoint) {
            // var newLine = Instantiate(line);
            
            line.SetPosition(0, startPoint);
            line.SetPosition(1, endPoint);
            line.enabled = true;

            yield return new WaitForSeconds(lineDuration);

            line.enabled = false;
        }
    }
}