using System;
using UnityEngine;

namespace SceneSystem
{
    public class Background : MonoBehaviour
    {
        private SpriteRenderer _background;

        private void Awake()
        {
            _background = GetComponent<SpriteRenderer>();

            CreateSide("LeftSide", new Vector3(_background.bounds.size.x, 0));
            CreateSide("RightSide", new Vector3(0 - _background.bounds.size.x, 0));
        }

        private void CreateSide(string name, Vector3 location)
        {
            var side = new GameObject(name);
            side.transform.SetParent(this.transform);
            var spriteRenderer = side.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = _background.sprite;
            spriteRenderer.flipX = true;
            spriteRenderer.color = new Color(0.10f, 0.10f, 0.10f, 1f);
            side.transform.position = location;
        }
    }
}