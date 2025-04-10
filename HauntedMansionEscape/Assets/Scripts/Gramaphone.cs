using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketInteractorScript : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;

    private void Awake()
    {
        socket = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
        socket.selectEntered.AddListener(OnPlacedInSocket);
        socket.selectExited.AddListener(OnRemovedFromSocket);
    }

    private void OnPlacedInSocket(SelectEnterEventArgs args)
    {
        var behavior = args.interactableObject.transform.GetComponent<PlayLore>();
        if (behavior != null)
        {
            behavior.OnPlaced();  // Trigger OnPlaced method on the object
        }
    }

    private void OnRemovedFromSocket(SelectExitEventArgs args){
        var behavior = args.interactableObject.transform.GetComponent<PlayLore>();
        if (behavior != null)
        {
            behavior.OnRemoved();  // Trigger OnPlaced method on the object
        }
    }
}