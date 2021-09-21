using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Serialization;

namespace ComponentUtils {
    public class RandomizeLightSettings : MonoBehaviour {
        public HardLight2D targetHardLight;

        [Header("Range")]
        public int minRange = 3;

        public int maxRange = 5;

        [Header("Intensity")]
        [Range(0, 100)]
        public int minIntensity = 75;

        [Range(0, 100)]
        public int maxIntensity = 100;

        [Header("Colors")]
        public Color[] colorOptions;

        private void Start() {
            var range = Random.Range(minRange, maxRange);
            targetHardLight.Range = range;

            var intensity = Random.Range(minIntensity, maxIntensity) / 100.0f;
            targetHardLight.Intensity = intensity;

            var color = ChooseColor();
            targetHardLight.Color = color;
        }

        private Color ChooseColor() {
            var colorIndex = Random.Range(0, colorOptions.Length);
            var color = colorOptions[colorIndex];

            return color;
        }
    }
}