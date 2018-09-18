using UnityEngine;
using System.Collections.Generic;

// AnimtaionObject has an Editor script, you can find it in GamePlayZoneEditor.cs
public class AnimationObject : MonoBehaviour 
{

    //Start variable declaration ------------

    public bool startActive;

    public List<GameObject> NewLaObjects;

    public int newAnimationObject;

    public Object[] prefabs;

    public bool showFoldout = true;

    //End variable declaration -----------

    void Start()
    {

        HidingOnStart();

    }

    // This method is used initially to create the object with a prefab as a child.
    public void SetPrefab() 
    {

        prefabs = Resources.LoadAll<GameObject>("LA_Objects/BaarMoeder");

        int randomPrefab = Random.Range(0, prefabs.Length);

        GameObject newPrefab = Instantiate(prefabs[randomPrefab], transform.position, Quaternion.identity) as GameObject;

        newPrefab.transform.parent = transform;

        newPrefab.GetComponentInChildren<LA_Animation>().WakeUp();

        startActive = true;

    }

    // The new children that are dragged into the inspector are set to children of this object once the button is pressed.
    public void ApplyNewChildren() 
    {

        NewLaObjects = new List<GameObject>();

        foreach (Transform child in transform)
        {

            if(child.parent == transform)
            {

                if (child.GetComponent<AnimationObject>())
                {

                    NewLaObjects.Add(child.gameObject);

                }
              
            }

        }
           
    }

    // When the child of this object with a sphere collider has been looked at, this method is called.
    public void EnableNext() 
    {

        for (int i = 0; i < NewLaObjects.Count; i++)
        {

            GamePlayArea gamePlayArea = GetComponentInParent<GamePlayArea>();

            if (!NewLaObjects[i].activeInHierarchy)
            {
                gamePlayArea.MoveMovers(transform.position, NewLaObjects[i].transform.position, 0.5f, this);
            }

        }

    }

    public void AddOneEmptySpot()
    {

        NewLaObjects.Add(null);

    }

    public void RemoveOneSpotFromList()
    {

        NewLaObjects.RemoveAt(NewLaObjects.Count -1);

    }

    // The children of this object are hidden, but there is a button to show them, using this method
    public void ShowMyChildren() 
    {

        UnhideMe();

        foreach(GameObject LaObject in NewLaObjects){

            LaObject.SetActive(true);

        }

    }

    // Create a new prefab for the selected AnimationObject
    public void NewPrefab(int newObject)
    {

        if(prefabs.Length < 1)
        {

            SetPrefab();

        }
        else
        {
            ClearPrefab();

            GameObject newPrefab = Instantiate(prefabs[newObject], transform.position, Quaternion.identity) as GameObject;
            newPrefab.transform.parent = transform;

            newPrefab.GetComponentInChildren<LA_Animation>().WakeUp();
        }

        

    }

    // Clear the current Prefab on this AnimationObject
    public void ClearPrefab()
    {
        //prefabs = Resources.LoadAll<GameObject>("LA_Objects/BaarMoeder");
        foreach (Transform child in transform)
        {

                DestroyImmediate(child.gameObject);

        }

    }

    // HidingOnStart is called from the GamePlayArea.cs on scene start, if this object needs to be hidden at start;
    public void HidingOnStart()
    {

        if(!startActive)
        {

            gameObject.SetActive(false);

        }

    }

    // If this AnimationObject needs to be unhidden, this method can be called
    public void UnhideMe()
    {

        gameObject.SetActive(true);

    }
}