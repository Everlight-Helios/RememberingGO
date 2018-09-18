using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class HideZone : MonoBehaviour {

    public int groupToShow, groupToHide;

    public GameObject player;
    public GameObject specificObjectToHide;

    private HideGroups hideGroups;
    private BoxCollider col;

    public bool showObjects, hideObjects, hideSpecificObject;
	public bool drawGizmo;

    void Start()
    {

        col = GetComponent<BoxCollider>();
        hideGroups = GameObject.Find("HideGroups").GetComponent<HideGroups>();
		
		player = GameObject.Find("Player");
		

    }

    void Update()
    {

        /*if (col.bounds.Contains(player.transform.position))
        {

            if (showObjects)
            {
                hideGroups.ShowGroup(groupToShow);
            }

            if (hideObjects)
            {

                hideGroups.HideGroup(groupToHide);

            }

            if (hideSpecificObject)
            {

                specificObjectToHide.SetActive(false);

            }

            this.enabled = false;
           

        }*/

    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player)
        {

            if (showObjects)
            {
                hideGroups.ShowGroup(groupToShow);
            }

            if (hideObjects)
            {

                hideGroups.HideGroup(groupToHide);

            }

            if (hideSpecificObject)
            {

                specificObjectToHide.SetActive(false);

            }

            this.gameObject.SetActive(false);
           

        }
	}

	public void Initialize()
    {

        player = GameObject.Find("Player");
        

    }

    void OnDrawGizmos()
    {
		if(drawGizmo){
			BoxCollider collider = GetComponent<BoxCollider>();

			Gizmos.color = new Color(0, 1, 0, 0.3f);
			Gizmos.DrawCube(transform.position, new Vector3(collider.size.x, collider.size.y, collider.size.z));
		}

    }


}
