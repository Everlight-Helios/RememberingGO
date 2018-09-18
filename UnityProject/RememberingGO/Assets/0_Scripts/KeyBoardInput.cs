using UnityEngine;

public class KeyBoardInput : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown(KeyCode.Escape))
        {

            Application.Quit();

        }

        if (Input.GetKeyDown(KeyCode.R))
        {

            UnityEngine.SceneManagement.SceneManager.LoadScene(0);

        }

        if (Input.GetKeyDown(KeyCode.F12))
        {

            UnityEngine.XR.InputTracking.Recenter();

        }

    }

}
