using UnityEngine;

// This Script makes the character controller run on the given path ( Set path in inspector )
public class FollowMotionPath : MonoBehaviour
{

    //Game State
    public bool moving;

	private bool finished = false;

    public float startSpeed = 3;

    //The transforms are used to put the character on the floor and  
    //move the Camera side to side over its local X-axis.
    public Transform character = null;


	private float speed;

    public float CurrentSpeed
    {

        get { return speed; }
        set { speed = value; }

    }

	//In inspector, drag and drop the path you want this character to follow
	public MotionPath motionPath;

	//The starting position on the path
	private float startPosition = 0.01f;

	//Do we loop if we come to the end of the path
	private bool loop = false;

	//This is the position where our camera looks at, based on the percentage of the path
	public float lookAheadAmount;

	//Path percentage and the actural position in Vector3
	public float uv;
	private Vector3 pointOnPath;
	public float publicSpeed;



	void Awake()
	{
		speed = startSpeed;
		//Set our initial position to our starting position
		uv = startPosition;
		if (motionPath == null)
			enabled = false;
	}

	void FixedUpdate()
	{
		publicSpeed = speed;
        
        if(moving)
        uv += ((speed / motionPath.length) * Time.fixedDeltaTime);
		//print(speed);
        //if loop is true close the track and put the player back on the start when the end is reached
        if (loop) uv = (uv<0?1+uv:uv) %1;

		//if not, just stop at the end
		else if (uv > 1) enabled = false;

     

        //Create new Vector3's for the character pos, the normalized pos and the target pos.
        Vector3 pos = motionPath.PointOnNormalizedPath(uv);
		Vector3 norm = motionPath.NormalOnNormalizedPath(uv);
		Vector3 target = motionPath.LookAheadOnPath(uv + lookAheadAmount);

		//Set the characters position to the new position.
		pointOnPath = pos;

        this.transform.position = pointOnPath;

        //Make the character look at the target but only rotate along it's local y-axis.
        //this.character.forward = this.speed >0?norm:-norm;
		//this.character.LookAt(target);
		//this.character.eulerAngles = new Vector3( 0, this.transform.eulerAngles.y, 0 );		

	}


}



	
