using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Puzzles.Maze
{
public class Enemy : MonoBehaviour
{
    public Maze maze;
    public float moveInterval = 1.0f;
    public float moveDuration = 0.2f;
    public int X, Y;
    public bool isFlying;

    private float posX, posY;
    private bool _moving;
    private int _prevX;
    private int _prevY;
    public GameEvent.GameEvent OnCatchGameEvent;
    private bool _hasCatched = false;

    void Start()
    {
        posX = maze.transform.position.x;
        posY = maze.transform.position.y;
        transform.position = GetWorldPosition(X, Y);
        StartCoroutine(ChaseLoop());
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W) ||
        //     Input.GetKeyDown(KeyCode.S))
        // {
        //     TryMoveOneStep();
        // }
            
        if (Vector3.Distance(transform.position, maze.Player.position) < 0.1f)
            OnPlayerCaught();
    }

    Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3((x + 0.5f) * maze.size + posX, (y + 0.5f) * maze.size + posY);
    }

    IEnumerator ChaseLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(moveInterval);
            if (!_moving)
                if (isFlying)
                {
                    MoveTowardPlayer();
                }else TryMoveOneStep();
        }
    }
    
    void MoveTowardPlayer()
    {
        int playerX = maze.PlayerX;
        int playerY = maze.PlayerY;

        // Simple greedy move: one step closer on X or Y
        int nextX = X, nextY = Y;

        if (Mathf.Abs(playerX - X) > Mathf.Abs(playerY - Y))
            nextX += (playerX > X ? 1 : -1);
        else if (playerY != Y)
            nextY += (playerY > Y ? 1 : -1);
        
        _moving = true;
        X = nextX;
        Y = nextY;
        transform.DOMove(GetWorldPosition(X, Y), moveDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                _moving = false;
                if (X == playerX && Y == playerY)
                    OnPlayerCaught();
            });
    }

    void MoveToNextNode()
    {
        _moving = true;
        Vector3 target = new Vector3((X + 0.5f) * maze.size + posX, (Y + 0.5f) * maze.size + posY);

        this.transform.DOMove(target, moveDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                if (TryGetNextDirection(out int nextX, out int nextY))
                {
                    _prevX = X;
                    _prevY = Y;
                    X = nextX;
                    Y = nextY;
                    MoveToNextNode(); 
                }
                else
                {
                    _moving = false;
                }
            });
    }
    
    bool TryGetNextDirection(out int nextX, out int nextY)
    {
        nextX = X;
        nextY = Y;

        bool canLeft = !maze.HWalls[X, Y] && _prevX != X - 1;
        bool canRight = !maze.HWalls[X + 1, Y] && _prevX != X + 1;
        bool canUp = !maze.VWalls[X, Y + 1] && _prevY != Y + 1;
        bool canDown = !maze.VWalls[X, Y] && _prevY != Y - 1;

        int dirs = (canLeft ? 1 : 0) + (canRight ? 1 : 0) + (canUp ? 1 : 0) + (canDown ? 1 : 0);

        if (dirs != 1)
            return false;

        if (canLeft) nextX--;
        else if (canRight) nextX++;
        else if (canUp) nextY++;
        else if (canDown) nextY--;

        return true;
    }

    void TryMoveOneStep()
    {
        List<Vector2Int> path = FindPath(X, Y, maze.PlayerX, maze.PlayerY);
        if (path == null || path.Count < 2) return;

        Vector2Int next = path[1]; // path[0] = current pos
        _moving = true;
        X = next.x;
        Y = next.y;

        transform.DOMove(GetWorldPosition(X, Y), moveDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                // if (TryGetNextDirection(out int nextX, out int nextY))
                // {
                //     _prevX = X;
                //     _prevY = Y;
                //     X = nextX;
                //     Y = nextY;
                //     MoveToNextNode(); 
                // }
                // else
                {
                    _moving = false;
                }            });
    }

    List<Vector2Int> FindPath(int startX, int startY, int goalX, int goalY)
    {
        int w = maze.Width;
        int h = maze.Height;

        var queue = new Queue<Vector2Int>();
        var prev = new Dictionary<Vector2Int, Vector2Int>();
        var visited = new bool[w, h];

        Vector2Int start = new(startX, startY);
        Vector2Int goal = new(goalX, goalY);

        queue.Enqueue(start);
        visited[startX, startY] = true;

        while (queue.Count > 0)
        {
            var cur = queue.Dequeue();
            if (cur == goal)
                return ReconstructPath(prev, start, goal);

            foreach (var dir in GetNeighbors(cur))
            {
                if (!visited[dir.x, dir.y])
                {
                    visited[dir.x, dir.y] = true;
                    prev[dir] = cur;
                    queue.Enqueue(dir);
                }
            }
        }

        return null; // no path found
    }

    List<Vector2Int> GetNeighbors(Vector2Int node)
    {
        int x = node.x;
        int y = node.y;
        var res = new List<Vector2Int>();

        // left
        if (x > 0 && !maze.HWalls[x, y]) res.Add(new Vector2Int(x - 1, y));
        // right
        if (x < maze.Width - 1 && !maze.HWalls[x + 1, y]) res.Add(new Vector2Int(x + 1, y));
        // down
        if (y > 0 && !maze.VWalls[x, y]) res.Add(new Vector2Int(x, y - 1));
        // up
        if (y < maze.Height - 1 && !maze.VWalls[x, y + 1]) res.Add(new Vector2Int(x, y + 1));

        return res;
    }

    List<Vector2Int> ReconstructPath(Dictionary<Vector2Int, Vector2Int> prev, Vector2Int start, Vector2Int goal)
    {
        var path = new List<Vector2Int>();
        var cur = goal;
        while (cur != start)
        {
            path.Add(cur);
            cur = prev[cur];
        }
        path.Add(start);
        path.Reverse();
        return path;
    }

    void OnPlayerCaught()
    {
        if (!_hasCatched)
        {
            GameManager.Instance.StartCoroutine(GameManager.Instance.EventManager.RunEvent(OnCatchGameEvent));
            _hasCatched = true;
        }
    }
}
}