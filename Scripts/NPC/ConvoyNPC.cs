using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]

public class ConvoyNPC : MonoBehaviour, IHitable, IDamagable
{
    public Animator animator;

    // need to be public to be accessed by the state machine
    public NavMeshAgent navMeshAgent;
    // public Rigidbody rigidbody;

    public static Action<GameObject> died;
    public static Action hit;

    [SerializeField] private UnityEvent<float, float> updateHealthBar;
    [SerializeField] private UnityEvent<bool> enableHealthBar;
    
    [SerializeField] public List<Waypoint> waypoints = new List<Waypoint>();
    [SerializeField] public float speed;
    [SerializeField] private int health;
    
    private float currentHealth;

    [SerializeField] private float slowDownSpeed;
    [SerializeField] private float slowDownTime;

    private float currentSlowDownTimer;
    private bool dead;
    private bool injured;
    private Collider collider;

    //public float followDistance;

    // State machine section
    public ConvoyNPCStateMachine stateMachine { get; set; }
    public ConvoyNPCIdleState idleState { get; set; }
    public ConvoyNPCWalkState patrolState { get; set; }

    private void Awake()
    {
        stateMachine = new ConvoyNPCStateMachine();
        idleState = new ConvoyNPCIdleState(this, stateMachine);
        patrolState = new ConvoyNPCWalkState(this, stateMachine);
        currentHealth = health;
    }

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine.Initialize(idleState);
        navMeshAgent.isStopped = true;
        collider = GetComponent<Collider>();
    }

    // Update state machine
    private void Update()
    {
        if(dead) return;

        stateMachine.currentState.FrameUpdate();

        CheckSlowDownTimer();

        float agentSpeed = navMeshAgent.speed;
        float speedRatio = (agentSpeed > 0.01f) ? navMeshAgent.velocity.magnitude / agentSpeed : 0f;
        
        if(injured)
        {
            animator.SetFloat("Injured", speedRatio);
        }
        animator.SetFloat("Speed", speedRatio);
        
    }

    private void CheckSlowDownTimer()
    {
        if (currentSlowDownTimer > 0)
        {
            currentSlowDownTimer -= Time.deltaTime;
            navMeshAgent.speed = slowDownSpeed;
        }
        else
        {
            navMeshAgent.speed = speed;
        }
    }

    public void OnHit(int damage, Vector3 ictVector)
    {
        UpdateHealth(-damage);
        StartHitEffect();
    }

    public bool isAlive()
    {
        return currentHealth > 0;
    }

    private void StartHitEffect()
    {
        currentSlowDownTimer = slowDownTime;
        animator.SetTrigger("Hit");
        injured = true;
    }

    public void UpdateHealth(int value)
    {
        currentHealth += value;
       
        updateHealthBar.Invoke(currentHealth, health); // is connected with progress bar ui
        hit?.Invoke(); // is connected with progress bar ui

        if (currentHealth <= 0 )
        {
            died?.Invoke(gameObject);
            StartCoroutine(StartHealthBarDisableDelay());
            animator.SetTrigger("Dead");
            dead = true;
            navMeshAgent.isStopped = true;
            collider.enabled = false;
        }
    }

    public void WaitForPlayer(bool wait)
    {
        navMeshAgent.isStopped = wait;
    }

    private IEnumerator StartHealthBarDisableDelay()
    {
        yield return new WaitForSeconds(0.5f);
        enableHealthBar?.Invoke(true);
    }

    public void OnUpdateWaypointConditionStatus()
    {
        if(stateMachine.currentState == patrolState)
        {
            patrolState.UpdateCheckPointWaitingCondition();
        }
    }

    public void OnNpcTriggeredInteractableRange(bool entered)
    { 
        enableHealthBar?.Invoke(entered);
    }
}
