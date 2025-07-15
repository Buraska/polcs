using UnityEngine;
using System.Collections;

namespace Animation
{


    public class CameraScreamer : MonoBehaviour
    {
        public Vector3 zoomTargetPosition; // куда приблизиться
        private Vector3 originalPosition;
        public float zoomDuration = 0.5f; // плавное приближение за 0.5 сек
        public float holdDuration = 2f;   // подержать 2 секунды

        private bool isZooming = false;

        void Start()
        {
            originalPosition = transform.position;
        }

        public void TriggerScreamer()
        {
            if (!isZooming)
                StartCoroutine(ZoomInAndSnapBack());
        }

        IEnumerator ZoomInAndSnapBack()
        {
            isZooming = true;

            // Плавное приближение
            float elapsed = 0f;
            while (elapsed < zoomDuration)
            {
                transform.position = Vector3.Lerp(originalPosition, zoomTargetPosition, elapsed / zoomDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = zoomTargetPosition;

            // Держим 2 секунды
            yield return new WaitForSeconds(holdDuration);

            // Мгновенный возврат
            transform.position = originalPosition;

            isZooming = false;
        }
    }

}