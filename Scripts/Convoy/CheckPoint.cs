using System;
using UnityEngine;

[RequireComponent (typeof(Waypoint))]
public class CheckPoint : MonoBehaviour
{
    Waypoint waypoint;
    public bool conditionMet { get; private set; }
    public static Action conditionStatusChanged;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }

    public void OnConditionMet()
    {
        conditionMet = true;
        conditionStatusChanged?.Invoke();
    }

  
}
