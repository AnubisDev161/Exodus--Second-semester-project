using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(IGoapInteractor))]
public class GoapAgent : MonoBehaviour
{
    IGoapInteractor goapInteractor;
    NavMeshAgent navMeshAgent;

    float currentTimeInterval;
    [SerializeField] float timerInterval = 2;


    AgentGoal lastGoal;
    public AgentGoal currentGoal;
    public ActionPlan actionPlan;
    public AgentAction currentAction;

    public Dictionary<AgentBelief.BeliefType, AgentBelief> beliefs;
    public HashSet<AgentAction> actions;
    public HashSet<AgentGoal> goals;

    IGoapPlanner goapPlanner;
    private void Awake()
    {

        goapPlanner = new GoapPlanner();
        goapInteractor = GetComponent<IGoapInteractor>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SetupBeliefs();
        SetupActions();
        SetupGoals();
    }

    private void SetupBeliefs()
    {
        beliefs = new Dictionary<AgentBelief.BeliefType, AgentBelief>();
        BeliefFactory factory = new BeliefFactory(this, beliefs);
        factory.AddBelief(AgentBelief.BeliefType.Nothing, () => false);
        factory.AddBelief(AgentBelief.BeliefType.IsPatroling, () => (!goapInteractor.HasTarget() && navMeshAgent.hasPath));

    }

    private void SetupActions()
    {
        actions = new HashSet<AgentAction>();

        // ! WIP !

        //actions.Add(new AgentAction.Builder(AgentAction.ActionType.Idle).WithStrategy(new IdleAction(5)).AddEffect(beliefs[AgentBelief.BeliefType.Nothing]).Build());
        //actions.Add(new AgentAction.Builder(AgentAction.ActionType.FindValidWaypoint).WithStrategy(new FindWaypointAction(navMeshAgent)).AddEffect(beliefs[AgentBelief.BeliefType.HasValidWaypoint]).Build());
        //actions.Add(new AgentAction.Builder(AgentAction.ActionType.PatrolToWaypoint).WithStrategy(new PatrolToWaypointAction(navMeshAgent)).
        //    AddPrecondition(beliefs[AgentBelief.BeliefType.HasNoTarget]).
        //    AddPrecondition(beliefs[AgentBelief.BeliefType.HasValidWaypoint]).
        //    AddEffect(beliefs[AgentBelief.BeliefType.IsPatroling]).Build());
        
    }

    private void SetupGoals()
    {
        goals = new HashSet<AgentGoal>();
        goals.Add(new AgentGoal.Builder(AgentGoal.GoalType.Idle).WithPriority(1).WithDesiredEffect(beliefs[AgentBelief.BeliefType.Nothing]).Build());
        goals.Add(new AgentGoal.Builder(AgentGoal.GoalType.Patrol).WithPriority(1).WithDesiredEffect(beliefs[AgentBelief.BeliefType.IsPatroling]).Build());

    }
    
    bool InRangeOf(Vector3 position, float range) => Vector3.Distance(transform.position, position) < range;

    private void HandleTargetChanged()
    {
        Debug.Log("target changed, clearing current action and goal");
        // Force the planner to re-evaluate the plan
        currentAction = null;
        currentGoal = null;

    }

    public void Update()
    {
        currentTimeInterval += Time.deltaTime;
        if (currentTimeInterval < timerInterval)
        {
            currentTimeInterval = 0;
            //UpdateStats();
        }

        // Update the plan and current action if there is one
        if(currentAction == null)
        {
            Debug.Log("calculating any potential new plan");
            CalculatePlan();

            if (actionPlan != null && actionPlan.actions.Count > 0)
            {
                

                currentGoal = actionPlan.agentGoal;
                Debug.Log($"Goal: {currentGoal.goalType.ToString()} with {actionPlan.actions.Count} actions in plan" );
                currentAction = actionPlan.actions.Pop();
                Debug.Log($"Popped action: {currentAction.actionType.ToString()}");
                // Verirfy all precondition effects are true
                if(currentAction.preconditions.All(belief => belief.Evaluate()))
                {
                    currentAction.Start();

                }else
                {
                    Debug.Log("preconditions not met, clearing current action and goal");
                    currentAction = null;
                    currentGoal = null;
                }
            }
        }

        // If we have a current action, execute it
        if (actionPlan != null && currentAction != null)
        {
            currentAction.Update(Time.deltaTime);

            if (currentAction.complete)
            {
                Debug.Log($"{currentAction.actionType.ToString()} complete");
                currentAction.Stop();
                currentAction = null;

                if (actionPlan.actions.Count == 0)
                {
                    Debug.Log("Plan complete");
                    lastGoal = currentGoal;
                    currentGoal = null;
                }
            }
        }
    }

    private void CalculatePlan()
    {
        var priorityLevel = currentGoal?.priority ?? 0;

        HashSet<AgentGoal> goalsToCheck = goals;

        // If we have a current goal, we only want to check goals with higher priority
        if(currentGoal != null)
        {
            Debug.Log("current goal exists, checking goals with higher priority");
            goalsToCheck = new HashSet<AgentGoal>(goals.Where(goal => goal.priority > priorityLevel));
        }

        var potentialPlan = goapPlanner.CreatePlan(this, goalsToCheck, lastGoal);
        if (potentialPlan != null)
        {
            actionPlan = potentialPlan;
        }
    }
}
