using UnityEngine;
using UnityEditor;
using System.Collections;

public class CreateHideGroups {

    private static int amountOfNewObjects = 5;


    [MenuItem("Tools/LevelEditor/Create HideGoups")]
    private static void SetupRandomAudio()
    {

        if (!GameObject.Find("HideGroups"))
        {
            GameObject hideGroups = new GameObject("HideGroups");
            hideGroups.AddComponent<HideGroups>();

            for (int i = 0; i < amountOfNewObjects; i++)
            {

                GameObject hideZone = new GameObject("hideZone" + i);
                hideZone.AddComponent<HideZone>();
                hideZone.transform.parent = hideGroups.transform;
                hideZone.transform.position = new Vector3(hideGroups.transform.position.x + i, hideGroups.transform.position.y, hideGroups.transform.position.z);

            }

        }
        else
        {
            Debug.Log("Scene already has HideGroups!");
        }

    }

}
