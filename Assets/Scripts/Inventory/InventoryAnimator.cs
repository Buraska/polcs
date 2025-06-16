using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryAnimator : MonoBehaviour
    {
        public IEnumerator FadeImage(Image image, bool fadeOut, float speed)
        {
            float alpha = fadeOut ? 1 : 0;
            float target = fadeOut ? 0 : 1;

            while (Mathf.Abs(image.color.a - target) > 0.01f)
            {
                alpha = Mathf.MoveTowards(alpha, target, Time.deltaTime * speed);
                var color = image.color;
                color.a = alpha;
                image.color = color;
                yield return null;
            }
        }
    }
}