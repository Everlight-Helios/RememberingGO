using UnityEngine;

public class HideGroups : MonoBehaviour
{

    public int numberOfHideTags, whatGroupToHide, whatGroupToShow;

    public GroupLists[] GroupList;

    public bool showHideGroups;


    public void Populate()
    {

        for (int i = 0; i < numberOfHideTags; i++)
        {
            if (GroupList[i] == null)
                GroupList[i] = new GroupLists();

            GroupList[i].hideGroups = GameObject.FindGameObjectsWithTag("HideGroup" + i);

        }

    }

    public void Clear()
    {

        GroupList = new GroupLists[numberOfHideTags];

    }

    public void HideObjects()
    {

        for (int i = 0; i < numberOfHideTags; i++)
        {

            foreach (GameObject hide in GroupList[i].hideGroups)
            {

                hide.SetActive(false);

            }
        }
    }

    public void ShowObjects()
    {

        for (int i = 0; i < numberOfHideTags; i++)
        {

            foreach (GameObject hide in GroupList[i].hideGroups)
            {

                hide.SetActive(true);

            }
        }
    }

    public void HideGroup(int groupToHide)
    {

        foreach (GameObject hide in GroupList[groupToHide].hideGroups)
        {

            hide.SetActive(false);

        }
    }

    public void ShowGroup(int groupToShow)
    {

        foreach (GameObject hide in GroupList[groupToShow].hideGroups)
        {

            hide.SetActive(true);

        }
    }
}

[System.Serializable]
public class GroupLists
{

    public GameObject[] hideGroups;

}