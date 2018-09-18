using UnityEngine;
using System.Collections;

public class EndCredits : MonoBehaviour {

    [SerializeField]
    private TextMesh[] fadeItems;

    [SerializeField]
    private float timeTheItemFades, timeToShowItem, initialWaitTime;

    private float numberOfFadeItems;

    private Color spriteColor;

    private int currentItem;
    private bool fadingIn, fadingOut;

    void Start()
    {

        currentItem = 0;
        NextItem(initialWaitTime);

    }

    void NextItem(float waitTime)
    {

        StartCoroutine(FadeInCredits(fadeItems[currentItem], timeTheItemFades, waitTime));
        

    }

    private IEnumerator FadeOutCredits(TextMesh item, float fadeTime, float waitTime)
    {

        yield return new WaitForSeconds(waitTime);

        float opacity = 0.5f;
        float time = 0f;
        fadeTime *= 10f;
        spriteColor = item.color;
        spriteColor.a = opacity;

        while (opacity > 0)
        {

            time += Time.deltaTime;
            opacity -= (time / fadeTime);
            //Debug.Log(opacity);
            spriteColor.a = opacity;
            item.color = spriteColor;

            yield return new WaitForEndOfFrame();

        }

        spriteColor.a = 0;
        item.color = spriteColor;

        NextItem(0);
        

    }

    private IEnumerator FadeInCredits(TextMesh item, float fadeTime, float waitTime)
    {

        yield return new WaitForSeconds(waitTime);

        float opacity = 0f;
        float time = 0f;
        fadeTime *= 10f;
        spriteColor = item.color;
        spriteColor.a = opacity;

        while (opacity < 1.0f)
        {

            time += Time.deltaTime;
            opacity += (time / fadeTime);
            spriteColor.a = opacity;
            item.color = spriteColor;

            yield return new WaitForEndOfFrame();

        }

        spriteColor.a = 1.0f;
        item.color = spriteColor;

        

        if (currentItem < fadeItems.Length)
        {
            StartCoroutine(FadeOutCredits(fadeItems[currentItem], timeTheItemFades, timeToShowItem));
        }

        currentItem++;

    }

}