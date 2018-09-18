using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HideCursor : MonoBehaviour {


    [SerializeField]
    private Image cursorImage;

    Color c;

    public void StartFadeOutCursor()
    {

        StartCoroutine(FadeOutCursor());

    }

    public void StartFadeInCursor()
    {

        StartCoroutine(FadeInCursor());

    }

    private IEnumerator FadeOutCursor()
    {

        float opacity = 1;
        c = cursorImage.color;
        c.a = opacity;

        while (opacity > 0)
        {

            opacity -= Time.deltaTime * 0.25f;
            c.a = opacity;
            cursorImage.color = c;

            yield return new WaitForEndOfFrame();

        }

        c.a = 0;
        cursorImage.color = c;

    }

    private IEnumerator FadeInCursor()
    {

        float opacity = 0;
        c = cursorImage.color;
        c.a = opacity;

        while (opacity < 1)
        {

            opacity += Time.deltaTime * 0.25f;
            c.a = opacity;
            cursorImage.color = c;

            yield return new WaitForEndOfFrame();

        }

        c.a = 1;
        cursorImage.color = c;

    }
}
