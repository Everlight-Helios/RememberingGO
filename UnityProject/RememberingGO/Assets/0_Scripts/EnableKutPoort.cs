using UnityEngine;
using System.Collections;

public class EnableKutPoort : MonoBehaviour {


    [SerializeField]
    private GameObject kutPoort;

    private bool move = false;

    void Start()
    {

        kutPoort.SetActive(false);
        GetComponent<GeboorteSwitcher>().OnFinalFase += EnableMe;

    }

    void EnableMe()
    {
		
        kutPoort.SetActive(true);
		print(kutPoort.activeInHierarchy);
        StartCoroutine(Count());
        
    }

    void Update()
    {
		//print(kutPoort.transform.localPosition.z);

		if (move){
			kutPoort.transform.Translate(0, 0, -Time.deltaTime * 2.7f);
		}

        if(kutPoort.transform.localPosition.z < 0)
        {

            move = false;
            kutPoort.SetActive(false);


        }

    }

    private IEnumerator Count()
    {

        yield return new WaitForSeconds(25);
        move = true;


    }


}
