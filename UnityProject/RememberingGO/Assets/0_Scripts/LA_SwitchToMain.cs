using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class LA_SwitchToMain : MonoBehaviour
{

    private ColorManager colorManager;
    private AsyncOperation async;
    private bool Switching = false;
	public RectTransform loadingBar;
	public TextMesh loadingText;
	public float loadingPercent = 0.0f;
	private bool loaded = false;
	private bool textPhased = false;
	private float textPhase = 0.0f;

    void Start()
    {

        colorManager = GameObject.Find("ColorManager").GetComponent<ColorManager>();

		//GetComponent<SphereCollider>().enabled = false;

        StartCoroutine(LoadNewLevelAsync(2));

      

    }

	private void Update()
	{
		if(loaded){
			if(textPhase < 1.0f && textPhased == false){
				textPhase += Time.deltaTime/2;
			} 
			if(textPhase >= 1.0f){
				textPhased = true;
				loadingText.text = "Thank you for your patience,\nplease gaze upon the yellow sphere to start";
			}
			if(textPhased == true && textPhase > 0.0f){
				textPhase -= Time.deltaTime/2;
			}
			
			loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, 1.0f-textPhase);
		}
	}


	void HitByRay()
    {

        if (!Switching)
        {

            StartCoroutine(GoToLoadedScene());
            Switching = true;
            GetComponent<SphereCollider>().enabled = false;
			GameObject.Find("Player").GetComponent<SmoothLerp>().enabled = false;
            GameObject.Find("CursorManager").GetComponent<HideCursor>().StartFadeOutCursor();


        }

    }

    private IEnumerator LoadNewLevelAsync(int sceneNumber)
    {
        async = SceneManager.LoadSceneAsync(sceneNumber);
        async.allowSceneActivation = false;
		
        // Wait until the asynchronous scene fully loads
        while (!async.isDone)
        {
			loadingPercent = async.progress + 0.1f;
			loadingBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 10*loadingPercent);
			if(loadingPercent >= 0.9f){
				loaded = true;
				GetComponent<SphereCollider>().enabled = true;
				
			}
            yield return null;
        }

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
