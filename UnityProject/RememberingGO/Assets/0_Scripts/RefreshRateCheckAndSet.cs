using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshRateCheckAndSet : MonoBehaviour {

	public bool FFROn = true;
	public bool HzOn = true;
	float[] freqs;

	// Use this for initialization
	void Start () {
		freqs = OVRManager.display.displayFrequenciesAvailable;
		if(FFROn){
			OVRManager.tiledMultiResLevel = OVRManager.TiledMultiResLevel.LMSHigh;
		}
		if(HzOn){
			OVRManager.display.displayFrequency = 72.0f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
