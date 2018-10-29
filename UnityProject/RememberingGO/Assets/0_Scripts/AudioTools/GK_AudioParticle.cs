using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class GK_AudioParticle : MonoBehaviour {

    [SerializeField]
    private bool audioEmission = true;

    private AudioSource audioSource;

    [SerializeField]
    private ParticleSystem partSyst;

    private ParticleSystem.EmissionModule emissonModule;

    [SerializeField]
    private float RmsValue;

    private const int QSamples = 512;
    private const float RefValue = 0.1f;
    private const float Threshold = 0.02f;

    private float[] _samples;
    private float[] _spectrum;
    private float _fSample;

    [SerializeField]
    [Range(1, 1000)]
    private float multiply = 500;


    // Use this for initialization
    void Start () {

        audioSource = GetComponent<AudioSource>();

        emissonModule = partSyst.emission;

        _samples = new float[QSamples];

        GameObject.Find("GeboorteSwitcher").GetComponent<GeboorteSwitcher>().OnFinalFase += DisableMe;

    }
	
    void DisableMe()
    {

        emissonModule = partSyst.emission;
        emissonModule.rateOverTime = 0;

        this.enabled = false;

    }

    void OnDisable()
    {

        //GameObject.Find("GeboorteSwitcher").GetComponent<GeboorteSwitcher>().OnFinalFase += DisableMe;

    }

	// Update is called once per frame
	void Update () {

        if (audioSource.isPlaying && audioEmission)
        {

            AnalyzeSound();
            emissonModule.rateOverTime = RmsValue;

        }

    }

    void AnalyzeSound()
    {

        // fill array with samples
        audioSource.GetOutputData(_samples, 0);

        int i;
        float sum = 0;

        for (i = 0; i < QSamples; i++)
        {

            // sum squared samples
            sum += _samples[i] * _samples[i];

        }

        RmsValue = Mathf.Sqrt(sum / QSamples) * multiply; // rms = square root of average
        

    }
}


