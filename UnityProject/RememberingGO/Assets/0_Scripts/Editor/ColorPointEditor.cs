using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof(ColorPoint))]
[CanEditMultipleObjects]
public class ColorPointEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ColorPoint colorPoint = target as ColorPoint;
        

        if(GUILayout.Button("Set my Color to skybox!"))
        {

            colorPoint.SetMyColor();

        }

    }
}
