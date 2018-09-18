using UnityEngine;
using RememberingManagers;
using System.Collections;

public class IntroState : ICurrentState
{

    private readonly StateMachine m_StateMachine;

    private float m_currentTime, m_waitTime;
    private ColorManager colorManager;

    private enum States:int { Black, Travel, EndOfIntro }
    private States currentState = States.Black;

    private SmoothLerp player;
    private ColorPoint colorSpot0;
    private AudioManager audioManager;
    private AudioClip introClip;


    public IntroState(StateMachine stateMachine)
    {

        player = stateMachine.Player.GetComponent<SmoothLerp>();

        colorSpot0 = stateMachine.colorPoint0;

        audioManager = stateMachine.audioManager;


        introClip = stateMachine.introClip;

    }

    public void UpdateState()
    {

        RunStates();

    }

    public void ToNewState()
    {

    }

    void RunStates()
    {

        switch (currentState)
        {

            case States.Black:

                /*
                colorSpot0.SendMyColor();
                
                audioManager.SetAmbient(introClip);
                //DOF Settings
                currentState = States.Travel;*/
                break;

            case States.Travel:

                

                break;

            case States.EndOfIntro:
               
                break;

            default:
               
                break;

        }
    }
}
