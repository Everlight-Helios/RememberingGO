using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(AudioSource))]

public class StartParticleEmission : MonoBehaviour {

    private ParticleSystem pSystem;
    private SphereCollider col;

    private float[] spectrum = new float[64];

    private AudioSource aSource;

    [SerializeField]
    [Range(1, 100)]
    private int magnitude = 1;

    private AudioSource SetAudioSource
    {

        get
        {
            return GetComponent<AudioSource>();
        }

    }

    private ParticleSystem SetParticleSystem
    {

        get
        {

            return transform.Find("ShockWave").GetComponent<ParticleSystem>();

        }



    }

    private SphereCollider SetCollider
    {

        get
        {

            return GetComponent<Collider>() as SphereCollider;

        }


    }

	void Start () {

        col = SetCollider;
        pSystem = SetParticleSystem;
        aSource = SetAudioSource;
	
	}

	void Update () {

        //aSource.ignoreListenerVolume = true;
        AudioListener.GetSpectrumData(this.spectrum, 0, FFTWindow.Hamming);

        float total = 0;
        
        
        foreach (float value in spectrum)
        {

            total += value;

        }

        ParticleSystem.EmissionModule emissionModule = pSystem.emission;
        emissionModule.rateOverTime = (total * magnitude);

    }
}
