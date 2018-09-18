using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof(StateMachine))]
public class StateMachineDisplay : Editor {

    private enum State { Introstate, GamePlayAreaState }
    private State currentState = State.Introstate;

    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();

        serializedObject.Update();

        StateMachine stateMachine = (StateMachine)target;

        currentState = (State)EditorGUILayout.EnumPopup("Current State", currentState);

        States state = new States();
        state.introState = new IntroState(stateMachine);
        EditorGUILayout.LabelField(state.introState.ToString());

        serializedObject.ApplyModifiedProperties();

    }

}
