using System;

using UnityEngine;

namespace Controllers {
    public class MusicManager : MonoBehaviour {
        [Header("Tracks")] [SerializeField] private AudioClip[] musicClips;
        [SerializeField] private AudioClip[] altMusicClips;
        [SerializeField] private bool useAltClip;

        [Header("Music Settings")]
        [SerializeField]
        private int intensity;

        [SerializeField] [Range(0, 1)] private float volume = 0.2f;

        private AudioSource _audioSource;

        private static MusicManager _instance;
        public static MusicManager Instance => _instance ??= FindObjectOfType<MusicManager>();

        
        public int Intensity {
            get => intensity;
            set => intensity = Math.Min(value, musicClips.Length - 1);
        }

        
        public float Volume {
            get => volume;
            set => volume = Math.Max(0, Mathf.Min(value, 1.0f));
        }

        
        public bool UseAltClip {
            get => useAltClip;
            set => useAltClip = value;
        }

        
        private void Start() {
            _audioSource = GetComponent<AudioSource>();
        }


        private void Update() {
            if (_audioSource.isPlaying) return;
            if (intensity >= musicClips.Length) {
                Debug.LogError("Desired intensity is greater than number of available tracks.");
                return;
            }

            AudioClip expectedAudioClip;
            if (useAltClip) {
                expectedAudioClip = intensity < altMusicClips.Length
                    ? altMusicClips[intensity]
                    : null;
            } else {
                expectedAudioClip = intensity < musicClips.Length
                    ? musicClips[intensity]
                    : null;
            }

            if (expectedAudioClip == null) {
                Debug.Log("Unable to select music track!");
                return;
            }

            if (_audioSource.clip != expectedAudioClip) {
                _audioSource.clip = expectedAudioClip;
            }

            _audioSource.volume = volume;
            _audioSource.Play();
        }
    }
}