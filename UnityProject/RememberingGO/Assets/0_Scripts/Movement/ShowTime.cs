using UnityEngine;
using System.Collections;

public class ShowTime : MonoBehaviour {

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float positionOnPath;

    [SerializeField]
    [Range(0.0f, 30.0f)]
    private float speed;

    public MotionPath motionPath;

    public Vector3 pos;
    public float diskSize;

    public string time;

    void OnDrawGizmos()
    {


        pos = motionPath.PointOnNormalizedPath(positionOnPath);

        Gizmos.DrawWireSphere(pos, 5);

        time = (positionOnPath * (motionPath.length / speed)).ToString("N2");


    }

}
