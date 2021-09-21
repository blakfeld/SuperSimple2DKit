using System.Collections;

using UnityEngine;

namespace ComponentUtils {
    public class SelfDestruct : MonoBehaviour {
        public float waitForSeconds = 5f;

        private void Start() {
            StartCoroutine(DestroySelf());
        }

        private IEnumerator DestroySelf() {
            yield return new WaitForSeconds(waitForSeconds);
            Destroy(gameObject);
        }
    }
}