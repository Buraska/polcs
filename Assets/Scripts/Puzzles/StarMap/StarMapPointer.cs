using System;
using System.Linq;
using UnityEngine;

namespace Puzzles.StarMap
{
    public class StarMapPointer : MonoBehaviour
    {
        [SerializeField] private Arrow _x;
        [SerializeField] private Arrow _y;

        [SerializeField] private GameObject XLocs;
        [SerializeField] private GameObject YLocs;

        private float[] _xLocs ;
        private float[] _yLocs; // 3, 1.92, 

        private void Start()
        {
            _xLocs = XLocs.GetComponentsInChildren<Transform>().Skip(1).Select(x => x.transform.localPosition.x).ToArray();
            _yLocs = YLocs.GetComponentsInChildren<Transform>().Skip(1).Select(y => y.transform.localPosition.y).ToArray();
            Debug.Log($"X positions are {string.Join(", ",_xLocs)}");
        }

        private void Update()
        {
            transform.localPosition = new Vector3(_xLocs[_x.CurrentPositionInd], _yLocs[_y.CurrentPositionInd]);
        }
    }
}