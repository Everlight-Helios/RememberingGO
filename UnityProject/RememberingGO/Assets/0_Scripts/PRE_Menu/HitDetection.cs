using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Lay Class
public class HitDetection : MonoBehaviour {

	public bool hit = false;
  
    public float _x = 1f;
    public bool Lay = false;
 
    public GameObject tc;
    
    float speed = 0.3f;
    private IEnumerator coroutine;

    // Use this for initialization
    void Start ()
    {
        if (tc == null)
        {
            tc = GameObject.Find("LaySelect");
        }
        
        coroutine = GoMove(_x);

        
    }


	void HitByRay()
    {
        GameObject.Find("CameraManager").GetComponent<CameraManager>().liggen = true;
        
        if (!Lay) //To make sure it runs 1 time
        {

            Lay = true;
            GetComponent<SphereCollider>().enabled = false; //To make sure it doesn't get hit again
            GameObject.Find("Stand").GetComponent<SphereCollider>().enabled = false; //To make sure the other one cant get hit 
            GameObject.Find("CursorManager").GetComponent<HideCursor>().StartFadeOutCursor(); //remove Cursor
            StartCoroutine(coroutine);
            
        }

    }

    private IEnumerator GoMove(float x)
    {
        tc.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        tc.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        tc.SetActive(true);
        GameObject.Find("Stand").SetActive(false);
        yield return new WaitForSeconds(0.1f);
        tc.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        tc.SetActive(true);
        GameObject.Find("CameraManager").GetComponent<Loadlevel>().gogo = true;
        
        yield return new WaitForSeconds(x);
        

    }

}
