using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class CandleListener : MonoBehaviour
{
    public GameObject cursedBook;
    public List<CandelManager> allCandles = new List<CandelManager>();
    public Transform BookSnapPoint;
    private bool BookInPlace;
    public AudioSource Connected;
    public AudioSource OpeningDoor;
    public AudioSource FinaleTune;
    public GameObject endDoor;





    // Start is called before the first frame update
    void Start()
    {
        FinaleTune.volume = .05f;
        allCandles.AddRange(FindObjectsOfType<CandelManager>());

    }

    // Update is called once per frame
    void Update()
    {
        CheckAllConditionsSatisfied();
    }


    // Snapping Logic
    void OnEnable()
    {
        var socket = GetComponent<XRSocketInteractor>();
        socket.selectEntered.AddListener(OnSelectEntered);

    }

    void OnSelectEntered(SelectEnterEventArgs args)
    {
        BookInPlace = true;
        // Once the book is in place it cant be moved again
        args.interactableObject.transform.GetComponent<Collider>().enabled = false;
        Connected.Play();
    }


    void CheckAllConditionsSatisfied()
    {
        var door = endDoor.GetComponent<EndDoorScript>();

        // Add logic for checking if book is satisfied by being in correct snapped place 
        bool allStateSatisfied = true;


        foreach (var candle in allCandles)
        {
            if (candle.isSatisfied != true)
            {
                allStateSatisfied = false;
                break;
            }

            // Stop checking candles if the book isnt in place
            if (BookInPlace == false)
            {
                allStateSatisfied = false;
                break;
            }
        }

        if (allStateSatisfied)
        {
            // Plays sound only if door isnt open
            if (door.State != DoorState.OPEN)
            {
                foreach (var candle in allCandles)
                {
                    candle.ChangeState(BurningState.BURNED);
                }


                door.UpdateState(DoorState.OPEN);
                OpeningDoor.Play();
                FinaleTune.Play();
                Destroy(cursedBook);
            }

        }
    }
}
