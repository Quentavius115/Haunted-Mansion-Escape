using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class CandleListener : MonoBehaviour
{
    public GameObject checkCube;
    public List<CandelManager> allCandles = new List<CandelManager>();
    public Transform BookSnapPoint;
    private bool BookInPlace;

    // Start is called before the first frame update
    void Start()
    {
        allCandles.AddRange(FindObjectsOfType<CandelManager>());
        checkCube.GetComponent<MeshRenderer>().material.color = Color.red;
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
        checkCube.GetComponent<MeshRenderer>().material.color = Color.yellow;
        args.interactableObject.transform.GetComponent<Collider>().enabled = false;
        Debug.Log("Book In Place");
    }


    void CheckAllConditionsSatisfied()
    {

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
            checkCube.GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }
}
