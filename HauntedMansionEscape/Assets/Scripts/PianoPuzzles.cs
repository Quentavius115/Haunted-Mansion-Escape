using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = PianoState;

public class Puzzle_2 : MonoBehaviour
{
    public PianoState State { get; private set; }
    private Note lastNote;

    private Dictionary<State, Action> stateEnterMethods;
    private Dictionary<State, Action> stateStayMethods;

    void Start()
    {
        stateEnterMethods = new()
        {
            [State.IDLE] = StateEnter_Idle,
            [State._FIRST_KEY_E] = StateEnter_First_Key_E,
            [State._SECOND_KEY_G] = StateEnter_SECOND_KEY_G,
            [State._THIRD_KEY_A] = StateEnter_THIRD_KEY_A,
            [State.FINALKEYF] = StateEnter_FINAL_KEY_F,
            [State.ERROR] = StateEnter_Error,
        };
        stateStayMethods = new()
        {
            [State.IDLE] = StateStay_Idle,
            [State._FIRST_KEY_E] = StateStay_First_Key_E,
            [State._SECOND_KEY_G] = StateStay_SECOND_KEY_G,
            [State._THIRD_KEY_A] = StateStay_THIRD_KEY_A,
            [State.FINALKEYF] = StateStay_FINAL_KEY_F,
            [State.ERROR] = StateStay_Error,
        };
        lastNote = Note.NONE;
        State = State.IDLE;
    }

    void Update()
    {
        if (lastNote == Note.NONE)
        {
            return;
        }
        stateStayMethods[State]();
        lastNote = Note.NONE;
    }

    private void ChangeState(State newState)
    {
        if (State != newState)
        {
            State = newState;
            stateEnterMethods[newState]();
        }
    }

    #region State Methods
    #region State Enter Methods
    private void StateEnter_Idle() { }
    private void StateEnter_First_Key_E()
    {
        // play E
    }
    private void StateEnter_SECOND_KEY_G()
    {
        // play G
    }
    private void StateEnter_THIRD_KEY_A()
    {
        // play A
    }
    private void StateEnter_FINAL_KEY_F()
    {
        // play F
    }
    private void StateEnter_Error()
    {
        SoundManager.Play(SoundType.WRONG);
        ChangeState(State.IDLE);
    }
    #endregion

    #region State Stay Methods
    private void StateStay_Idle()
    {
        // do something
    }
    private void StateStay_First_Key_E()
    {
        if (lastNote == Note._SECOND_KEY_G)
        {
            ChangeState(State._SECOND_KEY_G);
        }
        else
        {
            ChangeState(State.ERROR);
        }
    }
    private void StateStay_SECOND_KEY_G()
    {
        if (lastNote == Note._THIRD_KEY_A)
        {
            ChangeState(State._THIRD_KEY_A);
        }
        else
        {
            ChangeState(State.ERROR);
        }
    }
    private void StateStay_THIRD_KEY_A()
    {
        if (lastNote == Note._FINAL_KEY_F)
        {
            ChangeState(State._FINAL_KEY_F);
        }
        else
        {
            ChangeState(State.ERROR);
        }
    }
    private void StateStay_FINAL_KEY_F()
    {
        // Do Nothing
    }
    private void StateStay_Error()
    {
        // Something
    }
    #endregion
    #endregion
    public void Press(Note note)
    {
        lastNote = note;
    }
}