using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.XR.Interaction.Toolkit;

public class Port : MonoBehaviour
{
    public Pipe holdingPipe;

    XRSocketInteractor socket;

    void Awake() {
        socket = GetComponent<XRSocketInteractor>();
    }

    public void HoldObject() {
        holdingPipe = GetConnectedPipe();
    }

    public void FreeObject() {
        holdingPipe = null;
    }

    public GameObject SocketCheck()
    {
        IXRSelectInteractable objName = socket.GetOldestInteractableSelected();
        return objName.transform.gameObject;
    }

    public Pipe GetConnectedPipe() {
        Pipe connectedPipe = SocketCheck().GetComponent<Pipe>();
        return connectedPipe;
    }
}
