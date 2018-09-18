using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ShowTime))]
public class ShowTimeEditor : Editor {

    ShowTime timeDisplay;
    SerializedProperty positionInWorld, positionOnPath, sliderSize;

    void OnEnable()
    {

        timeDisplay = target as ShowTime;
        positionInWorld = serializedObject.FindProperty("pos");
        positionOnPath = serializedObject.FindProperty("positionOnPath");
        sliderSize = serializedObject.FindProperty("diskSize");

    }

    void OnSceneGUI()
    {

        serializedObject.Update();

        //diskSize.floatValue = Handles.RadiusHandle(Quaternion.identity, positionOnPath.vector3Value, diskSize.floatValue);

        float scaleFactor = HandleUtility.GetHandleSize(positionInWorld.vector3Value);

        positionOnPath.floatValue = Handles.ScaleSlider(positionOnPath.floatValue, positionInWorld.vector3Value, Vector3.forward, Quaternion.identity, scaleFactor, 0f);

        Handles.color = Color.red;
        Handles.Label(positionInWorld.vector3Value + Vector3.forward * scaleFactor * 2, "Position On Path: " + positionOnPath.floatValue.ToString("N3"));

        //Vector3 sliderPos = Handles.Slider(positionInWorld.vector3Value, Vector3.forward);

        

        serializedObject.ApplyModifiedProperties();

    }


}
