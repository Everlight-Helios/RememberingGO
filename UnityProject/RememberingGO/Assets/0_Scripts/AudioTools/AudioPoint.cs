using UnityEngine;
using UnityEngine.Audio;
using RememberingManagers;

[RequireComponent(typeof(BoxCollider))]
public class AudioPoint : MonoBehaviour
{

    [SerializeField]
    private AudioMixerSnapshot snapShot;

    [SerializeField]
    private float fadeTime;

    private BoxCollider col;
    public GameObject player;
    private AudioManager manager;

    private bool once;
	public bool drawGizmo = false;

    void Start()
    {

        col = GetComponent<BoxCollider>();
        manager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		player = GameObject.Find("Player");

    }

    void Update()
    {

        /*if (col.bounds.Contains(player.transform.position) && !once)
        {

            manager.SetSnapShot(snapShot, fadeTime);

        }*/
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player)
        {

            manager.SetSnapShot(snapShot, fadeTime);

        }
		this.gameObject.SetActive(false);
	}

	void OnDrawGizmos()
    {
		if(drawGizmo){
			col = GetComponent<BoxCollider>();

			Gizmos.color = new Color(0, 0, 1, 0.3f);
			Gizmos.DrawCube(transform.position, new Vector3(col.size.x, col.size.y, col.size.z));
		}

    }
}
