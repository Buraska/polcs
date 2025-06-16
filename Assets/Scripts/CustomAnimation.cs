using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;


    public class CustomAnimation
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
        
    }
