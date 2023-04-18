using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkboxInterface : MonoBehaviour
{
    private int[] cDirs;

    private bool powered = false;

    public SpriteRenderer spriteRenderer;

    public List<Sprite> sprites;

    public ConnectionPoint connectionPoint;

    public LinkBox linkBox;

    public InterfaceWireManager iwm;

    private void Start()
    {
        cDirs = new int[4];
        cDirs[0] = 0;
        cDirs[1] = 0;
        cDirs[2] = 0;
        cDirs[3] = 0;
        iwm = InterfaceWireManager.Instance;
        UpdateSprite();
    }

    void Update()
    {
        UpdatePower();
    }

    public void UpdatePower()
    {
        if (linkBox.type == 1)
        {
            if (linkBox.state == true && !powered)
            {
                connectionPoint.UpdateNetworkPower(true);
                powered = true;
            }
            else if (linkBox.state == false && powered)
            {
                connectionPoint.UpdateNetworkPower(false);
                powered = false;
            }
        }
        else if (linkBox.type == 0)
        {
            if (connectionPoint.powered == true && !powered)
            {
                linkBox.state = true;
                linkBox.stateSent = powered;
                linkBox.UpdateChannelSender();
                powered = true;
            }
            else if (connectionPoint.powered == false && powered)
            {
                linkBox.state = false;
                linkBox.stateSent = powered;
                linkBox.UpdateChannelSender();
                powered = false;
            }
        }
    }

    public void UpdateSprite()
    {
        //Debug.Log("updateSprite called");
        //Debug.Log(iwm.connectionPoints.Count);
        foreach (ConnectionPoint iwmConnectionPoint in iwm.connectionPoints)
        {
            if (Vector3.Distance(iwmConnectionPoint.transform.position, transform.position) < 1 && iwmConnectionPoint.typeChannel == this.connectionPoint.typeChannel)
            {
                if (iwmConnectionPoint.transform.position.x.Equals(transform.position.x) && Mathf.Approximately(iwmConnectionPoint.transform.position.y, transform.position.y + 0.5f))
                {
                    cDirs[0] = 1;
                }
                else if (Mathf.Approximately(iwmConnectionPoint.transform.position.x, transform.position.x + 0.5f) && iwmConnectionPoint.transform.position.y.Equals(transform.position.y))
                {
                    cDirs[1] = 1;
                }
                else if (iwmConnectionPoint.transform.position.x.Equals(transform.position.x) && Mathf.Approximately(iwmConnectionPoint.transform.position.y, transform.position.y - 0.5f))
                {
                    cDirs[2] = 1;
                }
                else if (Mathf.Approximately(iwmConnectionPoint.transform.position.x, transform.position.x - 0.5f) && iwmConnectionPoint.transform.position.y.Equals(transform.position.y))
                {
                    cDirs[3] = 1;
                }
            }
            //Debug.Log("connection point checked");
        }
        string tempString = cDirs[0].ToString() + cDirs[1].ToString() + cDirs[2].ToString() + cDirs[3].ToString();

        spriteRenderer.sprite = sprites[System.Convert.ToInt32(tempString, 2)];
    }
}
