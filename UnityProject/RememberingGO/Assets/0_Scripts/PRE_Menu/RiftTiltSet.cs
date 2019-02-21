using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftTiltSet : MonoBehaviour {

	public float tilt = 0.0f;
	public float pan = 0.0f;
	public float tiltPerPress = 10.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(OVRInput.GetDown(OVRInput.Button.Up)){
			tilt += tiltPerPress;
		}
		if(OVRInput.GetDown(OVRInput.Button.Down)){
			tilt -= tiltPerPress;
		}
		if(OVRInput.GetDown(OVRInput.Button.Right)){
			pan -= tiltPerPress;
		}
		if(OVRInput.GetDown(OVRInput.Button.Left)){
			pan += tiltPerPress;
		}
	}
}
