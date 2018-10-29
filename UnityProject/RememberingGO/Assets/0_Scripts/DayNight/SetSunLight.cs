using UnityEngine;
using System.Collections;

public class SetSunLight : MonoBehaviour
{
    Material sky;


    // Use this for initialization
    void Start()
    {

        sky = RenderSettings.skybox;

    }

    bool lighton = false;

    // Update is called once per frame
    void Update()
    {

    
        if (Input.GetKeyDown(KeyCode.T))
        {

            lighton = !lighton;

        }

        Vector3 tvec = Camera.main.transform.position;
     

    }
}