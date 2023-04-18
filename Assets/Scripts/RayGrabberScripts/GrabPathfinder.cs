using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPathfinder : MonoBehaviour
{
    public GameObject targetObject;

    public GrabHandler grabHandler;

    public LineRenderer lineRenderer;

    public List<Vector3> linePoints;

    //debug only
    public RaycastHit2D[] StoredRaycastHits;

    public Vector3 previousPosition;
    public Vector3 targetPreviousPosition;
    public Vector3 currentPoint;
    public Vector3 previousNormal;

    public Vector2 previousRayVector;

    public bool targetHit = false;

    //debug only
    public int previousTestedIterations = 0;

    public int testedIterations = 0;
    public int numberOfRays = 3;
    public int linepointLimit = 3;

    void Start()
    {
        targetObject = null;
        linePoints = new List<Vector3>();
        currentPoint = transform.position;
        previousPosition = currentPoint;
    }
    void Update()
    {
        //CheckIfTargetOrSelfMoved();
        //HandleSwitchingGrabState();
    }

    private void FixedUpdate()
    {
        //CheckIfRaycastHit();
    }

    private void HandleSwitchingGrabState()
    {
        if (grabHandler.objectGrabbed)
        {
            targetObject = grabHandler.grabbedObject;
            targetPreviousPosition = targetObject.transform.position;
            lineRenderer.gameObject.SetActive(true);
        }
        else
        {
            lineRenderer.gameObject.SetActive(false);
            targetObject = null;
        }
    }
    private void CheckIfTargetOrSelfMoved()
    {
        if (targetObject != null)
        {
            if ((previousPosition != transform.position) || (targetPreviousPosition != targetObject.transform.position))
            {
                if (targetHit)
                {
                    currentPoint = transform.position;
                    targetHit = false;
                    linePoints = new List<Vector3>();
                }
            }
        }

        previousPosition = transform.position;

        if (targetObject != null)
        {
            targetPreviousPosition = targetObject.transform.position;
        }
    }
    
    private void CheckIfRaycastHit()
    {
        //if (!targetHit && grabHandler.objectGrabbed)
        //{
            //PerformRaycast();
        //}
    }
    void HitTarget()
    {
        targetHit = true;
        lineRenderer.positionCount = linePoints.Count;
        lineRenderer.SetPositions(linePoints.ToArray());
        //if (testedIterations > previousTestedIterations)
        //{
            //Debug.Log(testedIterations);
            //previousTestedIterations = testedIterations;
        //}
        testedIterations = 0;
    }

    void PerformRaycast()
    {
        while (!targetHit)
        {
            if (testedIterations > 50000)
            {
                //Debug.Log("failed to find path in less than 10000 iterations");
                testedIterations = 0;
                currentPoint = transform.position;
                linePoints = new List<Vector3>();
                break;
            }
            if (linePoints.Count > linepointLimit)
            {
                currentPoint = transform.position;
                linePoints = new List<Vector3>();
            }
            CastRays();
            testedIterations++;
        }
    }

    void CastRays()
    {
        /*
        linePoints.Add(currentPoint);
        RaycastHit2D targetRay = new RaycastHit2D();
        targetRay = Physics2D.Raycast(currentPoint + (previousNormal * 0.1f), targetObject.transform.position - currentPoint, 1000f, ~(1 << 9));
        previousRayVector = targetObject.transform.position - currentPoint;
        if (targetRay.collider.gameObject == targetObject)
        {
        linePoints.Add(targetObject.transform.position);
        HitTarget();
        return;
        }
        linePoints.Add(targetRay.point);
        currentPoint = targetRay.point;
        previousNormal = targetRay.normal;
        */

        //random raycasts
        //for (int i = 0; i < numberOfRays; i++)
        //{
        //RaycastHit2D ray = new RaycastHit2D();
        //float number = Random.Range(-89, 90);
        //ray = Physics2D.Raycast(currentPoint + (previousNormal * 0.1f), new Vector2(previousNormal.x * Mathf.Cos(number) - previousNormal.y * Mathf.Sin(number), previousNormal.x * Mathf.Sin(number) + previousNormal.y * Mathf.Cos(number)), 1000f, ~(1 << 9));
        //linePoints.Add(ray.point);
        //currentPoint = ray.point;
        //previousNormal = ray.normal;
        //}


        //random raycasts that check distance to target and alternate between picking closest and farthest for higher probability of bouncing around obstacles
        /*
        for (int i = 0; i < numberOfRays; i++)
        {
            List<RaycastHit2D> rays = new List<RaycastHit2D>();
            for (int j = 0; j < 4; j++)
            {
                RaycastHit2D ray = new RaycastHit2D();
                float randomNumber = Random.Range(-89, 90);
                ray = Physics2D.Raycast(currentPoint + (previousNormal * 0.1f), new Vector2(previousNormal.x * Mathf.Cos(randomNumber) - previousNormal.y * Mathf.Sin(randomNumber), previousNormal.x * Mathf.Sin(randomNumber) + previousNormal.y * Mathf.Cos(randomNumber)), 1000f, ~(1 << 9));
                rays.Add(ray);
            }
            RaycastHit2D minimumDistanceRay = rays[0];
            foreach (RaycastHit2D rayInRays in rays)
            {
                if (rayInRays != rays[0])
                {
                    if (Vector2.Distance(rayInRays.point, targetObject.transform.position) < Vector2.Distance(minimumDistanceRay.point, targetObject.transform.position) && rays.IndexOf(rayInRays) % 2 == 0)
                    {
                        minimumDistanceRay = rayInRays;
                    }
                    if (Vector2.Distance(rayInRays.point, targetObject.transform.position) > Vector2.Distance(minimumDistanceRay.point, targetObject.transform.position) && rays.IndexOf(rayInRays) % 2 != 0)
                    {
                        minimumDistanceRay = rayInRays;
                    }
                }
            }
            linePoints.Add(minimumDistanceRay.point);
            currentPoint = minimumDistanceRay.point;
            previousNormal = minimumDistanceRay.normal;
        }
        */
             

        //algorithm juice
        /*
        List<RaycastHit2D> raycastHits = new List<RaycastHit2D>();
        RaycastHit2D probeRay = Physics2D.Raycast(currentPoint + (previousNormal * 0.1f), targetObject.transform.position - currentPoint, 1000f, ~(1 << 9));
        if (probeRay.collider != targetObject)
        {
            currentPoint = probeRay.point;
            previousNormal = probeRay.normal;
            raycastHits.Add(probeRay);
            probeRay = Physics2D.Raycast(currentPoint + (previousNormal * 0.1f), Vector2.pe)
        }
        */

    }

    //private void OnDrawGizmos()
    //{
        //Gizmos.color = Color.red;
        //foreach (RaycastHit2D storedRaycastHit in StoredRaycastHits)
        //{
            //Gizmos.DrawSphere(storedRaycastHit.point, 0.5f);
        //}
    //}
}

