using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FFR_Option: MonoBehaviour {

	public OVRManager.TiledMultiResLevel FFR_amount;
	public float[] freqs = new float[2];

	// Use this for initialization
	void Start () {
		OVRManager.tiledMultiResLevel = FFR_amount;
		freqs = OVRManager.display.displayFrequenciesAvailable;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
