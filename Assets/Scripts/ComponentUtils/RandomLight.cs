using System;
using System.ComponentModel;

using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

using Random = UnityEngine.Random;

namespace ComponentUtils {
    public class RandomLight : MonoBehaviour {
        [Header("Radius")] [SerializeField] private int minRadius = 5;
        [SerializeField] private int maxRadius = 10;
        [SerializeField] private int innerRadiusOffset = 3;

        [Header("Intensity")] [SerializeField] private float minIntensity = 0.4f;
        [SerializeField] private float maxIntensity = 0.7f;
        

        [Header("Colors")] [SerializeField] private Color[] colorOptions;

        public Light2D TargetLight { get; private set; }


        private void Awake() {
            TargetLight = GetComponent<Light2D>();
            ApplySettings();
        }


        public void ApplySettings() {
            var outerRadius = Random.Range(minRadius, maxRadius);
            TargetLight.pointLightOuterRadius = outerRadius;

            var innerRadius = outerRadius - innerRadiusOffset;
            TargetLight.pointLightInnerRadius = innerRadius;

            var intensity = Random.Range(minIntensity, maxIntensity);
            TargetLight.intensity = intensity;

            var color = ChooseColor();
            TargetLight.color = color;
            
        }


        private Color ChooseColor() {
            var colorIndex = Random.Range(0, colorOptions.Length);
            var color = colorOptions[colorIndex];

            return color;
        }
    }
}