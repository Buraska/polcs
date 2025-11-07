using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Puzzles.Maze
{
    public class Goal: MonoBehaviour
    {
        public int GoalX, GoalY;
        public int GateX = 0;
        public int GateY = 0;
        public bool isVertical = false;
        [FormerlySerializedAs("onTake")] public GameEvent.GameEvent onTakeGameEvent;

        public Maze maze;
        
        void Start()
        {
            maze.Init();
            GateY = maze.Height;
            GateX = maze.Width / 2;
            isVertical = false;


            if (GoalX < 0) GoalX += maze.Width;
            if (GoalY < 0) GoalY += maze.Height;
            transform.position = new Vector3((GoalX + 0.5f) * maze.size + maze.posX, (GoalY + 0.5f) * maze.size + maze.posY);
            maze.HighlightHWall(GateX, GateY, transform.GetComponent<SpriteRenderer>().color, isVertical);
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, maze.Player.position) < 0.1f)
            {
                GameManager.Instance.StartCoroutine(GameManager.Instance.EventManager.RunEvent(onTakeGameEvent));
                maze.RemoveHWall(GateX, GateY, isVertical);
                Destroy(gameObject);
            }
        }
    }
}