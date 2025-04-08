using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public enum BurningState
{
    UNBURNED,
    BURNING,
    BURNING_BLUE,   // Copper Chloride
    BURNING_GREEN,  // Copper Sulfate
    BURNING_RED,    // Strontium Chloride
    BURNING_YELLOW, // Sodium Chloride
    BURNING_WHITE,  // Magnesium Sulfate
    BURNED,
    REQUIRED_NONE,  // Only used for required state
    EXTINGUISH,
}





public class CandelManager : MonoBehaviour
{
    [Header("Flame Materials")]

    public GameObject candleFlame;
    public BurningState State;
    public BurningState requiredState;

    [HideInInspector]
    public bool isSatisfied;
    [HideInInspector]
    public bool onFire;


    // Start is called before the first frame update
    void Start()
    {
        UpdateBurning();
        isSatisfied = false;
    }


    // Update is called once per frame
    void Update()
    {

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
        CandelManager otherCandle = other.gameObject.GetComponent<CandelManager>();
        //== BurningState.BURNING && State != BurningState.BURNING && State != BurningState.EXTINGUISH
        if (otherCandle != null && otherCandle.onFire && !onFire)
        {
            ChangeState(otherCandle.State);
        }

        if (otherCandle != null && otherCandle.State == BurningState.EXTINGUISH)
        {
            ChangeState(BurningState.BURNED);
        }

    }

    void UpdateBurning()
    {
        if (requiredState == BurningState.UNBURNED || requiredState == BurningState.BURNED)
        {
            if (State == BurningState.UNBURNED || State == BurningState.BURNED)
            {
                isSatisfied = true;
            }
        }
        else if (State == requiredState || requiredState == BurningState.REQUIRED_NONE)
        {
            isSatisfied = true;
        }
        else
        {
            isSatisfied = false;
        }

        switch (State)
        {
            case BurningState.UNBURNED:
                onFire = false;
                candleFlame.GetComponent<MeshRenderer>().enabled = false;
                break;
            case BurningState.BURNING:
                onFire = true;
                candleFlame.GetComponent<MeshRenderer>().enabled = true;
                candleFlame.GetComponent<MeshRenderer>().material.color = Color.magenta;
                break;
            case BurningState.BURNING_BLUE:
                onFire = true;
                candleFlame.GetComponent<MeshRenderer>().enabled = true;
                candleFlame.GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            case BurningState.BURNING_GREEN:
                onFire = true;
                candleFlame.GetComponent<MeshRenderer>().enabled = true;
                candleFlame.GetComponent<MeshRenderer>().material.color = Color.green;
                break;
            case BurningState.BURNING_YELLOW:
                onFire = true;
                candleFlame.GetComponent<MeshRenderer>().enabled = true;
                candleFlame.GetComponent<MeshRenderer>().material.color = Color.yellow;
                break;
            case BurningState.BURNING_RED:
                onFire = true;
                candleFlame.GetComponent<MeshRenderer>().enabled = true;
                candleFlame.GetComponent<MeshRenderer>().material.color = Color.red;
                break;
            case BurningState.BURNING_WHITE:
                onFire = true;
                candleFlame.GetComponent<MeshRenderer>().enabled = true;
                candleFlame.GetComponent<MeshRenderer>().material.color = Color.white;
                break;
            case BurningState.BURNED:
                onFire = false;
                candleFlame.GetComponent<MeshRenderer>().enabled = true;
                candleFlame.GetComponent<MeshRenderer>().material.color = Color.black;
                break;
            case BurningState.EXTINGUISH:
                onFire = false;
                candleFlame.GetComponent<MeshRenderer>().enabled = true;
                candleFlame.GetComponent<MeshRenderer>().material.color = Color.cyan;
                break;
        }
    }


    // Might Implement Later if needed
    //
    // Dictionary<BurningState, Color> flameColors = new(){
    //     {BurningState.BURNING, Color.magenta},
    //     {BurningState.BURNING_BLUE, Color.blue},
    //     {BurningState.BURNING_GREEN, Color.green},
    //     {BurningState.BURNING_RED, Color.red},
    //     {BurningState.BURNING_YELLOW, Color.yellow},
    //     {BurningState.BURNING_WHITE, Color.white},
    //     {BurningState.BURNED, Color.black},
    //     {BurningState.EXTINGUISH, Color.cyan},
    // };

    // 
    // void setBurning(bool fireStatus, Color color)
    // {
    //     var meshrender = candleFlame.GetComponent<MeshRenderer>();
    //     onFire = fireStatus;
    //     meshrender.enabled = fireStatus;
    //     meshrender.material.color = color;
    // }
}


