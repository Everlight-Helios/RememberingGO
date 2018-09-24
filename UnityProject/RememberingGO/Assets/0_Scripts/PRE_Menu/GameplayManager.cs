using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{

    public static bool lyingDown = false;

    public Texture lyingTexture, standingTexture;

    private bool choiceMade = false;
    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (lyingDown) { }
    }

    private void OnGUI()
    {
        if (!choiceMade)
        {
            if (!lyingTexture || !standingTexture)
            {
                Debug.LogError("Assign a Texture in the inspector.");
                return;
            }
            GUI.DrawTexture(new Rect(10, 10, 60, 60), lyingTexture, ScaleMode.ScaleToFit, true, 10.0F);
            GUI.DrawTexture(new Rect(10, 10, 60, 60), standingTexture, ScaleMode.ScaleToFit, true, 10.0F);
        }
    }
}
