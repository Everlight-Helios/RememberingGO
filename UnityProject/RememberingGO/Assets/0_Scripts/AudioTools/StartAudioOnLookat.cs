using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class StartAudioOnLookat : MonoBehaviour {


    private AudioSource aSource;

	void Start () {

        aSource = GetComponent<AudioSource>();
 	
	}


    void HitByRay()
    {

            StartCoroutine(FadeInAudio());
	
	}

    private IEnumerator FadeInAudio()
    {

        GetComponent<SphereCollider>().enabled = false;

        while (aSource.volume < 1)
        {

            aSource.volume += Time.deltaTime;
            yield return new WaitForEndOfFrame();

        }

        Destroy(this);


    }

}
