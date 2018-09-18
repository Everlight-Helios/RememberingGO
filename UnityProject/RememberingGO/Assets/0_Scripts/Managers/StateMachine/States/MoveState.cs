using UnityEngine;
using System.Collections;

public class MoveState : ICurrentState
{

    private readonly StateMachine m_StateMachine;

    public MoveState(StateMachine stateMachine)
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
