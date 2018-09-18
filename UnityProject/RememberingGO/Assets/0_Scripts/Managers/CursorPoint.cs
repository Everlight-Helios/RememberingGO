using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(BoxCollider))]
public class CursorPoint : MonoBehaviour {

    private BoxCollider col;
    public GameObject player;
    public HideCursor cursorManager;

    public bool hide;
    private bool once;

    void Start()
    {

        col = GetComponent<BoxCollider>();
		player = GameObject.Find("Player");

    }

    void Update()
    {

        /*if (col.bounds.Contains(player.transform.position) && !once)
        {

            if (hide)
            {

                cursorManager.StartFadeOutCursor();

            }
            else
            {

                cursorManager.StartFadeInCursor();

            }

            once = true;

        }*/
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject == player){
			if (hide)
            {

                cursorManager.StartFadeOutCursor();

            }
            else
            {

                cursorManager.StartFadeInCursor();

            }
			this.gameObject.SetActive(false);
		}
	}

}
