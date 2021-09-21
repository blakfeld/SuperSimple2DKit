using System;

using Controllers;

using UnityEngine;

namespace Triggers {
    public class ChangeMusicIntensityTrigger : MonoBehaviour {
        private enum IntensityModifier {
            Increment,
            Decrement,
            Specific
        };

        [Header("Music Settings")]
        [SerializeField]
        private IntensityModifier intensityModifier;
        [SerializeField] private int incrementValue;
        [SerializeField] [Range(0, 1)] private float volume = 0.5f;
        [SerializeField] private bool playSpecialClip = false;

        [Header("Trigger Settings")]
        [SerializeField]
        private bool oneOff = true;

        private void OnTriggerEnter2D(Collider2D other) {
            switch (intensityModifier) {
                case IntensityModifier.Increment:
                    MusicManager.Instance.Intensity += 1;
                    break;
                case IntensityModifier.Decrement:
                    MusicManager.Instance.Intensity -= 1;
                    break;
                case IntensityModifier.Specific:
                    MusicManager.Instance.Intensity = incrementValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            MusicManager.Instance.Volume = volume;
            if (playSpecialClip) {
                MusicManager.Instance.UseAltClip = true;
            }

            if (oneOff) {
                enabled = false;
            }
        }
    }
}