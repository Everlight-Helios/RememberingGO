using UnityEngine;
using System.Collections;

public class ActivateAudioPointAfterTime : MonoBehaviour {

	void Start () {

        StartCoroutine(ActiveTime());
	
	}

    public IEnumerator ActiveTime()
    {

        yield return new WaitForSeconds(0.25f);

        gameObject.GetComponent<AudioPoint>().enabled = true;

    }

}
