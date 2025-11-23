
using System;
using System.Collections.Generic;
using UnityEngine;

public class ConvoyProgressBar : MonoBehaviour
{
    [SerializeField] private List<Waypoint> waypoints;
    [SerializeField] private float progressCheckTime;
    [SerializeField] private ConvoyRange convoyRange;
    [SerializeField] public static Action<float, float> updateProgress;

    private float currentProgressCheckTime;
    private float totalDistance;
    private float currentProgress;

    private void Start()
    {
        totalDistance = CalulateTotalDistance();
        Debug.Log("Total distance:"+ totalDistance);
    }
    private void Update()
    {
        if (waypoints.Count == 0) return;
        
        currentProgressCheckTime += Time.deltaTime;
        
        if(currentProgressCheckTime >= progressCheckTime)
        {
            currentProgressCheckTime = 0;
            CalculateCurrentProgress();
        }
    }

    private float CalulateTotalDistance()
    {
        float totalDistance = 0;
        Waypoint lastWaypoint = null;
        foreach (var waypoint in waypoints)
        {
            if (lastWaypoint != null)
            {
                totalDistance += Math.Abs((waypoint.transform.position - lastWaypoint.transform.position).magnitude);
            }

            lastWaypoint = waypoint;
        }
        return totalDistance;
    }

    private void CompareNearestWaypointWithTotalDistance(Waypoint nearestWaypoint)
    {
        int nearestWaypointIndex = 0;
        float distance = 0;
        for (int i = 0; i < waypoints.Count; i++)
        {
            if (waypoints[i] == nearestWaypoint)
            {
                nearestWaypointIndex = i;
            }
        }

        Waypoint nextWaypoint = null;
        if(nearestWaypointIndex + 1 < waypoints.Count)
        {
            nextWaypoint = waypoints[nearestWaypointIndex + 1];
        }
        else
        {
            nextWaypoint = waypoints[nearestWaypointIndex];
        }
        var distanceToNextWaypoint = (nextWaypoint.transform.position - convoyRange.transform.position ).magnitude;
        var distanceToNearestWaypoint = (convoyRange.transform.position - nearestWaypoint.transform.position).magnitude;
        //distance = distanceToNearestWaypoint;


        var distanceBetweenWaypoints = (nextWaypoint.transform.position - nearestWaypoint.transform.position).magnitude;


        distance = distanceToNextWaypoint;

      //  currentProgress = (((totalDistance / (waypoints.Count - 1)) * nearestWaypointIndex));
      //  currentProgress = (((totalDistance / (waypoints.Count -1)) * nearestWaypointIndex) - (distance - convoyRange.range)); //currentProgress - ( convoyRange.transform.position - nearestWaypoint.transform.position).magnitude;
        currentProgress = (((totalDistance / (waypoints.Count - 1)) * nearestWaypointIndex) + (distanceBetweenWaypoints - distanceToNextWaypoint ));
    }


    private void CalculateCurrentProgress()
    {
        Waypoint waypoint = CalculateNearestWypointToConvoy();
        CompareNearestWaypointWithTotalDistance(waypoint);
        updateProgress?.Invoke(currentProgress, totalDistance);
        //Debug.Log("current distance" + currentProgress);
        //Debug.Log("total distance" + totalDistance);
    }

    private Waypoint CalculateNearestWypointToConvoy()
    {
        currentProgress = 0;
        float lastNearestDistance = Mathf.Infinity;
        Waypoint currentWaypoint = null;

        foreach (var waypoint in waypoints)
        {
            float distance = Math.Abs((waypoint.transform.position - convoyRange.transform.position).magnitude);
            if (distance < lastNearestDistance)
            {
                lastNearestDistance = distance;
                currentWaypoint = waypoint;
            }
        }

        return currentWaypoint;
    }
}
