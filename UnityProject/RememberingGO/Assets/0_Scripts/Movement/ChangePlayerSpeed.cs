using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class ChangePlayerSpeed : MonoBehaviour
{

    [SerializeField]
    [Range(-250.0f, 250.0f)]
    private float newSpeed, newTransitionTime;

    private GameObject player;

    private BoxCollider col;
    private MovementManager moveManager;

    private bool once;

    void Start()
    {

        col = GetComponent<BoxCollider>();
        player = GameObject.Find("Player");
        moveManager = GameObject.Find("MovementManager").GetComponent<MovementManager>();

    }

    void Update()
    {

        /*if (col.bounds.Contains(player.transform.position) && !once)
        {

            moveManager.ChangePlayerSpeed = newSpeed;
            moveManager.ChangeTransitionTime = newTransitionTime;

            this.enabled = false;

        }*/

       

    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player){
			moveManager.ChangePlayerSpeed = newSpeed;
			moveManager.ChangeTransitionTime = newTransitionTime;
		}

        this.gameObject.SetActive(false);
	}

	void OnDrawGizmos()
    {

        Gizmos.color = Color.white;
        //Gizmos.DrawCube(transform.position, new Vector3(collider.size.x, collider.size.y, collider.size.z));

    }

}