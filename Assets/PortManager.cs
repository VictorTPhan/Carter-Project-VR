using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortManager : MonoBehaviour
{
    private Port[] ports;

    void Awake() {
        ports = FindObjectsOfType<Port>();
    }

    public void RotatePorts(int amount) {
        foreach (Port port in ports) {
            if (port.holdingPipe == null) {
                port.transform.Rotate(-amount, 0, 0);
            }
        }
    }
}
