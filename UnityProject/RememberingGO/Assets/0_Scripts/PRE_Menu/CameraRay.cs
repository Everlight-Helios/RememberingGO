using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRay : MonoBehaviour {

	private Camera cam;

	// Use this for initialization
	void Start () {
		cam = this.gameObject.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
		if (Physics.Raycast(ray, out hit)) {
            Transform objectHit = hit.transform;
            
			if(objectHit.GetComponent<HitDetection>()){
				objectHit.GetComponent<HitDetection>().hit = true;
			}
			if(objectHit.GetComponent<HitDetectionStand>()){
				objectHit.GetComponent<HitDetectionStand>().hit = true;
			}

            // Do something with the object that was hit by the raycast.
        }
	}
}
