using UnityEngine;

namespace Controllers.Weapon {
    public class RaycastProjectile : Projectile {
        [Header("References")]
        [SerializeField]
        private LineRenderer line;
        [SerializeField] private LayerMask layerMask;

        [Header("Debug")] [SerializeField] private bool debug;
        [SerializeField] private float debugLineDuration = 0.5f;
        [SerializeField] private Color debugLineColor = Color.red;
        
        private void Start() {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            var currPosition = transform.position;
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

            if (debug) {
                Debug.DrawLine(startPos, endPos, debugLineColor, debugLineDuration);
            }

            line.SetPosition(0, startPos);
            line.SetPosition(1, endPos);
        }
    }
}