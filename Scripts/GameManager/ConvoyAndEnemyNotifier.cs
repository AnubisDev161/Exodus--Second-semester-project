using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class ConvoyAndEnemyNotifier : MonoBehaviour
{
    [SerializeField] private List<GameObject> npcList;
    [SerializeField] public static Action npcDied;
    public static ConvoyAndEnemyNotifier instance;
    [SerializeField] private Player player;
    
    private void Awake()
    {
        Setup();
    }

    private void OnEnable()
    {
        ConvoyNPC.died += OnNPCDied;
        PlayerHealth.onPlayerDeath += OnPlayerDied;
    }
    private void OnDisable()
    {
        ConvoyNPC.died -= OnNPCDied;
        PlayerHealth.onPlayerDeath -= OnPlayerDied;
    }
    private void Setup()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public GameObject GetNearestTarget(GameObject currentEnemy)
    {
        float lastNearestDistance = Mathf.Infinity;
        GameObject nearestTarget = null;
        var deadNPCs = new List<GameObject>();
        foreach (GameObject npc in npcList)
        {
            if(ValidateNPC(npc))
            {
                deadNPCs.Add(npc);
                continue;
            }
            //float distance = npc.position.x - currentEnemyPosition.position.x;
            float distance = Vector3.Distance(npc.transform.position, currentEnemy.transform.position);
            if (distance < lastNearestDistance)
            {
                lastNearestDistance = distance;
                nearestTarget = npc;
            }
        }
        
        foreach (GameObject npc in deadNPCs)
        {
            npcList.Remove(npc);
        }

        if (nearestTarget == null && player != null)
        {
            nearestTarget = player.gameObject;
        }
        return nearestTarget;
    }

    public int GetListLength()
    {
        return npcList.Count;
    }

    public List<GameObject> GetList()
    {
        return npcList;
    }

    public Player GetPlayer()
    {
        return player;
    } 

    private bool ValidateNPC(GameObject npc)
    {
        if (npc == null || (!npc.GetComponent<IHitable>().isAlive())) 
        {
            return true;
        }
        
        return false;
    }

    public void OnNPCDied(GameObject npc)
    {
        if (!npcList.Contains(npc)) return;
        npcList.Remove(npc);
        npcDied?.Invoke(); // informs all enemies
    }
    public void OnPlayerDied()
    {
        player = null;
    }
}
