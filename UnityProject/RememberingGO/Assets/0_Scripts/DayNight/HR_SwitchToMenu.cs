using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HR_SwitchToMenu : MonoBehaviour {

    private ColorManager colorManager;
    private AsyncOperation async;

    [SerializeField]
    private float waitTimeInSeconds;

    [SerializeField]
    private float time;
    private bool Switching = false;

    void Start ()
    {

        colorManager = GameObject.Find("ColorManager").GetComponent<ColorManager>();

        StartCoroutine(LoadNewLevelAsync(0));

    }

	void Update () {

        time += Time.deltaTime;

        if(time > waitTimeInSeconds && !Switching)
        {

            StartCoroutine(GoToLoadedScene());
            Switching = true;

        }
	
	}

    private IEnumerator LoadNewLevelAsync(int sceneNumber)
    {

        async = SceneManager.LoadSceneAsync(sceneNumber);
        async.allowSceneActivation = false;
        yield return new WaitForEndOfFrame();
        Debug.Log("Main Scene is loaded!");

    }

    private IEnumerator GoToLoadedScene()
    {

        Debug.Log("Start Scene Switch");
        colorManager.FadeInOverlay();
        yield return new WaitForSeconds(5);


        async.allowSceneActivation = true;
        yield return async;

        Debug.Log("Loading complete");

    }
}
