using UnityEngine;
using System.Collections;

public class HS_AudioParticle : MonoBehaviour {


    private AudioSource audioSource;
    private ParticleSystem partSyst;

    private float[] spectrum = new float[64];

    private ParticleSystem.EmissionModule emissonModule;

    [SerializeField]
    private bool audioEmission = true;

    private ParticleSystem pSystem
    {

        get
        {

            ParticleSystem system = null;
            foreach (Transform child in transform)
            {

                system = child.GetComponent<ParticleSystem>();

            }

            return system;

        }

    }

    private float RmsValue;
    private float DbValue;
    private float PitchValue;

    private const int QSamples = 1024;
    private const float RefValue = 0.1f;
    private const float Threshold = 0.02f;

    private float[] _samples;
    private float[] _spectrum;
    private float _fSample;

    [SerializeField]
    [Range(1, 10)]
    private float multiply = 5;

    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;

        partSyst = pSystem;
        emissonModule = partSyst.emission;

        _samples = new float[QSamples];
        _spectrum = new float[QSamples];
        _fSample = AudioSettings.outputSampleRate;


    }

    void Update()
    {

        if (audioSource.isPlaying)
        {

            AnalyzeSound();
            emissonModule.rateOverTime = PitchValue / multiply;

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

        RmsValue = Mathf.Sqrt(sum / QSamples); // rms = square root of average
        DbValue = 20 * Mathf.Log10(RmsValue / RefValue); // calculate dB

        if (DbValue < -160) DbValue = -160; // clamp it to -160dB min

        // get sound spectrum
        GetComponent<AudioSource>().GetSpectrumData(_spectrum, 0, FFTWindow.BlackmanHarris);

        float maxV = 0;
        var maxN = 0;

        for (i = 0; i < QSamples; i++)
        {

            // find max 
            if (!(_spectrum[i] > maxV) || !(_spectrum[i] > Threshold))
                continue;

            maxV = _spectrum[i];
            maxN = i; // maxN is the index of max

        }

        float freqN = maxN; // pass the index to a float variable

        if (maxN > 0 && maxN < QSamples - 1)
        {

            // interpolate index using neighbours
            var dL = _spectrum[maxN - 1] / _spectrum[maxN];
            var dR = _spectrum[maxN + 1] / _spectrum[maxN];
            freqN += 0.5f * (dR * dR - dL * dL);

        }

        PitchValue = freqN * (_fSample / 2) / QSamples; // convert index to frequency

    }
}
