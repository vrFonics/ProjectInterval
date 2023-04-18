using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceWire : MonoBehaviour
{
    private int[] _cDirs;

    public ConnectionPoint cPoint;

    public SpriteRenderer spriteRenderer;

    public Sprite[] sprites;

    public InterfaceWireManager iwm;

    private void Awake()
    {
        Debug.unityLogger.logEnabled = false;
    }

    private void Start()
    {
        iwm = InterfaceWireManager.Instance;
        _cDirs = new int[4];
        _cDirs[0] = 0;
        _cDirs[1] = 0;
        _cDirs[2] = 0;
        _cDirs[3] = 0;
        UpdateSprite();
        //Debug.Log("start method called");
    }

    private void UpdateSprite()
    {
        //Debug.Log("updateSprite called");
        //Debug.Log(iwm.connectionPoints.Count.ToString() + "is the iwm connectionpoints count");
        foreach (ConnectionPoint connectionPoint in iwm.connectionPoints)
        {
            if ((Vector3.Distance(connectionPoint.transform.position, transform.position) < 1) && connectionPoint.typeChannel == cPoint.typeChannel) {
                if (connectionPoint.transform.position.x.Equals(transform.position.x) && Mathf.Approximately(connectionPoint.transform.position.y, transform.position.y + 0.5f))
                {
                    _cDirs[0] = 1;
                }
                else if (Mathf.Approximately(connectionPoint.transform.position.x, transform.position.x + 0.5f) && connectionPoint.transform.position.y.Equals(transform.position.y))
                {
                    _cDirs[1] = 1;
                }
                else if (connectionPoint.transform.position.x.Equals(transform.position.x) && Mathf.Approximately(connectionPoint.transform.position.y, transform.position.y - 0.5f))
                {
                    _cDirs[2] = 1;
                }
                else if (Mathf.Approximately(connectionPoint.transform.position.x, transform.position.x - 0.5f) && connectionPoint.transform.position.y.Equals(transform.position.y))
                {
                    _cDirs[3] = 1;
                }
            }
            //Debug.Log("connection point checked");
        }
        string tempString = _cDirs[0].ToString() + _cDirs[1].ToString() + _cDirs[2].ToString() + _cDirs[3].ToString();

        spriteRenderer.sprite = sprites[System.Convert.ToInt32(tempString, 2)];  
    }
}
