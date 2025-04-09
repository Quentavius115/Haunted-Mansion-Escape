using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public enum JarContents
{
    EMPTY,
    COPPER_CHLORIDE_BLUE,
    COPPER_SULFATE_GREEN,
    STRONTIUM_CHLORIDE_YELLOW,
    SODIUM_CHLORIDE_RED,
    MAGNESIUM_SULFATE_WHITE

}

public class JarScript : MonoBehaviour
{
    public JarState State { get; private set; }
    public JarContents content;

    public GameObject containerDust;
    public GameObject dust;
    public Transform lidSnapPoint;

    private BurningState fireColor;
    private Color chemecialColor;
    private bool pouring;

    // Socket Help Note https://learn.unity.com/tutorial/xr-interaction-toolkit-working-with-the-socket-interactor
    // Its out of date but IDE will update it and make the right call

    // Start is called before the first frame update
    void Start()
    {
        // sets the chemmical type and changes the color of the dust in the jar to match
        SetChemical(content);
        containerDust.GetComponent<MeshRenderer>().material.color = chemecialColor;

        // Adds Listeners
        XRSocketInteractor socket = GetComponent<XRSocketInteractor>();
        socket.selectEntered.AddListener(OnItemSnapped);
        socket.selectExited.AddListener(OnItemRemoved);
    }

    // Update is called once per frame
    void Update()
    {
        CheckRotation();
    }


    // Snapping Logic
    void OnItemSnapped(SelectEnterEventArgs arg0)
    {
        //Trying to fix object not fully snapping issue
        // args.interactableObject.transform.GetComponent<Collider>().enabled = false;
        UpdateState(JarState.CLOSED);
    }

    void OnItemRemoved(SelectExitEventArgs arg0)
    {
        // args.interactableObject.transform.GetComponent<Collider>().enabled = true;
        UpdateState(JarState.OPEN);
    }

    void SpawnChemical()
    {
        // spawn x dust
        for (int i = 0; i < 5; i++)
        {
            // small area for dust to spawn from the top of the jar
            Vector3 randomOffset = new(
                Random.Range(-0.05f, 0.05f),
                Random.Range(-0.05f, 0.05f) + .15f,
                Random.Range(-0.05f, 0.05f)
            );

            // World offset means it offsets with the actual object rotation
            Vector3 worldOffset = transform.TransformDirection(randomOffset);
            GameObject dustSpeck = Instantiate(dust, transform.position + worldOffset, transform.rotation);
            PowderScript chemicalPowder = dustSpeck.GetComponent<PowderScript>();

            // each chemical dust parrticle can interact with the fire
            chemicalPowder.SetChemical(fireColor, chemecialColor);
        }
    }

    // checks if its upside down, could update it to see if it intersects with player hand so not constantly updating
    void CheckRotation()
    {
        Vector3 rotation = transform.eulerAngles;

        // Upside down is between 120 and 240 for both x and z
        if (State != JarState.CLOSED)
        {
            if ((rotation.x > 120 && rotation.x < 240) || (rotation.z > 120 && rotation.z < 240))
            {
                UpdateState(JarState.POURING);
            }
            else
            {
                UpdateState(JarState.OPEN);
            }
        }
    }

    // State Machine for the jar, currently only open and pour works, will implement lid with snapping
    void UpdateState(JarState state)
    {
        State = state;
        switch (state)
        {
            case JarState.EMPTY:
                CancelInvoke(nameof(SpawnChemical));
                pouring = false;
                break;
            case JarState.IDLE:
                CancelInvoke(nameof(SpawnChemical));
                pouring = false;
                break;
            case JarState.CLOSED:
                // Intended to be so you can hold the jar upside down with lid and it wont pour, mainly to show state machine works.
                CancelInvoke(nameof(SpawnChemical));
                pouring = false;
                break;
            case JarState.OPEN:
                CancelInvoke(nameof(SpawnChemical));
                pouring = false;
                break;
            case JarState.POURING:
                // turns pouring on once instead of many times
                if (pouring == false)
                {
                    InvokeRepeating(nameof(SpawnChemical), .05f, .05f);
                }
                pouring = true;
                break;

        }
    }

    // This is just to select what each chemical is in each jar and tells the particle what it should have, doesnt actually switch in gameplay
    void SetChemical(JarContents chemical)
    {
        // Chemical determines what color the fire should be
        switch (chemical)
        {
            case JarContents.EMPTY:
                break;
            case JarContents.COPPER_CHLORIDE_BLUE:
                fireColor = BurningState.BURNING_BLUE;
                chemecialColor = Color.blue;
                break;
            case JarContents.COPPER_SULFATE_GREEN:
                fireColor = BurningState.BURNING_GREEN;
                chemecialColor = Color.green;
                break;
            case JarContents.STRONTIUM_CHLORIDE_YELLOW:
                fireColor = BurningState.BURNING_YELLOW;
                chemecialColor = Color.yellow;
                break;
            case JarContents.SODIUM_CHLORIDE_RED:
                fireColor = BurningState.BURNING_RED;
                chemecialColor = Color.red;
                break;
            case JarContents.MAGNESIUM_SULFATE_WHITE:
                fireColor = BurningState.BURNING_WHITE;
                chemecialColor = Color.white;
                break;
        }
    }
}
