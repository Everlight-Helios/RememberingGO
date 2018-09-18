using UnityEngine;
using System.Collections;

public class Movers : MonoBehaviour {

    private bool moving = false;
    


    public IEnumerator RunLerp(Vector3 start, Vector3 end, float timeToRun, AnimationObject callBack)
    {



        float time = 0;
        float smooth = 0;

        if (!moving)
        {
            moving = true;
            while (smooth < 1)
            {

                time += Time.deltaTime;
                smooth = time / timeToRun;
                transform.position = Vector3.Lerp(start, end, smooth);

                yield return new WaitForEndOfFrame();

            }

            moving = false;
            callBack.ShowMyChildren();
            gameObject.SetActive(false);

        }
    }
    /*
    public IEnumerator RunLerp(Vector3 start, Vector3 end, float timeToRun, TearDrop callBack)
    {



        float time = 0;
        float smooth = 0;

        if (!moving)
        {
            moving = true;
            while (smooth < 1)
            {

                time += Time.deltaTime;
                smooth = time / timeToRun;
                transform.position = Vector3.Lerp(start, end, smooth);

                yield return new WaitForEndOfFrame();

            }

            moving = false;
            callBack.ShockWave();
            yield return new WaitForSeconds(1);
            gameObject.SetActive(false);

        }
    }*/

}
