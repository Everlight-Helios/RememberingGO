using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor (typeof(HideZone))]
public class HideZoneEditor : Editor {

    public override void OnInspectorGUI()
    {

        HideZone hideZone = target as HideZone;


        hideZone.showObjects = GUILayout.Toggle(hideZone.showObjects, "Show Group: ");

        if (hideZone.showObjects)
        {

            hideZone.groupToShow = EditorGUILayout.IntSlider(hideZone.groupToShow, 0, 9);

        }

        hideZone.hideObjects = GUILayout.Toggle(hideZone.hideObjects, "Hide Group: ");

        if (hideZone.hideObjects)
        {

            hideZone.groupToHide = EditorGUILayout.IntSlider(hideZone.groupToHide, 0, 9);

        }

        if(hideZone.player == null)
        {

            if(GUILayout.Button("No player found, press me!"))
            {

                hideZone.Initialize();

            }

        }

        hideZone.hideSpecificObject = GUILayout.Toggle(hideZone.hideSpecificObject, "Should hide specific object?");
		hideZone.drawGizmo = GUILayout.Toggle(hideZone.drawGizmo, "Draw Gizmo?");

        if (hideZone.hideSpecificObject)
        {

            hideZone.specificObjectToHide = EditorGUILayout.ObjectField(hideZone.specificObjectToHide, typeof( GameObject), true) as GameObject;

        }


    }

}
