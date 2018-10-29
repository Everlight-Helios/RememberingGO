using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(AudioSource))]
public class BoomBeestVisualizer : MonoBehaviour {

    [SerializeField]
    private Transform boomBeest;

    private AudioSource audioSource;
    private AudioClip audioClip;
    private float clipLength;

    private SphereCollider col;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float total;

    [SerializeField]
    [Range(0, 7)]
    private int audioClipToUse;

    private bool once;

    public float[] spectrum = new float[64];
    Vector3 currentVelocity;

    [SerializeField]
    private float time = 0;

    void Start () {

        audioSource = GetComponent<AudioSource>();
        audioClip = audioSource.clip;

        clipLength = audioClip.length;
        time = clipLength;

		if(boomBeest == null){
			boomBeest = transform.Find("Icosphere_001");
		}
	
	}

	void Update () {

        if (true)
        {
            total = 0;

            audioSource.GetSpectrumData(this.spectrum, 0, FFTWindow.Hamming);

            foreach (float spacing in spectrum)
            {

                total += spacing;

            }

            boomBeest.localPosition = Vector3.SmoothDamp(boomBeest.localPosition, new Vector3(Mathf.Sin(Time.time * 2) * total * 100, -total * 100, Mathf.Cos(Time.time * 2) * total * 100), ref currentVelocity, 0.25f);
			



        }

        if(time < clipLength && once)
        {

            time += Time.deltaTime;

            if (total <= 0)
            {

                once = false;

            }

        }
        else
        {

            once = false;

        }
       

    }

    public void HitByRay()
    {

        if (!once)
        {
            once = true;
            audioSource.Play();
            time = 0;

        }

    }

}
