using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceWireManager : MonoBehaviour
{
    //TODO Refactor class name to ConnectionPointManager

    public int tempCounter;

    public int startingID = 0;

    public bool updatingNetwork = false;

    public List<ConnectionPoint> connectionPoints;

    public List<ConnectionPoint> connectionPointsUnsorted;

    public static InterfaceWireManager Instance = null;

    public List<List<ConnectionPoint>> networks;
    public List<int> networkPowerStates;

    private void Awake()
    {
        networks = new List<List<ConnectionPoint>>();
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        UpdateInterfaceWires();
    }

    private void Start()
    {
        AssignNetworks();
    }

    public void UpdateInterfaceWires()
    {
        connectionPoints = new List<ConnectionPoint>();
        foreach (ConnectionPoint cPoint in FindObjectsOfType<ConnectionPoint>())
        {
            connectionPoints.Add(cPoint);
        }
        connectionPointsUnsorted = connectionPoints;
    }

    public void AssignNetworks()
    {
        while (connectionPointsUnsorted.Count != 0)
        {
            networks.Add(new List<ConnectionPoint>());
            networkPowerStates.Add(0);
            connectionPointsUnsorted[0].UpdateNeighbors(startingID);
            startingID++;
        }
    }

    public void UpdateNetworkPower(int networkIndex, int updateAmount)
    {
        if (networkPowerStates[networkIndex] == 0)
        {
            if (updateAmount == 1)
            {
                PowerNetwork(networkIndex);
                networkPowerStates[networkIndex] += updateAmount;
            }
        }
        else
        {
            if ((networkPowerStates[networkIndex] + updateAmount) == 0)
            {
                UnpowerNetwork(networkIndex);
                
                networkPowerStates[networkIndex] += updateAmount;
            }
            else
            {
                networkPowerStates[networkIndex] += updateAmount;
            }
        }
    }

    public void PowerNetwork(int networkID)
    {
        foreach (ConnectionPoint cPoint in networks[networkID])
        {
            cPoint.powered = true;
        }
    }

    public void UnpowerNetwork(int networkID)
    {
        foreach (ConnectionPoint cPoint in networks[networkID])
        {
            cPoint.powered = false;
        }
    }
}
