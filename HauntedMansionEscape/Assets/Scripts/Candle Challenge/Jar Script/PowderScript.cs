using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;



public class PowderScript : MonoBehaviour
{
    public JarContents content;
    public BurningState fireState;
    private Color chemicalColor;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DestroyObject), .3f); // Deletes itself after .3 seconds to avoid lag
    }



    // Update is called once per frame
    void Update()
    {

    }

    // Function is required to use invoke
    void DestroyObject()
    {
        Destroy(gameObject);
    }

    // updates the chemical states, might look into doing it once instead of once for each instance, but it works
    // well enough for now and im tired. Ill try to come back to this.
    public void SetChemical(BurningState color, Color dustColor)
    {
        GetComponent<MeshRenderer>().material.color = chemicalColor;
        fireState = color;
        chemicalColor = dustColor;
    }
}
