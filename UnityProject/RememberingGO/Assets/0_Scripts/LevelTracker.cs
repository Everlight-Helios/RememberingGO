using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class LevelTracker : MonoBehaviour {

    [SerializeField]
    private int currentScene = 0;


    private ColorManager colorManager;

    void Start()
    {

        colorManager = GameObject.Find("ColorManager").GetComponent<ColorManager>();
        DontDestroyOnLoad(this.gameObject);
        

    }

	void Update () {

        if(colorManager == null)
        {

            colorManager = GameObject.Find("ColorManager").GetComponent<ColorManager>();

        }

        switch (currentScene)
        {

            case 0:
                if (Input.GetKeyDown(KeyCode.Space))
                {

                    StartCoroutine(LoadNewLevelAsync(1));
                    currentScene = 1;

                    

                }
                break;

            case 1:
                if (Input.GetKeyDown(KeyCode.Space))
                {

                    StartCoroutine(LoadNewLevelAsync(2));
                    currentScene = 2;

                }
                break;

            case 2:
                if (Input.GetKeyDown(KeyCode.Space))
                {

                    StartCoroutine(LoadNewLevelAsync(3));
                    currentScene = 3;

                }
                break;

            case 3:
                if (Input.GetKeyDown(KeyCode.Space))
                {

                    StartCoroutine(LoadNewLevelAsync(1));
                    currentScene = 1;

                }
                break;
        }
	
	}

    public void LoadNewScene(string sceneName)
    {

        SceneManager.LoadScene(sceneName);

    }

    public void LoadNewScene(int sceneNumber)
    {

        SceneManager.LoadScene(sceneNumber);

    }

    private IEnumerator LoadNewLevelAsync(int sceneNumber)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneNumber);
        async.allowSceneActivation = false;

        Debug.Log("Start Scene Switch");
        colorManager.FadeInOverlay();
        yield return new WaitForSeconds(5);


        async.allowSceneActivation = true;
        yield return async;

        Debug.Log("Loading complete");


    }

}
