using System.Collections;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;


    public static class CustomAnimation
    {
            public static IEnumerator FadeImage(Image img, bool fadeIn, int coef = 8)
        
            {
                Debug.Log(img.isActiveAndEnabled);
                // fade from opaque to transparent
                var col = img.color;
        
                if (fadeIn)
                {
                    // loop over 1 second backwards
                    for (float i = 1; i >= 0; i -= Time.deltaTime * coef)
                    {
                        col.a = i;
                        img.color = col;
                        yield return null;
                    }
                }
                // fade from transparent to opaque
                else
                {
                    // loop over 1 second
                    for (float i = 0; i <= 1; i += Time.deltaTime * coef)
                    {
                        col.a = i;
                        img.color = col;
                        yield return null;
                    }
                }
            }
        
            public static IEnumerator FadeImage(SpriteRenderer img, bool fadeIn, int coef = 8)
        
            {
                var col = img.color;
        
                if (fadeIn)
                {
                    for (float i = 1; i >= 0; i -= Time.deltaTime * coef)
                    {
                        col.a = i;
                        img.color = col;
                        yield return null;
                    }
                }
                else
                {
                    for (float i = 0; i <= 1; i += Time.deltaTime * coef)
                    {
                        col.a = i;
                        img.color = col;
                        yield return null;
                    }
                }
            }
            
            public static IEnumerator Fade(TextMeshProUGUI obj, bool fadeIn, int coef = 8)
        
            {
                var col = obj.color;
        
                if (fadeIn)
                {
                    for (float i = 1; i >= 0; i -= Time.deltaTime * coef)
                    {
                        col.a = i;
                        obj.color = col;
                        yield return null;
                    }
                    col.a = 0;
                }
                else
                {
                    for (float i = 0; i <= 1; i += Time.deltaTime * coef)
                    {
                        col.a = i;
                        obj.color = col;
                        yield return null;
                    }
                    col.a = 1;

                }
                obj.color = col;
            }
            
            public static IEnumerator RotateOverTime(Transform transform, float angle, float duration)
            {

                Quaternion startRotation = transform.rotation;
                Quaternion endRotation = startRotation * Quaternion.Euler(0, angle, 0);

                float elapsed = 0f;

                while (elapsed < duration)
                {
                    transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed / duration);
                    elapsed += Time.deltaTime;
                    yield return null;
                }

                transform.rotation = endRotation;
            }
        
    }
