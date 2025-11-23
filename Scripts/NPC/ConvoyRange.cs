using UnityEngine;
using System.Collections.Generic;
using System;

public class ConvoyRange : MonoBehaviour
{
    [Header("NPC Group Settings")]
    private List<GameObject> npcs = new List<GameObject>();
    public float extraRadius = 2f; // Additional buffer around the group

    [Header("Spawn Trigger Settings")]
    public LayerMask spawnPointLayer; // Set this to your spawn point layer
    public float checkInterval = 0.5f; // How often to check for spawn points

    private SphereCollider groupCollider;
    private float nextCheckTime;
    private ConvoyInstructor convoyInstructor;
    public float range => groupCollider.radius - extraRadius;

    [SerializeField] private Color rangeColor = Color.blue;
    [SerializeField] private float maxDistanceToPlayer;

    void Awake()
    {
        // Add and configure the sphere collider
        groupCollider = gameObject.AddComponent<SphereCollider>();
        groupCollider.isTrigger = true;
        groupCollider.radius = 1f; // Initial value, will be updated
    }

    public void Initialize(ConvoyInstructor convoyInstructor)
    {
        npcs = convoyInstructor.GetNPCList();
        this.convoyInstructor = convoyInstructor;
    }

    void Update()
    {
        UpdateGroupCollider();

        // Periodic check instead of every frame for performance
        if (Time.time >= nextCheckTime)
        {
            CheckForSpawnTriggers();
            nextCheckTime = Time.time + checkInterval;
            CheckDistanceToPlayer();
        }
    }

    private void CheckDistanceToPlayer()
    {
        var player = ConvoyAndEnemyNotifier.instance.GetPlayer();
        if (player == null) return;

        if((player.transform.position - transform.position).magnitude > maxDistanceToPlayer)
        {
           convoyInstructor.ConvoyWaitForPlayer(true);
        }
        else
        {
           convoyInstructor.ConvoyWaitForPlayer(false);
        }
    }

    void UpdateGroupCollider()
    {
        if (npcs.Count == 0) return;

        
        // Calculate center point of all NPCs
        Vector3 center = Vector3.zero;
        foreach (GameObject npc in npcs)
        {
            if(npc ==  null)
            {
                npcs.Remove(npc);
                return;
            }
            center += npc.transform.position;
        }
        center /= npcs.Count;

        // Position the collider at the center
        transform.position = center;

        // Calculate the radius needed to contain all NPCs
        float maxDistance = 0f;
        foreach (GameObject npc in npcs)
        {
            float distance = Vector3.Distance(center, npc.transform.position);
            if (distance > maxDistance) maxDistance = distance;
        }

        // Set collider radius with additional buffer
        groupCollider.radius = maxDistance + extraRadius;
    }

    void CheckForSpawnTriggers()
    {
        // Find all spawn points within the collider radius
        Collider[] hitColliders = Physics.OverlapSphere(
            transform.position,
            groupCollider.radius,
            spawnPointLayer
        );

        foreach (Collider hitCollider in hitColliders)
        {
            SpawnTrigger spawnTrigger = hitCollider.GetComponent<SpawnTrigger>();
            if (spawnTrigger != null && !spawnTrigger.wasTriggered)
            {
                spawnTrigger.Trigger();
            }
        }
    }

    // Visualize the sphere in editor
    void OnDrawGizmosSelected()
    {
        if (groupCollider != null)
        {
            Gizmos.color = rangeColor;
            Gizmos.DrawWireSphere(transform.position, groupCollider.radius);
        }
    }
}