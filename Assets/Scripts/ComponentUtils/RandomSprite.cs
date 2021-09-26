using UnityEngine;
using Random = System.Random;

namespace ComponentUtils {
    public class RandomSprite : MonoBehaviour {
        public Sprite[] sprites;
        public SpriteRenderer spriteRenderer;

        private readonly Random _random = new Random();

        public void Start() {
            ApplyRandomSprite();
        }

        public void OnDrawGizmos() {
            if (spriteRenderer.sprite != null) return;
            ApplyRandomSprite();
        }


        public void ApplyRandomSprite() {
            var spriteIndex = _random.Next(0, sprites.Length);
            var sprite = sprites[spriteIndex];
            spriteRenderer.sprite = sprite;
        }
    }
}