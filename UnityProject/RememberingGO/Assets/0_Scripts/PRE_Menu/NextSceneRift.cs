using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneRift : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(OVRInput.GetDown(OVRInput.Button.One)){
			GameObject.Find("CameraManager").GetComponent<Loadlevel>().gogo = true;
		}
	}
}
