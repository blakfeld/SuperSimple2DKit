using Helpers;

using UnityEngine;

/*
 * Listen for animation events and play a random footstep audio clip.
 */

namespace Controllers.Character {
    public class FootStepManager : MonoBehaviour {
        [SerializeField] private AudioClip[] clips;
        [SerializeField] private float minPitch = 0.9f;
        [SerializeField] private float maxPitch = 1.1f;
        [SerializeField] private float minVolume = 0.05f;
        [SerializeField] private float maxVolume = 0.15f;

        private AudioHelper _audioHelper;
        private BaseCharacter _baseCharacter;


        private void Awake() {
            var audioSource = GetComponent<AudioSource>();
            _audioHelper = new AudioHelper(audioSource);

            _baseCharacter = GetComponent<BaseCharacter>();
        }


        public void OnJumpLand() {
            Debug.Log("Playing");
            PlayStepSound();
        }


        public void PlayStepSound() {
            var pitch = Random.Range(minPitch, maxPitch);
            var volume = Random.Range(minVolume, maxVolume);
            _audioHelper.PlayRandomAudioOneShot(clips, pitch, volume);
        }
    }
}