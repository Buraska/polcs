using System;
using DG.Tweening;
using UnityEngine;

namespace Anime
{
    public class TweenFloat : MonoBehaviour
    {

        public float amplitude;
        public float duration;

        private Vector3 position;
        void Start()
        {
            position = transform.position;
        }

        void Update()
        {
            float offset = Mathf.Sin(Time.time * duration) * amplitude;
            transform.position = position + new Vector3(0, offset);
        }
    }
}