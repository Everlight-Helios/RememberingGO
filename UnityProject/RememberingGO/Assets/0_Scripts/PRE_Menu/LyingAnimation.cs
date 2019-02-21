using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LyingAnimation : MonoBehaviour {

    public static bool run = false;
    Vector3 targetAngle = new Vector3(65f, 0f, 0f);
    private Vector3 currentAngle;
    // Use this for initialization
    void Start () {
        currentAngle = transform.eulerAngles;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameObject.Find("CameraManager").GetComponent<RiftTiltSet>())
        {
            targetAngle = new Vector3(GameObject.Find("CameraManager").GetComponent<RiftTiltSet>().tilt,GameObject.Find("CameraManager").GetComponent<RiftTiltSet>().pan,0);
            currentAngle = new Vector3(
            Mathf.LerpAngle(currentAngle.x, targetAngle.x, (0.25f* Time.deltaTime)),Mathf.LerpAngle(currentAngle.y, targetAngle.y, (0.25f* Time.deltaTime)),0);

            transform.eulerAngles = currentAngle;
        }
		if (GameObject.Find("CameraManager").GetComponent<GOTiltSet>())
        {
            targetAngle = new Vector3(GameObject.Find("CameraManager").GetComponent<GOTiltSet>().tilt,GameObject.Find("CameraManager").GetComponent<GOTiltSet>().pan,0);
            currentAngle = new Vector3(
            Mathf.LerpAngle(currentAngle.x, targetAngle.x, (0.25f* Time.deltaTime)),Mathf.LerpAngle(currentAngle.y, targetAngle.y, (0.25f* Time.deltaTime)),0);

            transform.eulerAngles = currentAngle;
        }
	}
    
}
