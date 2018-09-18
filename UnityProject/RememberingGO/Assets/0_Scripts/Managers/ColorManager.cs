using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using UnityEngine.Audio;

public class ColorManager : MonoBehaviour {

    private Material skyBox;

    private Color startSkyColor, startGroundColor, startFogColor;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float skyTransition, groundTransition, both;

    [SerializeField]
    [Range(0f, 30f)]
    private float fadeTime = 5;

    private bool fading = false;

    [SerializeField]
    private FogMode fogMode;
    private float fogDensity, referencedSmoothValue;

    [SerializeField]
    private Image overlayImage;

    private float overlayFadeSpeed;
    private Color c;

    [SerializeField]
    private bool ifHospitalScene = false;

    private bool fadingOut = false;

    void Start()
    {

        if (!ifHospitalScene)
        {
            Initialise();
        }

    }

    void Update()
    {

        if (!fadingOut)
        {

            FadeOutOverlay();
            fadingOut = true;

        }


    }

    public void DefaultSettings()
    {

        fadeTime = 5f;
        fogMode = RenderSettings.fogMode;
        fogDensity = RenderSettings.fogDensity;

    }

    void Initialise()
    {

        skyBox = RenderSettings.skybox;
        startSkyColor = skyBox.GetColor("_SkyColor");
        startGroundColor = skyBox.GetColor("_HorizonColor");
        startFogColor = RenderSettings.fogColor;
        RenderSettings.fogMode = fogMode;
        both = 0f;

    }

    public Color[] Colors()
    {

        Initialise();

        Color[] colors = new Color[3];

        colors[0] = startSkyColor;
        colors[1] = startGroundColor;
        colors[2] = startFogColor;

        return colors;

    }

    public void FadeNewColor(Color newSkyColor, Color newGroundColor, Color newFogColor, float newFogDensity)
    {
   
        StartCoroutine(Fading(newSkyColor, newGroundColor, newFogColor, newFogDensity));

    }

    private IEnumerator Fading(Color newSkyColor, Color newGroundColor, Color newFogColor, float newFogDensity)
    {


        float currentTime = 0;
        float currentFogDensity = RenderSettings.fogDensity;

        if (!fading)
        {

            fading = true;

            while (both < 1f)
            {

                currentTime += Time.deltaTime;
                both = currentTime / fadeTime;

                float tempFogDensity = Mathf.Lerp(currentFogDensity, newFogDensity, both);

                skyTransition = both;
                groundTransition = both;
                
                Color tempSkyColor = Color.Lerp(startSkyColor, newSkyColor, skyTransition);
                Color tempGroundColor = Color.Lerp(startGroundColor, newGroundColor, groundTransition);
                Color tempFogColor = Color.Lerp(startFogColor, newFogColor, both);

                skyBox.SetColor("_SkyColor", tempSkyColor);
                skyBox.SetColor("_HorizonColor", tempGroundColor);
                RenderSettings.fogColor = tempFogColor;
                RenderSettings.fogDensity = tempFogDensity;

                yield return new WaitForEndOfFrame();

                if (!fading) { break; }

            }

            fading = false;
            Initialise();
            
        }

    }

    public void SetTransitions(float newFadeSpeed)
    {

        fadeTime = newFadeSpeed;

    }

    public void StopFading()
    {

        fading = false;

    }

    public void FadeInOverlay()
    {

        StartCoroutine(FadeInTheOverlay());

    }

    public void FadeOutOverlay()
    {

        StartCoroutine(FadeOutTheOverlay());

    }

    private IEnumerator FadeOutTheOverlay()
    {


        float opacity = 1;
        c = overlayImage.color;
        c.a = opacity;

        while (opacity > 0)
        {

            opacity -= Time.deltaTime * 0.25f;
            c.a = opacity;
            overlayImage.color = c;

            yield return new WaitForEndOfFrame();

        }

        c.a = 0;
        overlayImage.color = c;


    }

    private IEnumerator FadeInTheOverlay()
    {



        float opacity = 0;
        c = overlayImage.color;
        c.a = opacity;

        while(opacity < 1)
        {

            opacity += Time.deltaTime * 0.25f;
            c.a = opacity;
            overlayImage.color = c;

            yield return new WaitForEndOfFrame();

        }

        c.a = 1;
        overlayImage.color = c;

    }

}
