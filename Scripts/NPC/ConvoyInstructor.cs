using System;
using System.Collections.Generic;
using UnityEngine;

public class ConvoyInstructor : MonoBehaviour
{
    private List<GameObject> npcs = new List<GameObject>();
    [SerializeField] private ConvoyRange convoyRange;

    private void OnEnable()
    {
        CheckPoint.conditionStatusChanged += UpdateNPCStoppingCondition;
    }

    private void OnDisable()
    {
        CheckPoint.conditionStatusChanged -= UpdateNPCStoppingCondition;
    }

    private void Start()
    {
        npcs = ConvoyAndEnemyNotifier.instance.GetList();
        convoyRange.Initialize(this);
    }

    public List<GameObject> GetNPCList()
    {
        return npcs;
    }

    public void ConvoyWaitForPlayer(bool stop)
    {
        foreach (GameObject npc in npcs)
        {
            npc.GetComponent<ConvoyNPC>().WaitForPlayer(stop);
        }
    }

    public void UpdateNPCStoppingCondition()
    {
        foreach (GameObject npc in npcs)
        {
            npc.GetComponent<ConvoyNPC>().OnUpdateWaypointConditionStatus();
        }
    }
}
