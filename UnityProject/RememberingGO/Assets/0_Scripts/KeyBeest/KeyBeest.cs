using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class KeyBeest : MonoBehaviour
{

    [SerializeField]
    private GameObject keyBeest, keyBeestTrail;

    [SerializeField]
    [Range(0f, 10f)]
    private float offset = 2f, speed = 2f, scale = 1;

    private TrailRenderer keyBeestTrailRender;
    private SphereCollider col;

    float currentVelocity;
    float oldValue;
    bool krimpen;


    // Use this for initialization
    void Start()
    {

        Debug.Log(keyBeest.name);
        keyBeestTrailRender = keyBeestTrail.GetComponent<TrailRenderer>();
        col = GetComponent<SphereCollider>();

        gameObject.layer = 12;

        keyBeestTrailRender.time = 0;

    }


    // Update is called once per frame
    void Update()
    {

        float newTime = keyBeestTrailRender.time;

        keyBeest.transform.position = new Vector3(2 * Mathf.PI * Mathf.Sin(Time.time * speed), 2 * Mathf.PI * Mathf.Cos(Time.time * speed), 0) * offset + transform.position;
        col.center = keyBeest.transform.position - transform.position;

        if(newTime == oldValue)
        {

            krimpen = true;

        }

        if(krimpen && keyBeestTrailRender.time > 0)
        {

            keyBeestTrailRender.time -= Time.deltaTime;

        }

        oldValue = keyBeestTrailRender.time;

    }

    void HitByRay()
    {

        keyBeestTrailRender.time += Time.deltaTime;
        krimpen = false;


    }


}
