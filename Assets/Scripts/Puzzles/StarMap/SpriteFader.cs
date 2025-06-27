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
        }

        private void Update()
        {
            Color color = _spriteRenderer.color;
            color.a = Mathf.MoveTowards(color.a, _targetAlpha, fadeSpeed * Time.deltaTime);
            _spriteRenderer.color = color;
            if (color.a == _targetAlpha)
            {
                if (_targetAlpha == 1f)
                {
                    _targetAlpha = 0.5f;
                }
                else
                {
                    _targetAlpha = 1f;
                }
            }
        }
    }

}