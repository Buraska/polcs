using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class MouseManager: MonoBehaviour
    {
        public static MouseManager Instance;

        public Texture2D normalCursor;
        public Texture2D hoverCursor;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }

        public void SetCursorVisible(bool value)
        {
            Cursor.visible = value;
        }
        private void Update()
        {
            Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorld, Vector2.zero);
            if (hit.collider != null)
            {
                Cursor.SetCursor(hoverCursor, new Vector2(46, 17), CursorMode.Auto);
            }
            else Cursor.SetCursor(normalCursor, new Vector2(46, 17), CursorMode.Auto);

        }

        private static Vector2 GetMiddleOfCursor(Texture2D cursor)
        {
            return new Vector2(cursor.width / 2f, cursor.height / 2f);
        }
    }
}