using UnityEngine;

namespace Puzzles.StarMap
{
    public class StarMapPointer : MonoBehaviour
    {
        [SerializeField] private Arrow _x;
        [SerializeField] private Arrow _y;

        private readonly float[] _xLocs = { -4.59f, -3f, -1.45f, 0.2f, 1.86f, 3.45f, 5.07f };
        private readonly float[] _yLocs = { 3f, 1.92f, 1f, -0.14f, -1.11f, -2.14f, -3.15f }; // 3, 1.92, 

        private void Update()
        {
            transform.localPosition = new Vector3(_xLocs[_x.CurrentPositionInd], _yLocs[_y.CurrentPositionInd]);
        }
    }
}