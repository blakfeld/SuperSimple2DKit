using Controllers.Weapon;

using Unity.Mathematics;

using UnityEngine;

namespace ComponentUtils {
    public class Shootable : MonoBehaviour {
        [Header("References")] public GameObject hitReaction;

        public void HitByRaycastProjectile(HitByProjectileMessage msg) {
            var hit = msg.Hit;
            var rotation = Quaternion.FromToRotation(Vector3.right, hit.normal);
            rotation *= quaternion.Euler(0, 0, -90);

            Instantiate(hitReaction, hit.point, rotation);
        }
    }
}