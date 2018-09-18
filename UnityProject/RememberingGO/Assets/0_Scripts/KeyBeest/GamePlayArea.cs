using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// GamePlayArea has an Editor script, you can find it in GamePlayZoneEditor.cs
public class GamePlayArea : MonoBehaviour
{

    #region Variables

    // First variables are for the general GamePlayArea settings, see the GamePlayZoneEditor.cs for the custom inspector settings;

    // Start Declaration ---------------------------------------------------

    public int activeCount;
    public int Amount;
    public int Radius;

    public int numberOfSplines = 3;
    public GameObject[] splines = new GameObject[10];
    public int[] startChecks = new int[10];

    public GameObject moveParent;
    public GameObject[] movers;
    public List<GameObject> AnimatedObjects = new List<GameObject>();

    public float eersteSpline, tweedeSpline;
    public float min = 0, max = 2;

    public bool showFoldout = true, showkeyBeestSplines = true;

    // End Declaration   ---------------------------------------------------

    // The next variables are all for making the keyBeest move on the splines

    // Start Declaration ---------------------------------------------------

    public GameObject keyBeest;
    private BezierSpline currentSpline;

    public float duration;

    public SplineWalkerMode mode;

    private float progress;
    private bool goingForward = true;

    // End Declaration   ---------------------------------------------------
    #endregion

    // The AnimationObjects are created in line, movers give feedback to the player as the where they spawn.
    // These movers first need to be created, if a scene/gamePlayArea has no movers, this method is called upon.
    public void SetupMovers()
    {

        if (!GameObject.Find("Movers"))
        {

            GameObject prefab = Resources.Load<GameObject>("GamePlayArea/Movers");
            moveParent = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
            moveParent.name = "Movers";

        }

        int index = 0;

        moveParent = GameObject.Find("Movers");

        movers = new GameObject[moveParent.transform.childCount];

        foreach (Transform child in moveParent.transform)
        {

            movers[index] = child.gameObject;
            index++;

        }

    }

    // The aformentioned movers move with a Coroutine, this method starts the Coroutine;
    public void MoveMovers(Vector3 start, Vector3 end, float time, AnimationObject moverTarget)
    {
        for (int i = 0; i < movers.Length; i++)
        {

            if (!movers[i].activeInHierarchy)
            {
                movers[i].SetActive(true);
                StartCoroutine(movers[i].GetComponent<Movers>().RunLerp(start, end, time, moverTarget));
                break;
            }

        }


    }

    // This method is called upon initialization of a gamePlayArea
    public void Initialize()
    {



        for (int initial = 0; initial < Amount; initial++)
        {

            GameObject newObject = new GameObject("AnimationObject" + AnimatedObjects.Count);
            newObject.transform.parent = transform;
            newObject.transform.localPosition = Random.insideUnitSphere * Radius;
            newObject.AddComponent<AnimationObject>().SetPrefab();

            AnimatedObjects.Add(newObject);

        }


    }

    // This method is called by a button, it clears all children from a GamePlayArea
    public void Clear()
    {

        foreach (Transform child in transform)
        {

            AnimatedObjects.Remove(child.gameObject);
            DestroyImmediate(child.gameObject);


        }

        if (transform.childCount > 0)
        {

            Clear();

        }

        AnimatedObjects.Clear();

    }

    // This method re-creates the spline array for the custom inspector view
    public void CreateSplineArray(int p_numberOfSplines)
    {

        splines = new GameObject[p_numberOfSplines];

    }

    // This method is called from SmoothLerp.cs when an LA_Object has been lookt at;
    public void ChangeActiveCount()
    {

        activeCount++;

        for (int i = 0; i < splines.Length; i++)
        {

            if (startChecks[i] <= activeCount)
            {

                currentSpline = splines[i].GetComponent<BezierSpline>();
                startChecks[i] = 99;
                Debug.Log(currentSpline);
                StartCoroutine(MoveKeyBeest());

            }

        }

    }

    // Cycle though all AnimationObjects and hide them from the player.
    private void Start()
    {

        for (int i = 0; i < Amount; i++)
        {

            AnimatedObjects[i].GetComponent<AnimationObject>().HidingOnStart();

        }

    }

    // Move the KeyBeest on the pre made splines
    private IEnumerator MoveKeyBeest()
    {

        if (currentSpline)
        {

            progress = 0f;

            while (progress < 1)
            {

                progress += Time.deltaTime / duration;

                Vector3 position = currentSpline.GetPoint(progress);
                keyBeest.transform.localPosition = position;
                keyBeest.transform.LookAt(position + currentSpline.GetDirection(progress));

                yield return new WaitForEndOfFrame();

            }

        }

    }

}
