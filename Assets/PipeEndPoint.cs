using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PipeEndPoint : MonoBehaviour
{
    public PipeEndPoint[] siblingEndPoints;
    public PipeEndPoint connectingEndPoint;
    public WaterCollector connectingWaterCollector;

    void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out PipeEndPoint pipeEndPoint) 
            && !siblingEndPoints.Contains(pipeEndPoint)) {
            connectingEndPoint = pipeEndPoint;
            return; 
        }

        if (other.TryGetComponent(out WaterCollector waterCollector)) {
            connectingWaterCollector = waterCollector;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.TryGetComponent(out PipeEndPoint pipeEndPoint) 
            && !siblingEndPoints.Contains(pipeEndPoint)
            && connectingEndPoint != null
            && connectingEndPoint.Equals(pipeEndPoint)) {
            connectingEndPoint = null;    
            return; 
        }

        if (other.TryGetComponent(out WaterCollector waterCollector)) {
            connectingWaterCollector = null;
        }
    }

    public void FlowWaterToSiblings() {
        foreach (PipeEndPoint sibling in siblingEndPoints) {
            sibling.FlowWaterToConnectingEndPoint();
        }
    }

    public void FlowWaterToConnectingEndPoint() {
        if (connectingWaterCollector != null) {
            connectingWaterCollector.GetWater();
        }

        if (connectingEndPoint == null) {
            print(name + " is flowing water");
            return;
        }

        connectingEndPoint.FlowWaterToSiblings();
    }
}
