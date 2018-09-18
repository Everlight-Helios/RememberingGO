using UnityEngine;
using System.Collections;
using RememberingManagers;

[AddComponentMenu("Animation/Start Animation on LookAt")]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(AudioSource))]
public class LA_Animation : MonoBehaviour {

    public Animator animatorObject;

    private AudioSource aSource;

    [SerializeField]
    private bool once = false;

    public void WakeUp()
    {

        gameObject.layer = 12;
        transform.localPosition = Vector3.zero;
        GetComponent<AudioSource>().playOnAwake = false;

    }

    void Start()
    {

        //animatorObject.Play("idle", -1, Random.Range(0f, 1f));
		//animatorObject.Play();
        aSource = GetComponent<AudioSource>();
        

    }
    
	
	void HitByRay () {


        animatorObject.SetBool("Play", true);

        if (!once)
        {

            once = true;
            GetComponent<SphereCollider>().enabled = false;
            aSource.Play();

        }
	}
}
