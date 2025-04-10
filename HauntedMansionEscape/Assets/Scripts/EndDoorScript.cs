using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndDoorScript : MonoBehaviour
{
    public Transform doorPivot;
    public DoorState State;

    // Start is called before the first frame update
    void Start()
    {
        UpdateState(State);
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OpeningAnimation()
    {
        // animate
        UpdateState(DoorState.OPEN);
    }

    public void UpdateState(DoorState newState)
    {
        State = newState;
        switch (State)
        {
            case DoorState.CLOSED:
                doorPivot.localRotation = Quaternion.Euler(0, 180f, 0);
                break;
            case DoorState.OPENING:
                OpeningAnimation();
                break;
            case DoorState.OPEN:
                print("I OPENED");
                doorPivot.localRotation = Quaternion.Euler(0, -90f, 0);
                break;

        }

    }
}
