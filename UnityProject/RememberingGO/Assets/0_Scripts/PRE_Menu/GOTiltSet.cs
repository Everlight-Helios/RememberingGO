using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOTiltSet : MonoBehaviour {

	public float tilt = 0.0f;
	public float pan = 0.0f;
	public float tiltPerPress = 10.0f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 touchposition = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
		if(touchposition.y > 0.5f && touchposition.x >-.5f && touchposition.x < 0.5f && OVRInput.GetDown(OVRInput.Button.One)){
			tilt += tiltPerPress;
		}
		if(touchposition.y < -0.5f && touchposition.x >-.5f && touchposition.x < 0.5f && OVRInput.GetDown(OVRInput.Button.One)){
			tilt -= tiltPerPress;
		}
		if(touchposition.x > 0.5f && touchposition.y >-.5f && touchposition.y < 0.5f && OVRInput.GetDown(OVRInput.Button.One)){
			pan -= tiltPerPress;
		}
		if(touchposition.x > 0.5f && touchposition.y >-.5f && touchposition.y < 0.5f && OVRInput.GetDown(OVRInput.Button.One)){
			pan += tiltPerPress;
		}



	}
}
