using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingsMovement : MonoBehaviour
{
    public KingState State { get; private set; }
    public GameObject King;

    // Start is called before the first frame update
    void Start()
    {
        stateEnterMethods = new()
        {
            [State.IDLE] = StateEnter_Idle,
            [State._MOVING] = StateEnter_MOVING,
            [State.ERROR] = StateEnter_Error,
        };
        stateStayMethods = new()
        {
            [State.IDLE] = StateStay_Idle,
            [State._MOVING] = StateStay_MOVING,
            [State.ERROR] = StateStay_Error,
        };
        LastState = State.None
        State = State.IDLE;
    }

    void Update()
    {
        if (lastState == State.NONE)
        {
            return;
        }
        stateStayMethods[State]();
        lastState = State.NONE;
    }

    private void ChangeState(State newState)
    {
        if (State != newState)
        {
            State = newState;
            stateEnterMethods[newState]();
        }
    }

    private void StateEnter_Idle() 
    { 
        // Do Nothing
    }
    private void StateEnter_MOVING()
    {
        // Move King
    }
    private void StateEnter_Error()
    {
        ChangeState(State.IDLE);
    }
#endregion

    #region State Stay Methods
    private void StateStay_Idle()
    {
        // do something
    }
    private void StateStay_MOVING()
    {
        if (lastState == State._MOVING)
        {
            ChangeState(State._MOVING);
        }
        else
        {
            ChangeState(State.ERROR);
        }
    }
    
    private void StateStay_Error()
    {
        // Something
    }
}
