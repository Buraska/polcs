using UnityEngine;

namespace Puzzles.Nightmare
{
    public class NightmareController: MonoBehaviour
    {
        private int zoomStep = 0;

        public int ZoomIn()
        {
            zoomStep++;
            return zoomStep;
        }
        
        public int ZoomOut()
        {
            zoomStep++;
            return zoomStep;
        }
    }
}