using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class HB_StartParticlesOnRadius : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem partSys;
    private AudioSource aSource;
    private Transform player;
    private float distance;

    //private ParticleSystem.EmissionModule emissionModule;
    private float rate;
	private bool started = false;

    private float time;

    private float shortTime = 27.428f, maxTime = 54.857f, currentTime;

	void Awake()
	{
		
	}

	void Start()
    {

        aSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        distance = aSource.maxDistance;
		partSys = transform.GetComponentInChildren<ParticleSystem>();
        
        

    }

    void Update()
    {
		var emissionModule = partSys.emission;
		if(started == false){
			emissionModule.enabled = false;
			started = true;
		}


        float dist = Vector3.Distance(aSource.transform.position, player.position);
        if (dist < distance)
        {

            time = player.GetComponent<SmoothLerp>().TimerTime;

            if (!aSource.isPlaying)
            {

                if (aSource.clip.length < (maxTime - 1))
                {

                    if (time > shortTime)
                    {
                        time -= shortTime;
                    }


                }

                emissionModule.enabled = true;
                aSource.Play();

                aSource.time = time;

                Destroy(this);

            }
        }
    }
}
