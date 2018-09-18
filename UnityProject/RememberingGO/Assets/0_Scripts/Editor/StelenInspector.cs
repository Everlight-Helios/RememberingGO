using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(StelenCreation))]
public class StelenInspector : Editor
{

    StelenCreation stelen;



    public override void OnInspectorGUI()
    {


        int index = 0;

        stelen = target as StelenCreation;

        if (stelen.isActiveAndEnabled)
        {
            foreach (Transform origin in stelen.transform)
            {
                if (origin.name == ("LA_origin"))
                {

                    index++;
                    if (origin.childCount == 0)
                    {
                        stelen.Setup(origin);

                    }
                }
            }
        }


        stelen.prefabOne = (GameObject)EditorGUILayout.ObjectField(stelen.prefabOne, typeof(GameObject), true);

        if (index == 2)
            stelen.prefabTwo = (GameObject)EditorGUILayout.ObjectField(stelen.prefabTwo, typeof(GameObject), true);

        if (stelen.prefabOne != null)
        {
            if (GUILayout.Button("Set New Prefabs"))
            {

                if (index == 1)
                {
                    Reset(stelen.prefabOne);
                }
                else if (index == 2)
                {

                    Reset(stelen.prefabOne, stelen.prefabTwo);

                }

            }
        }


        if (GUILayout.Button("New Random Particle Flower"))
        {

            Clear();

        }

    }

    void Clear()
    {

        if (stelen.isActiveAndEnabled)
        {
            foreach (Transform origin in stelen.transform)
            {
                if (origin.name == ("LA_origin"))
                {

                    if (origin.childCount != 0)
                    {
                        foreach (Transform child in origin)
                        {

                            DestroyImmediate(child.gameObject);

                        }
                    }
                }
            }
        }
    }

    void Reset(GameObject prefabObjectOne)
    {

        Clear();

        stelen.SetSpecificPrefab(prefabObjectOne);

    }

    void Reset(GameObject prefabObjectOne, GameObject prefabObjectTwo)
    {

        Clear();

        stelen.SetSpecificPrefabs(prefabObjectOne, prefabObjectTwo);

    }



}
