using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(MotionPath))]
[CanEditMultipleObjects]
public class MotionPathEditor : Editor 
{
	
	private SerializedObject path;
	private SerializedProperty controlPoints;
	private SerializedProperty samples;
   // MotionPath motionPath;
	
	private static Vector3 textOffset = Vector3.down * 0.5f;
	private static GUILayoutOption
		buttonWidth = GUILayout.MaxWidth(30),
		indexWidth = GUILayout.MaxWidth(20);
	
	
	
	void OnSceneGUI()
	{


        MotionPath motionPath = (MotionPath)target;
		Handles.matrix = motionPath.transform.localToWorldMatrix;
		//Undo.SetSnapshotTarget(motionPath, "MovePathPoints");
		Undo.RecordObject(motionPath, "MovePathPoints");
		
		GUIStyle controlPointText = new GUIStyle();
		controlPointText.normal.textColor = Color.green;
		controlPointText.fontSize = 20;
		
		GUIStyle lengthText = new GUIStyle();
		lengthText.normal.textColor = Color.cyan;
		lengthText.fontSize = 15;
		
		// Draw the length of the path in the center
		Handles.Label(motionPath.centerPoint + Vector3.up, motionPath.length.ToString(), lengthText);


        Undo.RecordObject(target, "ControlPoints");

        EditorGUI.BeginChangeCheck();

		// Draw the number of the control point and the handle to translate it
		for (int i = 0; i < motionPath.controlPoints.Length; i++)
		{
			if (i == motionPath.controlPoints.Length -1)
			{
				if(!motionPath.looping)
					Handles.Label(motionPath.controlPoints[i] + textOffset, i.ToString(), controlPointText);
			}
			else
				Handles.Label(motionPath.controlPoints[i] + textOffset, i.ToString(), controlPointText);
			
			
			//Vector3 newPos = Handles.FreeMoveHandle(path.controlPoints[i], Quaternion.identity, 0.8f, Vector3.one, Handles.DotCap);
            Vector3 newPos = Handles.DoPositionHandle(motionPath.controlPoints[i], Quaternion.identity);
			// Automatically rebuild the path luts if a point moves
			if (motionPath.controlPoints[i] != newPos)
			{
                motionPath.controlPoints[i] = newPos;
                motionPath.Rebuild();
			}
		}

        EditorGUI.EndChangeCheck();

    }
	
	
	void OnEnable()
	{

       // motionPath = (MotionPath)target;

        ((MotionPath)target).Init();
		
		path = new SerializedObject(target);
		controlPoints = path.FindProperty("controlPoints");
		samples = path.FindProperty("samples");
	}
	
	 
	public override void OnInspectorGUI ()
	{
		path.Update();
		MotionPath pathObject = (MotionPath)target;
		GUILayout.Space(10);
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PropertyField(samples, new GUIContent("Samples Per Span", string.Format("\nTotal Samples = {0}", (pathObject.controlPoints.Length-1) * samples.intValue)));
		
		EditorGUILayout.EndHorizontal();
		GUILayout.Space(20);
		
		
		GUILayout.Label("Path Points");
		// First row add button to for begining of path
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("", indexWidth);
		if (GUILayout.Button("+", buttonWidth))
		{
			Vector3 start = controlPoints.GetArrayElementAtIndex(0).vector3Value;
			Vector3 end = controlPoints.GetArrayElementAtIndex(1).vector3Value;
			Vector3 norm = (start - end).normalized;
			controlPoints.InsertArrayElementAtIndex(0);
			controlPoints.GetArrayElementAtIndex(0).vector3Value = start + norm;
		}
		EditorGUILayout.EndHorizontal();
		
		int stopIndex = controlPoints.arraySize -1;
		for (int i = 0; i < controlPoints.arraySize; i++)
		{
			SerializedProperty
				point = controlPoints.GetArrayElementAtIndex(i);
			EditorGUILayout.BeginHorizontal();
			
			EditorGUILayout.LabelField(i.ToString(), indexWidth);
			if (GUILayout.Button("X", buttonWidth))
			{
				if (controlPoints.arraySize < 3)
					break;
				controlPoints.DeleteArrayElementAtIndex(i);
				if (i == stopIndex)
					break;
			}
			
			EditorGUILayout.PropertyField(point, GUIContent.none);
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("", indexWidth);
			if (GUILayout.Button("+", buttonWidth))
			{
				Vector3 start = controlPoints.GetArrayElementAtIndex(i).vector3Value;
				if (i == controlPoints.arraySize -1)
				{
					Vector3 pre = controlPoints.GetArrayElementAtIndex(i-1).vector3Value;
					Vector3 norm = (start - pre).normalized;
					controlPoints.InsertArrayElementAtIndex(i+1);
					controlPoints.GetArrayElementAtIndex(i+1).vector3Value = start + norm;
				}
				else
				{
					Vector3 end = controlPoints.GetArrayElementAtIndex(i+1).vector3Value;
					Vector3 newPoint = Vector3.Lerp(start, end, 0.5f);
					controlPoints.InsertArrayElementAtIndex(i+1);
					controlPoints.GetArrayElementAtIndex(i+1).vector3Value = newPoint;
				}
			}
			EditorGUILayout.EndHorizontal();
		}
		
		
		if (!pathObject.looping)
		{
			GUILayout.Space(5);
			if(GUILayout.Button("Make Loop"))
			{
				int i = controlPoints.arraySize-1;
				controlPoints.InsertArrayElementAtIndex(i);
				controlPoints.GetArrayElementAtIndex(i+1).vector3Value = controlPoints.GetArrayElementAtIndex(0).vector3Value;
			}
		}
		
		
		if(path.ApplyModifiedProperties())
			pathObject.Rebuild();

        serializedObject.ApplyModifiedProperties();
	}
}