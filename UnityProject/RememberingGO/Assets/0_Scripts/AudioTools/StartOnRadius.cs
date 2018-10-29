using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class StartOnRadius : MonoBehaviour
{

    private AudioSource aSource;
    private Transform player;
    public float distance;

    void Start()
    {

        aSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        distance = aSource.maxDistance;

    }

    void Update()
    {

        float dist = Vector3.Distance(aSource.transform.position, player.position);
        if (dist < distance)
        {
			print("Play Audio! -> "+this.gameObject.name); 
			aSource.Play();
            Destroy(this);
        }

    }

}
