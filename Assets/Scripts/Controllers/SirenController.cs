using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.PlayerLoop;

namespace Controllers {
    public class SirenController : MonoBehaviour {
        [Header("Colors")]
        public Color startColor;

        public Color endColor;
        public float strobeDuration = 3.0f;

        [Range(0, 100)]
        public int graphicAlpha = 80;

        [Range(0, 100)]
        public int lightAlpha = 100;

        [Header("Lights")]
        public HardLight2D[] hardLight2Ds;

        public Light2D[] light2Ds;
        public bool pulseIntensity;
        public float minIntensity;
        public float maxIntensity;
        public int strobeMultiplier = 4;

        [Header("Graphics")]
        public SpriteRenderer[] spriteRenderers;


        public void Update() {
            var lightColorLerp = ColorLerp(lightAlpha);
            var lerpedIntensity = IntensityLerp();
            foreach (var hardLight2D in hardLight2Ds) {
                hardLight2D.Color = lightColorLerp;
                if (pulseIntensity) {
                    hardLight2D.Intensity = lerpedIntensity;
                }
            }
            
            foreach (var light2D in light2Ds) {
                light2D.color = lightColorLerp;
                if (pulseIntensity) {
                    light2D.intensity = lerpedIntensity;
                }
            }

            var graphicColorLerp = ColorLerp(graphicAlpha);
            foreach (var spriteRenderer in spriteRenderers) {
                spriteRenderer.color = graphicColorLerp;
            }
        }

        private float IntensityLerp() {
            var duration = strobeDuration / strobeMultiplier;
            return Mathf.Lerp(
                minIntensity,
                maxIntensity,
                Mathf.PingPong(Time.time, duration) / duration
            );
        }

        private Color ColorLerp(int alphaPercent) {
            var alpha = alphaPercent / 100.0f;
            var colorOne = new Color(startColor.r, startColor.g, startColor.b, alpha);
            var colorTwo = new Color(endColor.r, endColor.g, endColor.b, alpha);
            return Color.Lerp(
                colorOne,
                colorTwo,
                Mathf.PingPong(Time.time, strobeDuration) / strobeDuration
            );
        }
    }
}