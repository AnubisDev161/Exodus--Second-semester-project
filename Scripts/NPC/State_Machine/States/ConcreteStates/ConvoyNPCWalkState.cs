using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConvoyNPCWalkState : NPCState
{
    private float currentWaitingTime;
    private int currentWaypointIndex;
    private float maxWaypointWaitingTime = 0f;
    private int lastWaypointIndex;
    private bool stopAtCurrentWaypoint;
    public ConvoyNPCWalkState(ConvoyNPC convoyNPC, ConvoyNPCStateMachine stateMachine) : base(convoyNPC, stateMachine)
    {
    }

    public override void EnterState()
    {
        //Debug.Log("Enter Patrol state!");
        if (convoyNPC.waypoints != null && convoyNPC.waypoints.Count > 1)
        {
            currentWaypointIndex = lastWaypointIndex;
            maxWaypointWaitingTime = convoyNPC.waypoints[currentWaypointIndex].GetComponent<Waypoint>().GetWaitingTime();
            UpdateCheckPointWaitingCondition();
        }
        else
        {
            Debug.LogError("Not enough Waypoints assigned!!!");
            stateMachine.ChangeState(convoyNPC.idleState);
            return;
        }

        convoyNPC.navMeshAgent.speed = convoyNPC.speed;
        SetDestination();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        if (convoyNPC.navMeshAgent.remainingDistance < 0.1f)
        {
            if (stopAtCurrentWaypoint)
            {
                convoyNPC.navMeshAgent.isStopped = stopAtCurrentWaypoint;
                return;
            }
            UpdateWaitingTime();
            if (currentWaitingTime >= maxWaypointWaitingTime)
            {
                currentWaitingTime = 0;
                NextWaypoint();
                SetDestination();
            }
        }
    }

    private void NextWaypoint()
    {
        currentWaypointIndex++;
        if (currentWaypointIndex >= convoyNPC.waypoints.Count)
        {
            currentWaypointIndex = 0;
            lastWaypointIndex = 0;
        }

        maxWaypointWaitingTime = convoyNPC.waypoints[currentWaypointIndex].GetComponent<Waypoint>().GetWaitingTime();
        UpdateCheckPointWaitingCondition();

        lastWaypointIndex = currentWaypointIndex;
    }

    private void SetDestination()
    {
        Vector3 target = convoyNPC.waypoints[currentWaypointIndex].transform.position;
        convoyNPC.navMeshAgent.destination = target;

    }

    public override void PhysicsUpdate()
    { 
        base.PhysicsUpdate();
    
    }

    private void UpdateWaitingTime()
    {
        currentWaitingTime += Time.deltaTime;
    }

    public void UpdateCheckPointWaitingCondition()
    {
        if (convoyNPC.waypoints.Count == 0) return;
        if (convoyNPC.waypoints[currentWaypointIndex].TryGetComponent<CheckPoint>(out CheckPoint checkPoint))
        {
            stopAtCurrentWaypoint = !checkPoint.conditionMet;
            if (!stopAtCurrentWaypoint)
            {
                convoyNPC.navMeshAgent.isStopped = false;
            }
        }
    }
}