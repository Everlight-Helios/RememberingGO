using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// This is the main movement/control class for our player
// It uses Vector3.Lerp and SphereCast to move the player around.
// The interaction with world objects also starts from this class.
public class SmoothLerp : MonoBehaviour
{
    // Start Declaration ---------------------------------------------------


    public float smooth = 0;

    // With this mask we set all the layers with which our Raycast need to collide
    public LayerMask mask;

    private Camera occulusCamera;

    private Transform target, LaTarget;

    private Image lerpCircle;

    private Vector3 startPos, endPos;

    [SerializeField]
    private float lerpTimeInSeconds = 25f;

    private bool moving;

    [SerializeField]
    [Range(10, 300)]
    private int rayDistance = 100;

    [Range(0f, 5f)]
    [SerializeField]
    private float lerpCircleSize = 3, circleShrinkSpeed = 1f;
    private float lerpCircleScale;
    private Color c;

    // These Circles are found in the Canvas but should be auto applied on player creation; 
    [SerializeField]
    private AudioSource m_circleInSound, m_circleOutSound;

    private bool finishPlaying = false;

    private float time, maxTime = 54.857f;

    public float TimerTime
    {

        get { return time; }

    }

    // End Declaration   ---------------------------------------------------

    void Timer()
    {

        time += Time.deltaTime;

        if(time > maxTime)
        {

            time = 0;

        }

    }

    void Start()
    {

        Cursor.visible = false;
        Initialize();

    }

    void Initialize()
    {

        // Initialize the circle (in the canvas) and the occulusCamera
        lerpCircle = GameObject.Find("LerpCircle").GetComponent<Image>();
        occulusCamera = GameObject.Find("CenterEyeAnchor").GetComponent<Camera>();

        // Initialize the default setting for the lerpCircle
        c = lerpCircle.color;
        c.a = 0;
        lerpCircle.color = c; //Set the alpha to 0

        lerpCircle.rectTransform.localScale = new Vector3(lerpCircleSize, lerpCircleSize, 1); //Set lerCircleScale to it's start value

    }

    // Any size change in the circle is done here:
    void UpdateLerpCircle(float opacityInput, float scaleInput)
    {

        c.a += opacityInput;
        c.a = Mathf.Clamp(c.a, 0f, 1f);

        lerpCircleScale += scaleInput;
        lerpCircleScale = Mathf.Clamp(lerpCircleScale, 0f, lerpCircleSize);
        lerpCircle.rectTransform.localScale = new Vector3(lerpCircleScale, lerpCircleScale, 1);

        if (lerpCircleScale < 0.5f) c.a = 0;

        lerpCircle.color = c;

    }

    // FixedUpdate runs in sync with the Unity3D physicsengine, Physics.SphereCast is part of unityphysics, therefore its run in this method;
    // In FixedUpdate we detect what the player is looking at, depending on the layer, different things happen.
    void FixedUpdate()
    {

        #region MouseVisable
        if (Input.GetMouseButtonDown(1))
        {
            if (!Cursor.visible)
            {

                Cursor.visible = true;

            }
            else
            {

                Cursor.visible = false;

            }
        }
        #endregion

        Debug.DrawRay(occulusCamera.transform.position, occulusCamera.transform.forward * rayDistance, Color.red);

        //Debug.DrawLine(transform.position, transform.position + new Vector3(0, 0.3f, 0), Color.blue);

        RaycastHit hit;

        if (Physics.SphereCast(occulusCamera.transform.position, 2, occulusCamera.transform.forward, out hit, rayDistance, mask))
        {

            // Layer 12 is the SoundSpot layer, used on all worldObject that have interactivity or sould make a sound.
            // When the Circle is small enough, we send a message to the targeted object.
            if (hit.transform.gameObject.layer == 12)
            {
				print("Hit!");
                LaTarget = hit.collider.gameObject.transform;
                UpdateLerpCircle(0.25f, -Time.deltaTime * circleShrinkSpeed);

            }

        }
        else
        {

            // Set all targets to null if nothing is in our direct view
            LaTarget = null;
            UpdateLerpCircle(-0.25f, 0.25f);

        }
    }

    // Update runs every frame, we use it to start transitions or send messages to targets
    void Update()
    {

        Timer();

        // If any target:
        if (LaTarget != null)
        {

            // Play sound when the circle is showing and set bool to true;
            if (c.a > 0.99f)
            {

                if (m_circleInSound.volume < 1)
                {
                    m_circleInSound.Stop();
                }

                m_circleInSound.volume = 1;

                if (!m_circleInSound.isPlaying)
                {
                    m_circleInSound.Play();
                }

            }

            // Do different things depending on the current target, only do this when the circle scale is below 0.5
            if (lerpCircleScale < 0.5f)
            {

                // if its a soundspot, send a message, the functionality is in the receiving object;
                if (LaTarget)
                {

                    LaTarget.SendMessage("HitByRay", time);
                    finishPlaying = true;

                }
            }
        }
        // If we don't see anything, set bool to false. This disables the circle again.
        else if (m_circleInSound.volume > 0  && !finishPlaying)
        {

            //showLerpCircle = false;
            m_circleInSound.volume -= Time.deltaTime * 2;

        }

        if (m_circleInSound.time > 3.3f)
        {

            finishPlaying = false;
           
        }

    }

    // Other objects can call this instance to move the player
    public void SetLerp(Vector3 end, float timeToRun)
    {

        StartCoroutine(RunLerp(transform.position, end, timeToRun));

    }

    public void StopCoroutine()
    {

        StopCoroutine();

    }

    // This Coroutine moves the player to the target Attractor
    private IEnumerator RunLerp(Vector3 start, Vector3 end, float timeToRun)
    {


        float time = 0;
        smooth = 0;
        lerpCircleScale = lerpCircleSize;

        if (!moving)
        {
            moving = true;
            while (smooth < 1)
            {




                time += Time.deltaTime;
                smooth = time / timeToRun;
                transform.position = Vector3.Lerp(start, end, smooth);

                yield return new WaitForEndOfFrame();

            }

            moving = false;


        }
    }
}
