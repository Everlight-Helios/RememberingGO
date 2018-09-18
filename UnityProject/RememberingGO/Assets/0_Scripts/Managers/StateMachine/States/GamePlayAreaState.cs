using UnityEngine;
using System.Collections;

public class GamePlayAreaState : ICurrentState
{

    private readonly StateMachine m_StateMachine;

    public GamePlayAreaState(StateMachine stateMachine)
    {

        m_StateMachine = stateMachine;

    }

    public void UpdateState()
    {

    }

    public void ToNewState()
    {

    }

}