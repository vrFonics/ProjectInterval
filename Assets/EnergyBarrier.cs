using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBarrier : MonoBehaviour
{
    public Transform nodeOne;
    public Transform nodeTwo;

    public BoxCollider2D barrierCollider;

    public GameObject barrierColliderObject;

    public LineRenderer lineRenderer;

    public LinkBox linkBox;

    private void Start()
    {
        UpdateBarrierCollider();
    }

    public void UpdateBarrierCollider()
    {
        barrierCollider.size = new Vector2(Vector3.Distance(nodeOne.transform.position, nodeTwo.transform.position), barrierCollider.size.y);
        barrierColliderObject.transform.position = (nodeOne.transform.position + nodeTwo.transform.position) / 2;

        barrierColliderObject.transform.Rotate(new Vector3(0, 0, Vector3.Angle(barrierColliderObject.transform.right, nodeTwo.transform.position - barrierColliderObject.transform.position)));

        Vector3[] nodes = new Vector3[3];
        nodes[0] = nodeOne.transform.position;
        nodes[1] = barrierColliderObject.transform.position;
        nodes[2] = nodeTwo.transform.position;
        lineRenderer.positionCount = 3;
        lineRenderer.SetPositions(nodes);
    }
    
    void Update()
    {
        if (linkBox == null)
        {
            Debug.Log("No assigned linkbox for: " + this.gameObject);
            return;
        }
        if (linkBox.state && barrierColliderObject.activeSelf)
        {
            barrierColliderObject.SetActive(false);
        }
        else if (!linkBox.state && !barrierColliderObject.activeSelf)
        {
            barrierColliderObject.SetActive(true);
        }
    }
}
