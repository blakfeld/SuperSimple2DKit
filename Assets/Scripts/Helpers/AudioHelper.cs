using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Helpers {
    public class AudioHelper {

        private readonly AudioSource _audioSource;

        
        public AudioHelper(AudioSource audioSource) {
            _audioSource = audioSource;
        }

        
        public void PlayRandomAudioOneShot(AudioClip[] audioClips, float pitch = 1f, float volume = 1f) {
            var audioClip = ChooseRandomAudioClip(audioClips);
            if (audioClip == null) return;

            _audioSource.pitch = pitch;
            _audioSource.PlayOneShot(audioClip, volume);
        }

        
        private static AudioClip ChooseRandomAudioClip(IReadOnlyList<AudioClip> audioClips) {
            if (audioClips == null || audioClips.Count == 0) return null;

            var audioCLipIndex = Random.Range(0, audioClips.Count);
            var audioClip = audioClips[audioCLipIndex];

            return audioClip;
        }
    }
}