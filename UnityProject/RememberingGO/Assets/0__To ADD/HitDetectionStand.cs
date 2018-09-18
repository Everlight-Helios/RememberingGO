using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Stand Class
public class HitDetectionStand : MonoBehaviour {


    public float _x = 1f;
    public bool Standing = false;

    public GameObject tc;
    float speed = 0.3f;
    private IEnumerator coroutine;

    // Use this for initialization
    void Start ()
    {
       
        if (tc == null)
        {
            tc = GameObject.Find("StandSelect");
        }
          
        coroutine = GoMove(_x);

    }


    void HitByRay()
    {
        
        if (!Standing)
        {

            Standing = true;
            GetComponent<SphereCollider>().enabled = false; //To make sure it doesn't get hit again
            GameObject.Find("Lay").GetComponent<SphereCollider>().enabled = false;
            GameObject.Find("CursorManager").GetComponent<HideCursor>().StartFadeOutCursor(); //remove Cursor
            StartCoroutine(coroutine);
            

        }
    }

    private IEnumerator GoMove(float x) //Wait x Seconds before moving the image
    {
        tc.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        tc.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        tc.SetActive(true);
        GameObject.Find("Lay").SetActive(false);
        yield return new WaitForSeconds(0.1f);
        tc.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        tc.SetActive(true);
        GameObject.Find("CameraManager").GetComponent<Loadlevel>().gogo = true;
        
        yield return new WaitForSeconds(x);
      
        
    }

}
