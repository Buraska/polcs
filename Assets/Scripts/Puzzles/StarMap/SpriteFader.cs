namespace Puzzles.StarMap
{
    using UnityEngine;

    public class SpriteFader : MonoBehaviour
    {
        [SerializeField] private float fadeSpeed = 1f; 
        private SpriteRenderer _spriteRenderer;
        private float _targetAlpha = 1f;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            StartCoroutine(CustomAnimation.Blinking(_spriteRenderer));
        }

        private void Update()
        {
        }
    }

}