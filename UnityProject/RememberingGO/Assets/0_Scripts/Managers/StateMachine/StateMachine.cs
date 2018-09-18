using UnityEngine;
using RememberingManagers;
using System.Collections;

public class StateMachine : MonoBehaviour {

    public ICurrentState currentState;
    public AudioClip introClip;

    public AudioManager audioManager;
    public GameObject Player;
    public ColorPoint colorPoint0;

    private States states;

    void Awake()
    {

        DeclareStates();

    }

	void Start () {

        currentState = states.introState;

	}
	
	void Update () {

        currentState.UpdateState();
	
	}

    public void DeclareStates()
    {

        states = new States();
        states.introState = new IntroState(this);
        states.gamePlayAreaState = new GamePlayAreaState(this);
        states.moveState = new MoveState(this);

    }

}

public struct States
{

    public IntroState introState;
    public GamePlayAreaState gamePlayAreaState;
    public MoveState moveState;

}