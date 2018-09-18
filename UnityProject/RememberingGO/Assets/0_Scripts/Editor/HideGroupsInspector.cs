using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof(HideGroups))]
public class HideGroupsInspector : Editor {

    public override void OnInspectorGUI()
    {

        EditorGUILayout.HelpBox("Voodat je met deze tool aan de slag gaat moet je eerst zorgen dat er 'HideGroups' zijn. Deze kan je maken of gebruiken in het tag menu hierboven. Zorg ervoor dat de juiste objecten de juiste HideGroup + nummer krijgen. Standaard zijn er 3 hidegroups. Als je er meer aanmaakt geeft dat dan hieronder aan: ", MessageType.None);

        HideGroups hideGroups = target as HideGroups;

        serializedObject.Update();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("numberOfHideTags"), true);

        EditorGUILayout.HelpBox("Een HideGroup ziet er altijd zo uit: HideGroup + nummer. Dus bijvoorbeeld: HideGroup4", MessageType.None);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("GroupList"), true);

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
        EditorGUILayout.PropertyField(serializedObject.FindProperty("whatGroupToHide"), true);

        if (GUILayout.Button("Hide Group"))
        {

            hideGroups.HideGroup(hideGroups.whatGroupToHide);

        }

        EditorGUILayout.Space();


        EditorGUILayout.LabelField("Selecteer Group om te showen: ");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("whatGroupToShow"), true);

        if (GUILayout.Button("Show Group"))
        {

            hideGroups.ShowGroup(hideGroups.whatGroupToShow);

        }

        serializedObject.ApplyModifiedProperties();
    }

}

