using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionPoint : MonoBehaviour
{
    public int networkIndex = -1;

    public int typeChannel = 0;

    public bool powered = false;

    public List<ConnectionPoint> connections;

    public InterfaceWireManager iwm;

    void Start()
    {
        iwm = InterfaceWireManager.Instance;
        UpdateConnections();
    }

    public void UpdateConnections()
    {
        connections = new List<ConnectionPoint>();
        foreach (ConnectionPoint cPoint in iwm.connectionPoints)
        {
            //TODO float comparision here feels wrong, potentially put objects into a grid map and use grid coordinates instead.
            //This approach would remove grid-independent circuitry placement however which may not be ideal, have to do more research into this
            if ((Vector3.Distance(cPoint.transform.position, transform.position) == 0.5f) && (typeChannel == cPoint.typeChannel))
            {
                //Debug.Log(Vector3.Distance(cPoint.transform.position, transform.position));
                connections.Add(cPoint);
            }
        }
    }

    public void UpdateNeighbors(int newNetworkID)
    {
        iwm.networks[newNetworkID].Add(this);
        iwm.connectionPointsUnsorted.Remove(this);
        networkIndex = newNetworkID;
        foreach (ConnectionPoint cPoint in connections)
        {
            if (cPoint.networkIndex != networkIndex)
            {
                cPoint.UpdateNeighbors(networkIndex);
            }
        }
    }
    
    public void UpdateNetworkPower(bool newPowered)
    {
        if (newPowered == true)
        {
            iwm.UpdateNetworkPower(networkIndex, 1);
        }
        else
        {
            iwm.UpdateNetworkPower(networkIndex, -1);
        }
    }
}
