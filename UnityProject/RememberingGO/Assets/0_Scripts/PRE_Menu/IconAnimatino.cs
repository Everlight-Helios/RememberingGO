using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconAnimatino : MonoBehaviour {

    Transform stand;
    Transform lay;
    float speed = 0.3f;
    bool blink = false;
    public bool l = false;
    //Renderer[] r;

	// Use this for initialization
	void Start () {
        l = LyingAnimation.run;
        stand = this.gameObject.transform.GetChild(0);
        lay = this.gameObject.transform.GetChild(1);
        //r = this.gameObject.GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Liggen()
    {
       
        /*
         * if (blink)
         * {
            if (Time.fixedTime % .5 < .2)
            {
                renderer.enabled = false;
            }
            else
            {
                renderer.enabled = true;
            }
        }
        */

        if (l)
        {
            //Invoke("fadeToBlack", 3);
            speed += 0.1f;  //increase speed with every frame 
                            // move stand Image backwards
            stand.transform.position = Vector3.Lerp(stand.transform.position, new Vector3(0f, 0f, 50f), speed * Time.deltaTime);
            //move lay Image down to 0 position
            lay.transform.position = Vector3.Lerp(lay.transform.position, new Vector3(0f, 0f, 20f), speed * Time.deltaTime);
        }
    }
  
}
