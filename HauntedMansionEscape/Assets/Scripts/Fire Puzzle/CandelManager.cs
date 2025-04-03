using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum BurningState
{
    UNBURNED,
    BURNING,
    BURNED,
    EXTINGUISH,
}


public class CandelManager : MonoBehaviour
{

    public Material unburnedMaterial;
    public Material BurningMaterial;
    public Material BurnedMaterial;
    public GameObject candleFlame;
    public BurningState State;
    public BurningState requiredState;

    [HideInInspector]
    public bool isSatisfied;


    // Start is called before the first frame update
    void Start()
    {
        UpdateBurning();
        isSatisfied = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (State == requiredState || (requiredState == BurningState.UNBURNED && State == BurningState.BURNED))
        {
            isSatisfied = true;
        }
        else
        {
            isSatisfied = false;
        }
    }

    public void ChangeState(BurningState newState)
    {
        if (State != newState)
        {
            State = newState;
            UpdateBurning();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger detected with: " + other.gameObject.name);

        CandelManager otherCandle = other.gameObject.GetComponent<CandelManager>();

        if (otherCandle != null)
        {
            Debug.Log("I FOUND THE WICK!");
        }

        if (otherCandle != null && otherCandle.State == BurningState.BURNING && State != BurningState.BURNING && State != BurningState.EXTINGUISH)
        {
            Debug.Log("Changing state to BURNING.");
            ChangeState(BurningState.BURNING);
        }

        if (otherCandle != null && otherCandle.State == BurningState.EXTINGUISH)
        {
            Debug.Log("Changing state to Unburned.");
            ChangeState(BurningState.BURNED);
        }

    }

    void UpdateBurning()
    {
        switch (State)
        {
            case BurningState.UNBURNED:
                candleFlame.GetComponent<MeshRenderer>().enabled = false;
                break;
            case BurningState.BURNING:
                candleFlame.GetComponent<MeshRenderer>().enabled = true;
                candleFlame.GetComponent<MeshRenderer>().material = BurningMaterial;
                break;
            case BurningState.BURNED:
                candleFlame.GetComponent<MeshRenderer>().enabled = true;
                candleFlame.GetComponent<MeshRenderer>().material = BurnedMaterial;
                break;
            case BurningState.EXTINGUISH:
                candleFlame.GetComponent<MeshRenderer>().enabled = true;
                candleFlame.GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
        }

        Debug.Log($"Material set to {State.ToString()}");

    }
}

