using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : MonoBehaviour, IWorldSwitchListener, IHitable, IDamagable
{
   // public Animator animator;

    // need to be public to be accessed by the state machine
    public NavMeshAgent navMeshAgent;

    public static Action OnEnemyDied;
    public GameObject target { get; set; }
    public Player playerRef;
    public Animator animator;
    public CapsuleCollider hitBox;

    [SerializeField] UnityEvent<bool> DisableAggroRange;
    [SerializeField] public float speed;
    [SerializeField] private int health;
    [SerializeField] EnemyBody body;
    [SerializeField] private int damage;
    [SerializeField] private Color gizmoColor = Color.red;
    [SerializeField] private float playerDetachTime = 1;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] [Range(0, 1)] private float ammoDropProbability;
    [SerializeField] private GameObject ammoPickup;
    [SerializeField] private float turnAroundDuration;


    [HideInInspector] public SoundPlayer soundPlayer;
    private float currentPlayerDetachTimer;
   // public float convoyDetachtimer;
    public float attackRange;
    public float attackTimeInterval;

    public bool isAttacking { get; set; }
    public bool isScreaming {  get;  set; }
    public bool isDead { get; set; }

    // State machine section
    public EnemyStateMachine stateMachine { get; set; }
    public EnemyAttackState attackState { get; set; }
    public EnemyPrepareAttackState prepareAttackState { get; set; }
    public EnemyChaseState chaseState { get; set; }
    public EnemyIdleState idleState { get; set; }

    public EnemyDeadState deadState { get; set; }
  
    private void Awake()
    {
        stateMachine = new EnemyStateMachine();
        attackState = new EnemyAttackState(this, stateMachine);
        prepareAttackState = new EnemyPrepareAttackState(this, stateMachine);
        chaseState = new EnemyChaseState(this, stateMachine);
        idleState = new EnemyIdleState(this, stateMachine);
        deadState = new EnemyDeadState(this, stateMachine);
    }

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine.Initialize(chaseState);
        soundPlayer = GetComponent<SoundPlayer>();
    }
   
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void OnEnable()
    {
        WorldSwitcher.switchWorld += OnSwitchWorld;
        ConvoyNPC.died += OnNpcDied;
        GunScript.shot += OnUpdateDropProbability;
        PlayerHealth.onPlayerDeath += OnPlayerDied;
    }

    public void OnUpdateDropProbability(float ammoReserve, float currentAmmo)
    {
        if (currentAmmo <2)
        {
            currentAmmo = 2;
        }
        var totalAmmo = ammoReserve + currentAmmo;

        ammoDropProbability = 1 / (totalAmmo / 2);

        Debug.Log("Ammo drop rate:" + ammoDropProbability);

        // if there is a strangely high drop rate, it is due to a timing problem:
        // the gun shoots first and therefore the drop rate increses even before the enemy is hit.
        // This is why the drop rate is already at 50% when it should be 30%.
    }

    private void OnNpcDied(GameObject npc)
    {
        if(target != null && target == npc)
        {
            ClearTarget();
        }
    }

    private void OnDisable()
    {
        WorldSwitcher.switchWorld -= OnSwitchWorld;
        ConvoyNPC.died -= OnNpcDied;
        GunScript.shot -= OnUpdateDropProbability;
        PlayerHealth.onPlayerDeath -= OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        playerRef = null; 
        target = null;
        stateMachine.ChangeState(idleState);
    }

    public void OnSwitchWorld(bool isInHellWorld)
    {
        if(isDead) return;

        body.EnableBody(isInHellWorld);
        EnableHitBox(isInHellWorld);
        DisableAggroRange?.Invoke(!isInHellWorld);
        DisableParticles(isInHellWorld);
    }

    private void DisableParticles(bool isInHellWorld)
    {
        var particlesMain = particleSystem.main;
        var newColor = particleSystem.main.startColor.color;

        if (isInHellWorld)
        {
            newColor.a = 0;
        }
        else
        {
            newColor.a = 1;
        }

        particlesMain.startColor = newColor;
    }

    public void Initialize(GameObject target )
    {
        this.target = target;
    }

    // Update state machine
    private void Update()
    {
        stateMachine.currentState.FrameUpdate();

        // AI generated code
        if (target == null) return;
        navMeshAgent.updateRotation = false;

        Vector3 direction = (target.transform.position - transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                lookRotation,
                Time.deltaTime * turnAroundDuration // Adjust rotation speed
            );
        }

        // AI generated code end
    }

    public void OnAggroRangeTriggered(Player player)
    {
        if (player == null) return;
        stateMachine.currentState.TargetEnteredAggroRange(player);
    }
    
    public void OnHit(int damage, Vector3 impactVector)
    {
        UpdateHealth(-damage);
        HitByBullet(impactVector);

    }

    private void HitByBullet(Vector3 impactVector)
    {
        stateMachine.currentState.HitByBullet();
    }

    public void UpdateHealth(int value)
    {
        health += value;

        if (health <= 0)
        {
            DisableAggroRange?.Invoke(true);
            DisableParticles(true);
            stateMachine.ChangeState(deadState);
        }
    }

    public void UpdatePlayerDetachTimer()
    {
        if (target == null || playerRef == null || target != playerRef.gameObject)
        {
            currentPlayerDetachTimer = 0;
            return;
        }
        currentPlayerDetachTimer += Time.deltaTime;
       
        if (currentPlayerDetachTimer >= playerDetachTime)
        {
            ClearTarget();
        }
    }

     public void ResetPlayerDetachTimer()
     {
        currentPlayerDetachTimer = 0;  
     }

    public void Attack()
    {
        if (target != null && target.TryGetComponent<IHitable>(out IHitable hitableComponent) && (target.transform.position - transform.position).magnitude <= attackRange)
        {
            hitableComponent.OnHit(damage, transform.forward);

            if (playerRef != null && target == playerRef.gameObject)
            {
                ResetPlayerDetachTimer();
            }
        }
    }

    private void ClearTarget()
    {
        target = null;
    }


    private void CheckRandomPickupDrop()
    {
        var randomNumber = UnityEngine.Random.Range(0, 1f);
        if(randomNumber <= ammoDropProbability)
        {
            Instantiate(ammoPickup, transform.position, Quaternion.identity);
        }
    }

    public void OnDropPickUp()
    {
        CheckRandomPickupDrop();
    }

    public void OnDeathAnimationFinished()
    {
        Destroy(gameObject);
    }

    public void OnAttackAnimationFinished()
    {
        stateMachine.currentState.OnAttackAnimationFinished();
    }

    public void OnPlayerCaughtInstantly()
    {
        if (!isAttacking)
        {
            isScreaming = true;
            stateMachine.ChangeState(prepareAttackState);
        }
    }

    public void OnScreamAnimationFinished()
    {
        stateMachine.currentState.OnScreamAnimationFinished();
    }

    public bool isAlive()
    {
        return health > 0;
    }

    public void EnableHitBox(bool enable)
    {
        hitBox.enabled = enable;
    }
}
