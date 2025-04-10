using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State = PianoState;

public class PianoPuzzle : MonoBehaviour
{
    public PianoState State { get; private set; }
    private Note lastNote;
    public AudioSource audioSrc;

    private Dictionary<State, Action> stateEnterMethods;
    private Dictionary<State, Action> stateStayMethods;

    void Start()
    {
        stateEnterMethods = new()
        {
            [State.IDLE] = StateEnter_Idle,
            [State._FIRST_KEY_E] = StateEnter_FIRST_KEY_E,
            [State._SECOND_KEY_G] = StateEnter_SECOND_KEY_G,
            [State._THIRD_KEY_A] = StateEnter_THIRD_KEY_A,
            [State._FINAL_KEY_F] = StateEnter_FINAL_KEY_F,
            [State.ERROR] = StateEnter_Error,
        };
        stateStayMethods = new()
        {
            [State.IDLE] = StateStay_Idle,
            [State._FIRST_KEY_E] = StateStay_FIRST_KEY_E,
            [State._SECOND_KEY_G] = StateStay_SECOND_KEY_G,
            [State._THIRD_KEY_A] = StateStay_THIRD_KEY_A,
            [State._FINAL_KEY_F] = StateStay_FINAL_KEY_F,
            [State.ERROR] = StateStay_Error,
        };
        lastNote = Note.NONE;
        State = State.IDLE;
        audioSrc = GetComponent<AudioSource>();
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
            Debug.Log("NewState");
            State = newState;
            stateEnterMethods[newState]();
        }
    }

    #region State Methods
    #region State Enter Methods
    private void StateEnter_Idle() 
    { 
        // Do Nothing
    }
    private void StateEnter_FIRST_KEY_E()
    {
        SoundManager.Play(SoundType.E4);
    }
    private void StateEnter_SECOND_KEY_G()
    {
        SoundManager.Play(SoundType.G4);
    }
    private void StateEnter_THIRD_KEY_A()
    {
        SoundManager.Play(SoundType.A5);
    }
    private void StateEnter_FINAL_KEY_F()
    {
        SoundManager.Play(SoundType.FINISHED);
    }
    private void StateEnter_Error()
    {
        ChangeState(State.IDLE);
    }
    #endregion

    #region State Stay Methods
    private void StateStay_Idle()
    {
        if (lastNote == Note.E4)
        {
            ChangeState(State._FIRST_KEY_E);
        }
        else
        {
            ChangeState(State.ERROR);
        }
    }
    private void StateStay_FIRST_KEY_E()
    {
        if (lastNote == Note.G4)
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
        if (lastNote == Note.A5)
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
        if (lastNote == Note.F4)
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
        
    }
    private void StateStay_Error()
    {
        // Do Nothing
    }
    #endregion
    #endregion
    public void Press(Note note)
    {
        Debug.Log("inPress");
        lastNote = note;
    }
}