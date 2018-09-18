using UnityEngine;
//using UnityEngine.PostProcessing;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class SetDoF : MonoBehaviour
{

    private BoxCollider col;

	[SerializeField]
    private GameObject mainCam;
	public GameObject renderTex;
	

	

    

    private bool once;

    void Start()
    {

        col = GetComponent<BoxCollider>();

    }

    void OnTriggerEnter(Collider other)
    {
		if(other.gameObject == GameObject.Find("Player")){
			SetDepthOfField();
		}
    }

    void SetDepthOfField()
    {
		mainCam.SetActive(false);
		renderTex.SetActive(false);
		this.gameObject.SetActive(false);
        //depthOfField.focalLength = blurDistance;
        //depthOfField.focusDistance = focalSize;

    }

    void OnDrawGizmos()
    {

        BoxCollider collider = GetComponent<BoxCollider>();

        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position, new Vector3(collider.size.x, collider.size.y, collider.size.z));

    }


}
