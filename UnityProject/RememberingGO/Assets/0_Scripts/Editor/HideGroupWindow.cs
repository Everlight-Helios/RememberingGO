using UnityEngine;
using UnityEditor;
using System.Collections;

public class HideGroupWindow : EditorWindow {

    HideGroups hideGroups;

    private bool showHideObjects;

    [MenuItem("Tools/HideGoups/HideGroupsWindow")]
    static void OpenWindow()
    {

        HideGroupWindow window = (HideGroupWindow)GetWindow(typeof(HideGroupWindow));
        window.Show();

    }

    void OnGUI()
    {

        if (hideGroups == null)
        {

            if (GameObject.Find("HideGroups"))
            {
                hideGroups = GameObject.Find("HideGroups").GetComponent<HideGroups>();
            }
            else
            {
                return;
            }
            
        }
     
        if(hideGroups.showHideGroups = EditorGUILayout.Foldout(hideGroups.showHideGroups, "Show all HideGroupObjects: "))
        {

            EditorGUILayout.HelpBox("Voodat je met deze tool aan de slag gaat moet je eerst zorgen dat er 'HideGroups' zijn. Deze kan je maken of gebruiken in het tag menu hierboven. Zorg ervoor dat de juiste objecten de juiste HideGroup + nummer krijgen. Standaard zijn er 3 hidegroups. Als je er meer aanmaakt geeft dat dan hieronder aan: ", MessageType.None);


            hideGroups.numberOfHideTags = EditorGUILayout.IntField(hideGroups.numberOfHideTags);

            EditorGUILayout.HelpBox("Een HideGroup-tag ziet er altijd zo uit: HideGroup + nummer. Dus bijvoorbeeld: HideGroup4", MessageType.None);

            EditorGUILayout.HelpBox("Dit zijn de huidige HideGroups in de scene: ", MessageType.None);
            for (int i = 0; i < hideGroups.GroupList.Length; i++)
            {
                if(hideGroups.GroupList[i] != null)
                    EditorGUILayout.ObjectField(hideGroups.GroupList[i].hideGroups[0], typeof(GameObject), true);

            }

            EditorGUILayout.Space();

            EditorGUILayout.HelpBox("Met deze tool kan je automatisch alle HideGroups ophalen.", MessageType.None);
            EditorGUILayout.HelpBox("Zorg ervoor dat de objecten allemaal de juiste tags hebben.", MessageType.None);

            if (GUILayout.Button("Setup! (Always press me before Populate!)"))
            {

                hideGroups.Clear();

            }


            if (GUILayout.Button("Populate"))
            {

                hideGroups.Populate();

            }

            EditorGUILayout.Space();

        }
        
        

        EditorGUILayout.HelpBox("Hier kan je alle HideGroups Hiden of Showen", MessageType.None);


        if (GUILayout.Button("Hide Objects"))
        {

            hideGroups.HideObjects();

        }

        if (GUILayout.Button("Show Objects"))
        {

            hideGroups.ShowObjects();

        }

        EditorGUILayout.Space();

        EditorGUILayout.HelpBox("Hier kan je een specifieke HideGroup Hiden of Showen", MessageType.None);

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Selecteer Group om te hiden: ");

        hideGroups.whatGroupToHide = EditorGUILayout.IntField(hideGroups.whatGroupToHide);

        if (GUILayout.Button("Hide Group"))
        {

            hideGroups.HideGroup(hideGroups.whatGroupToHide);

        }

        EditorGUILayout.Space();


        EditorGUILayout.LabelField("Selecteer Group om te showen: ");
        hideGroups.whatGroupToShow = EditorGUILayout.IntField(hideGroups.whatGroupToShow);

        if (GUILayout.Button("Show Group"))
        {

            hideGroups.ShowGroup(hideGroups.whatGroupToShow);

        }


    }

}
