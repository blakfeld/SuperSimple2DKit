using Helpers;

using UnityEngine;

namespace Controllers.Weapon.Effects {
    public class GunShotAudio : MonoBehaviour {
        [Header("Audio Clips")]
        [SerializeField]
        private AudioClip[] shootClips;
        [SerializeField] private AudioClip[] shootStopClips;

        [Header("Audio Variation")]
        [SerializeField]
        private float minPitch = 0.8f;
        [SerializeField] private float maxPitch = 1.1f;
        [SerializeField] private float minVolume = 0.3f;
        [SerializeField] private float maxVolume = 0.5f;

        private AudioHelper _audioHelper;
        private AudioSource _audioSource;
        private Gun _gun;

        private void Awake() {
            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null) {
                Debug.LogError("AudioSource component required.");
            }

            _audioHelper = new AudioHelper(_audioSource);

            _gun = GetComponent<Gun>();
            if (_gun == null) {
                Debug.LogError("Gun component required.");
            }
        }

        public void OnEnable() {
            _gun.OnShoot += OnShoot;
            _gun.OnStopShoot += OnShootStop;
        }

        public void OnDisable() {
            _gun.OnShoot -= OnShoot;
            _gun.OnStopShoot -= OnShootStop;
        }

        public void OnShoot() {
            var pitch = Random.Range(minPitch, maxPitch);
            var volume = Random.Range(minVolume, maxVolume);
            _audioHelper.PlayRandomAudioOneShot(shootClips, pitch, volume);
        }

        public void OnShootStop() {
            _audioSource.Stop();

            var pitch = Random.Range(minPitch, maxPitch);
            var volume = Random.Range(minVolume, maxVolume);
            _audioHelper.PlayRandomAudioOneShot(shootStopClips, pitch, volume);
        }
    }
}