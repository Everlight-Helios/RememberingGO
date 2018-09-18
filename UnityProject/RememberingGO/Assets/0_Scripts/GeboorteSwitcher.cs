using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//GeboorteSwitcher has an EditorClass, it's located in Editor/geboorte, it handles all the arrays in the inspector

public class GeboorteSwitcher : MonoBehaviour
{

    public ParticleSystem[] particleSystems;
    public Color[] skyColor, groundColor;
    public float[] timings, emissions;
    public int[] fases;
    public int amountOfSpots;

    public int setFase, currentFase;

    private bool counting = false;
    public bool autoContinue = true;

    public delegate void FinalFase();
    public event FinalFase OnFinalFase;

    [SerializeField]
    private ColorManager colorManager;

    [SerializeField]
    private float fadeTime = 5.0f;

    private ParticleSystem.EmissionModule emitter;

    //Start on play
    void Start()
    {
		fadeTime = 5.0f;
        Init();
        colorManager = GameObject.Find("ColorManager").GetComponent<ColorManager>();
        
        GetComponent<ParentWombTunnel>().Activation += StartEmitting;

    }

    void StartEmitting()
    {

        StartCoroutine(WaitForNextFase(3));
       
    }

    //Disable emission for all particle systems
    public void Init()
    {

        foreach (ParticleSystem system in particleSystems)
        {

            emitter = system.emission;
            emitter.rateOverTime = 0;

            if (system.gameObject.name == "Geboorte01")
            {

                emitter.rateOverTime = 1;

            }

        }

    }

    //Waiting to enable the next fase
    private IEnumerator WaitForNextFase(float waitTime)
    {

        if (!counting)
        {

            counting = true;

            yield return new WaitForSeconds(waitTime);

            counting = false;

            NextFase();

        }

    }

    // Sets next fase based on currentfase
    void NextFase()
    {

        currentFase++;
        SetFase(currentFase);

        if(currentFase == fases.Length)
        {

            OnFinalFase();
            colorManager.FadeNewColor(Color.white, Color.white, RenderSettings.fogColor, RenderSettings.fogDensity);

            StartCoroutine(LoadNewLevelAsync(1));

        }

    }

    //This method checks the fase array, if any entry in the index matches the desired fase, it fades the colors and turns the particleSystem on. 
    //Otherwise the emission gets set to 0.
    public void SetFase(int fase)
    {

        for (int i = 0; i < fases.Length; i++)
        {

            if (fases[i] == fase)
            {

                colorManager.SetTransitions(fadeTime);
                colorManager.FadeNewColor(skyColor[i], groundColor[i], RenderSettings.fogColor, RenderSettings.fogDensity);

                
                emitter = particleSystems[i].emission;
                emitter.enabled = true;
                emitter.rateOverTime = emissions[i];
                

                if (autoContinue)
                {
                    StartCoroutine(WaitForNextFase(timings[i]));
                }
                


            }
            else if (fases[i] != fase)
            {

                emitter = particleSystems[i].emission;
                emitter.enabled = false;
                emitter.rateOverTime = 0;

            }

        }

    }

    public void SetColour(int index)
    {

 
        RenderSettings.skybox.SetColor("_SkyColor", skyColor[index]);
        RenderSettings.skybox.SetColor("_HorizonColor", groundColor[index]);
       

    }

    // This method re-creates the arrays for the custom inspector view
    public void CreateArrays(int p_amountOfSpots)
    {

        fases = new int[p_amountOfSpots];
        particleSystems = new ParticleSystem[p_amountOfSpots];
        skyColor = new Color[p_amountOfSpots];
        groundColor = new Color[p_amountOfSpots];
        timings = new float[p_amountOfSpots];
        emissions = new float[p_amountOfSpots];

    }

    private IEnumerator LoadNewLevelAsync(int sceneNumber)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneNumber);
        async.allowSceneActivation = false;
        Debug.Log("Hospital scene was loaded!");
        yield return new WaitForSeconds(45);
        
        Debug.Log("Start Scene Switch");
        colorManager.FadeInOverlay();
        yield return new WaitForSeconds(5);


        async.allowSceneActivation = true;
        yield return async;

        Debug.Log("Loading complete");

    }

}
