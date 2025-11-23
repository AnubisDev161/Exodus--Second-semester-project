using System;
using UnityEngine;
using UnityEngine.Events;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Color gizmoColor = Color.white;
    [SerializeField] private float radius = 1.0f;
    [SerializeField] private float waitingTime = 1.0f;


    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, radius);
    }

    public float GetWaitingTime()
    {
        return waitingTime;
    }
}

