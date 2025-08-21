using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;
using DigitalRuby.SoundManagerNamespace;
using JetBrains.Annotations;

namespace Anime
{
    public class MadnessAnimation : MonoBehaviour
    {
        public Volume MadnessVolume;
        public AnimationCurve madnessCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        [CanBeNull] public AudioSource MadnessAudio;
        private const float AudioVolumeCoef = 0.25f;


        private float _current_weight;


        public IEnumerator IncreaseVolumeTween(float additionalValue, float duration = 1f, bool mute = false)
        {


            var target = Mathf.Clamp01(MadnessVolume.weight + additionalValue);

            DOTween.Kill(MadnessVolume, true);

            var visualTween = DOTween.To(() => MadnessVolume.weight,
                    x => MadnessVolume.weight = x,
                    target,
                    duration)
                .SetEase(madnessCurve)
                .SetTarget(MadnessVolume);

            if (!mute && MadnessAudio != null)
            {
                if (!MadnessAudio.isPlaying)
                {
                    MadnessAudio.loop = true;
                    MadnessAudio.Play();
                    MadnessAudio.volume = 0;
                }
                var audioTarget = Mathf.Clamp01(MadnessAudio.volume + additionalValue * AudioVolumeCoef);
                var audioTween = DOTween.To(() => MadnessAudio.volume,
                        x => MadnessAudio.volume = x,
                        audioTarget,
                        duration)
                    .SetEase(madnessCurve)
                    .SetTarget(MadnessVolume);    
            }
            yield return visualTween.WaitForCompletion();
        }
    }

}