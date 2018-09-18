using UnityEngine;
using System.Collections;

public class MovePlanes : MonoBehaviour {

    [SerializeField]
    [Range(-1f, 1f)]
    private float moveDirection;

	void Start ()
    {

        transform.localPosition = new Vector3(12 * moveDirection, 0, 0);

	}

    void OnEnable()
    {

        StartCoroutine(MoveToSide());

    }

    private IEnumerator MoveToSide()
    {

        yield return new WaitForSeconds(20);

        while(transform.localPosition.x < 12.6f && transform.localPosition.x > -12.6f)
        {
            transform.Translate(Time.deltaTime * moveDirection * 0.25f, 0, 0);
            yield return new WaitForEndOfFrame();
        }

    }

}
