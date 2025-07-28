using UnityEngine;

namespace Puzzles.StarMap
{
    public class SpriteFader : MonoBehaviour
    {
        [SerializeField] private float fadeSpeed = 1f;
        private SpriteRenderer _spriteRenderer;
        private float _targetAlpha = 1f;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            StartCoroutine(CustomAnimation.Blinking(_spriteRenderer, fadeSpeed));
        }

        private void Update()
        {
        }
    }
}