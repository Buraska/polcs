using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;
using DigitalRuby.SoundManagerNamespace;

namespace Animation
{
    public class MadnessAnimation : MonoBehaviour
    {
        public Volume MadnessVolume;
        public AnimationCurve madnessCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        public AudioSource MadnessAudio;


        private float _current_weight;


        public IEnumerator IncreaseVolumeTween(float additionalValue, float speed = 1f)
        {
            if (!MadnessAudio.isPlaying)
            {
                MadnessAudio.loop = true;
                MadnessAudio.Play();
                MadnessAudio.volume = 0;
            };
            float target = Mathf.Clamp01(MadnessVolume.weight + additionalValue);

            // Прервать прошлую анимацию, если была
            DOTween.Kill(MadnessVolume, true);

            // Анимировать вес с кастомной кривой
            var visualTween = DOTween.To(() => MadnessVolume.weight, 
                    x => MadnessVolume.weight = x, 
                    target, 
                    speed)
                .SetEase(madnessCurve)
                .SetTarget(MadnessVolume);
            
            var audioTween = DOTween.To(() => MadnessAudio.volume, 
                    x => MadnessAudio.volume = x, 
                    target, 
                    speed)
                .SetEase(madnessCurve)
                .SetTarget(MadnessVolume);
            
            yield return visualTween.WaitForCompletion();
        }
    }

}