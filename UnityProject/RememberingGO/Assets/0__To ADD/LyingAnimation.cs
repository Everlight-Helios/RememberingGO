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
        if (GameObject.Find("CameraManager").GetComponent<CameraManager>().liggen)
        {
            StartCoroutine(TurnUp());
        }	
        if (run)
        {
            currentAngle = new Vector3(
            Mathf.LerpAngle(currentAngle.x, targetAngle.x, (0.25f* Time.deltaTime)),0,0);

            transform.eulerAngles = currentAngle;
        }
	}
    private IEnumerator TurnUp() {
        yield return new WaitForSeconds(0.5f);
        run = true;
    }
}
