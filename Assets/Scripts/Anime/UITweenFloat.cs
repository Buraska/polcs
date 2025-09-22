using DG.Tweening;
using UnityEngine;

namespace Anime
{
    public class UITweenFloat : MonoBehaviour
    {
        [Header("Tween Settings")]
        public float amplitude = 10f;   // how far it moves up & down
        public float speed = 0.5f;        // how fast it moves

        private RectTransform rectTransform;
        private Vector2 startPos;

        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            startPos = rectTransform.anchoredPosition;
        }

        void Update()
        {
            float offset = Mathf.Sin(Time.time * speed) * amplitude;
            rectTransform.anchoredPosition = startPos + new Vector2(0, offset);
        }
    }
}