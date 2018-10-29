using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class ColorPoint : MonoBehaviour
{


    [SerializeField]
    private Color skyColor, groundColor, fogColor;

    [SerializeField]
    [Range(0f, 1f)]
    private float fogDensity = 0.06f;

    [SerializeField]
    [Range(0f, 40f)]
    private float fadeTime = 5f;

   
   

    public GameObject player;

    private ColorManager manager;

    private bool once;

    [SerializeField]
    private enum FadeMode { Nothing, FadeIn, FadeOut, Background }
	public bool drawGizmo = false;

    [SerializeField]
    private FadeMode fadeMode = FadeMode.Nothing;

    void Start()
    {
		
        manager = GameObject.Find("ColorManager").GetComponent<ColorManager>();
		
        player = GameObject.Find("Player");

        

    }

    void Update()
    {
		

    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player && !once)
        {

            SendMyColor();

            switch (fadeMode)
            {

                case FadeMode.Nothing:
                    break;

                case FadeMode.FadeIn:
                    manager.FadeInOverlay();
                    break;

                case FadeMode.FadeOut:
                    manager.FadeOutOverlay();
                    break;

                case FadeMode.Background:
                    Camera.main.backgroundColor = skyColor;
                    break;

            }

            this.gameObject.SetActive(false);

        }
	}

	public void SendMyColor()
    {

        manager.SetTransitions(fadeTime);
        manager.FadeNewColor(skyColor, groundColor, fogColor, fogDensity);
        once = true;

    }


    public void Initialise()
    {

        Material skybox = RenderSettings.skybox;
        
        skyColor = skybox.GetColor("_SkyColor");
        groundColor = skybox.GetColor("_HorizonColor");
        fogColor = RenderSettings.fogColor;
		

        player = GameObject.Find("Player");

        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");


    }

    void OnDrawGizmos()
    {
		if(drawGizmo){
			BoxCollider collider = GetComponent<BoxCollider>();

			Gizmos.color = new Color(1,0,0,0.3f);
			Gizmos.DrawCube(transform.position, new Vector3(collider.size.x, collider.size.y, collider.size.z));
		}

    }

    public void SetMyColor()
    {

        RenderSettings.skybox.SetColor("_SkyColor", skyColor);
        RenderSettings.skybox.SetColor("_HorizonColor", groundColor);
        RenderSettings.fogColor = fogColor;
        RenderSettings.fogDensity = fogDensity;

    }

}
