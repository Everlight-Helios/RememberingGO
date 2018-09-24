using UnityEngine;
using System.Collections;

public class RotatePlayer : MonoBehaviour {
    bool lig = false;

    private void Start()
    {
		if(GameObject.Find("CameraManager")){
			lig = GameObject.Find("CameraManager").GetComponent<CameraManager>().liggen;
			if (lig)
			{
				transform.Rotate(65f, 0, 0, Space.World);
			}
		}
    }

}
