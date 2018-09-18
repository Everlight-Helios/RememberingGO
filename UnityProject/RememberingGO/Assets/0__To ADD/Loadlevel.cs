using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loadlevel : MonoBehaviour {

    public Image fade;
    private AsyncOperation async;
    public bool gogo = false;

    // Use this for initialization
    void Start()
    {
        
        StartCoroutine(LoadNewLevelAsync(3));
        
    }

    void Update()
    {
        if (gogo)
        {
            //float curTime = Time.time;
            //float alpha = Map(curTime, 0f, 5f, 0f, 1f);
            Image[] ts = this.transform.GetComponentsInChildren<Image>(true);
            if (ts.Length != 0)
            {
                fade = ts[0];
            }
            var tempColor = fade.color;
            tempColor.a += (0.2f *Time.deltaTime);
           
            fade.color = tempColor;
            
            StartCoroutine(GoToLoadedScene());
        }
    }

    private IEnumerator LoadNewLevelAsync(int sceneNumber)
    {
        async = SceneManager.LoadSceneAsync(sceneNumber);
        async.allowSceneActivation = false;
        yield return new WaitForEndOfFrame();
        Debug.Log("New scene loaded!"); 
    }

  

    private IEnumerator GoToLoadedScene()
    {

        
        yield return new WaitForSeconds(5);


        async.allowSceneActivation = true;
        yield return async;

        Debug.Log("Loading complete");

    }
}
