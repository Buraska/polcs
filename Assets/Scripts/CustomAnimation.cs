using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class CustomAnimation
{
    public static IEnumerator FadeImage(Image img, bool fadeIn, int speed = 8)

    {
        Debug.Log(img.isActiveAndEnabled);
        // fade from opaque to transparent
        var col = img.color;

        if (fadeIn)
        {
            if (speed == 0)
            {
                col.a = 0;
                img.color = col;
                yield break;
            }
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime * speed)
            {
                col.a = i;
                img.color = col;
                yield return null;
            }

            col.a = 0;
            img.color = col;
        }
        // fade from transparent to opaque
        else
        {
            if (speed == 0)
            {
                col.a = 1;
                img.color = col;
                yield break;
            }
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime * speed)
            {
                col.a = i;
                img.color = col;
                yield return null;
            }

            col.a = 1;
            img.color = col;
        }
    }

    public static IEnumerator FadeImage(SpriteRenderer img, bool fadeIn, int speed = 8)
        // Coef 0 - fades immediately
    {
        var col = img.color;


        if (fadeIn)
        {
            if (speed == 0)
            {
                col.a = 0;
                img.color = col;
                yield break;
            }

            for (float i = 1; i >= 0; i -= Time.deltaTime * speed)
            {
                col.a = i;
                img.color = col;
                yield return null;
            }
        }
        else
        {
            if (speed == 0)
            {
                col.a = 1;
                img.color = col;
                yield break;
            }

            for (float i = 0; i <= 1; i += Time.deltaTime * speed)
            {
                col.a = i;
                img.color = col;
                yield return null;
            }
        }
    }

    public static IEnumerator Blinking(SpriteRenderer sprite, float fadeSpeed = 1f, float minTarget = 0.5f)
    {
        var _targetAlpha = 1f;
        while (true)
        {
            var color = sprite.color;
            color.a = Mathf.MoveTowards(color.a, _targetAlpha, fadeSpeed * Time.deltaTime);
            sprite.color = color;
            if (color.a == _targetAlpha)
            {
                if (_targetAlpha == 1f)
                    _targetAlpha = minTarget;
                else
                    _targetAlpha = 1f;
            }

            yield return null;
        }

        yield break;
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
        var startRotation = transform.rotation;
        var endRotation = startRotation * Quaternion.Euler(0, angle, 0);

        var elapsed = 0f;

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
    }
}