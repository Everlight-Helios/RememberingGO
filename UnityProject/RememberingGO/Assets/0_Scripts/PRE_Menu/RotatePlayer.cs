using UnityEngine;
using System.Collections;

public class RotatePlayer : MonoBehaviour {
    public static bool run = false;
    Vector3 targetAngle = new Vector3(0f, 0f, 0f);
    private Vector3 currentAngle;
	public float lerpSpeed = 1.0f;
    // Use this for initialization
    void Start () {
		transform.eulerAngles = new Vector3(GameObject.Find("CameraManager").GetComponent<ControllerTiltSet>().tilt,GameObject.Find("CameraManager").GetComponent<ControllerTiltSet>().pan,0);
        currentAngle = transform.eulerAngles;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameObject.Find("CameraManager").GetComponent<ControllerTiltSet>())
        {
            targetAngle = new Vector3(GameObject.Find("CameraManager").GetComponent<ControllerTiltSet>().tilt,GameObject.Find("CameraManager").GetComponent<ControllerTiltSet>().pan,0);
            currentAngle = new Vector3(
            Mathf.LerpAngle(currentAngle.x, targetAngle.x, (lerpSpeed* Time.deltaTime)),Mathf.LerpAngle(currentAngle.y, targetAngle.y, (lerpSpeed* Time.deltaTime)),0);

            transform.eulerAngles = currentAngle;
        }
		
	}

}
