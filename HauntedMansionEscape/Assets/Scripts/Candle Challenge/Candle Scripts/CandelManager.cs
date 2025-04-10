using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs;


public class CandelManager : MonoBehaviour
{
    [Header("Flame Materials")]
    public BurningState State;
    public BurningState requiredState;
    public ParticleSystem flameParticles;

    [Header("Fire Audio")]
    public AudioSource ignite;
    public AudioSource reaction;
    public AudioSource idle;

    [HideInInspector]
    public bool isSatisfied;
    [HideInInspector]
    public bool onFire;


    // Start is called before the first frame update
    void Start()
    {
        isSatisfied = false;
        UpdateBurning();

        idle.volume = .1f;
        ignite.volume = .2f;
        reaction.volume = .2f;

        flameParticles = GetComponent<ParticleSystem>();
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
        PowderScript dust = other.gameObject.GetComponent<PowderScript>();


        // Candle lighting other candle
        if (otherCandle != null && otherCandle.onFire && !onFire)
        {
            ChangeState(otherCandle.State);
            ignite.Play();
            idle.Play();
        }
        else if (otherCandle != null && otherCandle.State == BurningState.EXTINGUISH)
        {
            ChangeState(BurningState.BURNED);
        }

        if (dust != null)
        {
            if (onFire == true)
            {
                ChangeState(dust.fireState);
            }
        }
    }

    // Sets the fire color and makes it visible if not already
    // Particle Info https://docs.unity3d.com/6000.0/Documentation/ScriptReference/ParticleSystem.MainModule.html
    void SetFlameColor(Color color)
    {
        // random audio here of a flame changing, maybe popping or something. This will use the example from class, we can do the same for lighting/thunder
        onFire = true;
        ParticleSystem.MainModule main = flameParticles.main;
        main.startColor = color;
        flameParticles.Play();
        reaction.Play();

        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<MeshRenderer>().material.color = color;
    }

    void UpdateBurning()
    {
        // Candle collision check, also seeing if it is satisfied with itself only when burning is updated, that way your not always calling the check

        if (State == requiredState || requiredState == BurningState.REQUIRED_NONE)
        {
            isSatisfied = true;
        }
        else
        {
            isSatisfied = false;
        }

        switch (State)
        {
            // Not on fire
            case BurningState.UNBURNED:
                onFire = false;
                idle.Stop();
                flameParticles.Stop();
                GetComponent<MeshRenderer>().enabled = false;
                break;
            case BurningState.BURNED:
                onFire = false;
                idle.Stop();
                flameParticles.Stop();
                // Play audio here of a sizzling out
                GetComponent<MeshRenderer>().enabled = false;
                break;

            // On Fire
            case BurningState.EXTINGUISH:
                idle.Stop();
                SetFlameColor(Color.black);
                break;
            case BurningState.BURNING:
                SetFlameColor(Color.red);
                break;
            case BurningState.BURNING_BLUE:
                SetFlameColor(Color.blue);
                break;
            case BurningState.BURNING_GREEN:
                SetFlameColor(Color.green);
                break;
            case BurningState.BURNING_YELLOW:
                onFire = true;
                SetFlameColor(Color.yellow);
                break;
            case BurningState.BURNING_RED:
                SetFlameColor(Color.magenta);
                break;
            case BurningState.BURNING_WHITE:
                SetFlameColor(Color.white);
                break;
        }
    }
}


