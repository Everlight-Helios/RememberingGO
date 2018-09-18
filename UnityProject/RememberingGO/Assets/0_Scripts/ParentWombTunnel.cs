using UnityEngine;
using System.Collections;

public class ParentWombTunnel : MonoBehaviour {

    public delegate void OnActive();
    public OnActive Activation;

    [SerializeField]
    private Transform geboorteSwitcher;

    [SerializeField]
    private GameObject audioSources;

    [SerializeField]
    private GameObject player;
	public Transform cam;

    private BoxCollider col;

    void Start()
    {

        col = GetComponent<BoxCollider>();

    }

    void Update()
    {

        /*if (col.bounds.Contains(player.transform.position))
        {

            geboorteSwitcher.parent = cam.transform;
            geboorteSwitcher.localPosition = new Vector3(0, 0, 0);
            geboorteSwitcher.localRotation = new Quaternion(0,0,0,0);
            audioSources.SetActive(true);
            Activation();
            Destroy(this);

        }*/
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject == player){
			geboorteSwitcher.parent = cam.transform;
            geboorteSwitcher.localPosition = new Vector3(0, 0, 0);
            geboorteSwitcher.localRotation = new Quaternion(0,0,0,0);
            audioSources.SetActive(true);
            Activation();
			Destroy(this.GetComponent<Collider>());
            Destroy(this);
		}
	}
}
