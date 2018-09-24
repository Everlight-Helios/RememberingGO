using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public bool liggen = false;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
        if (liggen) {
            //StartCoroutine(PrintToConsole());
        }
	}

    private IEnumerator PrintToConsole() {

        //Debug.Log("Liggen = activated");
        StopCoroutine("PrintToConsole");
        yield return null;

    }
}
