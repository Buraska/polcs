using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;

namespace Animation
{
    public class MadnessAnimation : MonoBehaviour
    {
        public Volume MadnessVolume;
        public AnimationCurve madnessCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        private float _current_weight;


        public IEnumerator IncreaseVolumeTween(float additionalValue, float speed = 1f)
        {
            float target = Mathf.Clamp01(MadnessVolume.weight + additionalValue);

            // Прервать прошлую анимацию, если была
            DOTween.Kill(MadnessVolume);

            // Анимировать вес с кастомной кривой
            var tween = DOTween.To(() => MadnessVolume.weight, 
                    x => MadnessVolume.weight = x, 
                    target, 
                    speed)
                .SetEase(madnessCurve)
                .SetTarget(MadnessVolume);
            yield return tween.WaitForCompletion();
        }
    }

}