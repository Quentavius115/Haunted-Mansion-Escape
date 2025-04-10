using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActivateKing : MonoBehaviour
{
    public enum KingState
    {
        Inactive,
        Active
    }

    private KingState currentState;
    private int checkmate = 0;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        UpdateState();  // Initialize state
    }

    void Update()
    {
        // Optional: You can show this for debugging purposes
        Debug.Log("Current State: " + currentState + " | Checkmate: " + checkmate);
    }

    public void Increase()
    {
        checkmate++;
        UpdateState();
    }

    public void Decrease()
    {
        checkmate--;
        UpdateState();
    }

    private void UpdateState()
    {
        // Determine the new state based on checkmate value
        KingState newState = (checkmate >= 3) ? KingState.Active : KingState.Inactive;

        // Only act if state changes
        if (newState != currentState)
        {
            currentState = newState;
            ApplyState();
        }
    }

    private void ApplyState()
    {
        switch (currentState)
        {
            case KingState.Inactive:
                grabInteractable.enabled = false;
                break;

            case KingState.Active:
                grabInteractable.enabled = true;
                break;
        }
    }
}