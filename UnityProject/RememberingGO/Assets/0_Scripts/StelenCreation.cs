using UnityEngine;
using System.Collections;

/// <summary>
/// This script has an Editor script to show the interaction.
/// You can vind it at Editor/StelenCreation
/// </summary>

public class StelenCreation : MonoBehaviour {

    private Transform origin;

    private Object[] prefabs;

    public GameObject prefabOne, prefabTwo;

	public void Setup (Transform originObject) {

        origin = originObject;

        prefabs = Resources.LoadAll<GameObject>("LA_Objects/Particles");

        int randomNumber = Random.Range(0, prefabs.Length);

        GameObject newObject = Instantiate(prefabs[randomNumber], origin.position, origin.rotation) as GameObject;

        newObject.transform.parent = origin;
	
	}

    public void SetSpecificPrefab(GameObject prefab)
    {

        GameObject newObject = Instantiate(prefab, origin.position, origin.rotation) as GameObject;
        newObject.transform.parent = origin;

    }

    public void SetSpecificPrefabs(GameObject prefabOne, GameObject prefabTwo)
    {

        int index = 0;

        foreach(Transform originObject in transform)
        {

            if(originObject.name == "LA_origin")
            {

                if(originObject.childCount == 0)
                {
                    index++;
                    if (index == 1)
                    {

                        GameObject newObject = Instantiate(prefabOne, originObject.position, originObject.rotation) as GameObject;
                        newObject.transform.parent = originObject;

                    }

                    if(index == 2)
                    {

                        GameObject newObject = Instantiate(prefabTwo, originObject.position, originObject.rotation) as GameObject;
                        newObject.transform.parent = originObject;

                    }
                }    
            }
        }
    }
}
