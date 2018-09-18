using UnityEngine;
using System.Collections;

public class MovementManager : MonoBehaviour {

    [SerializeField]
    [Range(1.0f, 30.0f)]
    private float transitionTime;

    private float playerSpeed;

    public float ChangePlayerSpeed
    {

        get { return playerSpeed; }
        set { playerSpeed = value; }

    }

    public float ChangeTransitionTime
    {

        get { return transitionTime;  }
        set { transitionTime = value; }

    }


    private GameObject player;

    [SerializeField]
    private float[] stops;

    [SerializeField]
    private MotionPath motionPath;

    private int currentFase = 0;

    private float currentVelocity, smoothTime = 5.0f;

    private FollowMotionPath followPath;


    void Start()
    {

        player = GameObject.Find("Player");
        followPath = player.GetComponent<FollowMotionPath>();

        ChangePlayerSpeed = followPath.CurrentSpeed;

    }

    void Update()
    {

        followPath.CurrentSpeed = Mathf.SmoothDamp(followPath.CurrentSpeed, playerSpeed, ref currentVelocity, transitionTime);

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            SetNextFase();

        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            if (followPath.moving)
            {
                followPath.moving = false;
            }
            else
            {

                followPath.moving = true;

            }
             

        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            SetPreviousfase();

        }
#endif


    }

#if UNITY_EDITOR
    public void SetNextFase()
    {

        currentFase++;

   

       if(currentFase < stops.Length)

         followPath.uv = stops[currentFase];

     
    }

    public void SetPreviousfase()
    {

        currentFase--;

  

        if(currentFase > -1)

            followPath.uv = stops[currentFase];

    }
#endif

    public void SetFase(int fase)
    {

        followPath.uv = stops[fase];

    }

    void OnDrawGizmos()
    {



        for(int i = 0; i < stops.Length; i++)
        {
            Gizmos.color = Color.red;

            try
            {
                Vector3 pos = motionPath.PointOnNormalizedPath(stops[i]);
                Gizmos.DrawWireSphere(pos, 5);
            }
            catch ( System.NullReferenceException nullEx)
            {

                Debug.Log(nullEx);

            }
            
    
            
        }
        

        

       // time = (positionOnPath * (motionPath.length / speed)).ToString("N2");


    }

}
