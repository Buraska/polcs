using System;
using System.Collections;
using System.Reflection;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

namespace EventActions
{
    public class AnimateAnyEA : EventAction
    {
        
        [Tooltip("The component that has the field/property you want to animate")]
        public Component target;

        [Tooltip("The name of the float field or property to animate (case-sensitive)")]
        public string fieldName = "pitch";

        [Tooltip("Target value for the animation")]
        public float endValue = 2f;

        [Tooltip("Duration in seconds")]
        public float duration = 2f;

        [Tooltip("Easing type")]
        public Ease ease = Ease.Linear;

        private Tween tween;
        
        public override IEnumerator ActionCoroutine()
        {
            if (target == null || string.IsNullOrEmpty(fieldName))
            {
                Debug.LogWarning("TweenFieldByName: Target or field name not set!");
                yield break;
            }

            // Try to get the field or property via reflection
            Type type = target.GetType();
            FieldInfo field = type.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            PropertyInfo prop = type.GetProperty(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (field == null && prop == null)
            {
                Debug.LogWarning($"TweenFieldByName: No field or property '{fieldName}' found on {target.name}");
                yield break;
            }

            DOGetter<float> getter;
            DOSetter<float> setter;

            if (field != null)
            {
                getter = () => (float)field.GetValue(target);
                setter = x => field.SetValue(target, x);
            }
            else
            {
                getter = () => (float)prop.GetValue(target);
                setter = x => prop.SetValue(target, x);
            }
            tween = DOTween.To(getter, setter, endValue, duration)
                .SetEase(ease);
            
        }
        

        void Start()
        {

        }

        void OnDestroy()
        {
            tween?.Kill();
        }
    }
}