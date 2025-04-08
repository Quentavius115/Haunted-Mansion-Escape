using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActivateKing : MonoBehaviour
{
    int checkmate = 0;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    // Start is called before the first frame update
    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        Debug.Log(checkmate);
    }

    // Update is called once per frame
    void Update()
    {
        if(checkmate == 3){
            grabInteractable.enabled = true;
        }
        else{
            grabInteractable.enabled = false;
        }
        Debug.Log(checkmate);
    }

    public void Increase(){
        checkmate += 1;
    }

    public void Decrease(){
        checkmate -= 1;
    }
}
