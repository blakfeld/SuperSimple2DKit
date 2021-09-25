using UnityEngine;

namespace ComponentUtils {
    public class RandomizeLightSettings : MonoBehaviour {
        [Header("References")]
        [SerializeField]
        private HardLight2D targetHardLight;
        
        [Header("Range")] [SerializeField] private int minRange = 3;
        [SerializeField] private int maxRange = 5;

        [Header("Intensity")]
        [Range(0, 100)]
        [SerializeField]
        private int minIntensity = 75;

        [Range(0, 100)] [SerializeField] private int maxIntensity = 100;

        [Header("Colors")] [SerializeField] private Color[] colorOptions;


        private void Awake() {
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