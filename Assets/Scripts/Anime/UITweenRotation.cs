using DG.Tweening;
using UnityEngine;

namespace Anime
{
    public class UITweenRotation : MonoBehaviour
    {
        [Header("Tween Settings")]
        public float duration = 2f; 
        private RectTransform rectTransform;
        // how fast it moves
        void Start()
        {
            rectTransform = GetComponent<RectTransform>();

            rectTransform.DORotate(new Vector3(0, 0, 360), duration, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear);
            
        }

    }
}