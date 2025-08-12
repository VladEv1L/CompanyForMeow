using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public enum AIState { Idle, Search, Attack, Flee };

    
    private NavMeshAgent agent;

   
    public Transform playerTransform;

  
    public List<Transform> patrolPoints;

    
    private int currentPatrolPointIndex = 0;

    public float MoveToPlayerDistance = 10f;
    public float attackDistance = 1f;

    
    public float fleeDistance = 10f;

    
    public float chaseDuration = 10f;

   
    public float stateChangeInterval = 0.25f;

    
    private AIState currentState;

    
    private float chaseTimer;

    public AudioClip Roar;
    AudioSource AS;
    void Start()
    {
        AS = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();

      
        playerTransform = FindAnyObjectByType<P_STATE>().gameObject.transform;
      
        ChangeState(AIState.Search);
    }

    void Update()
    {
        switch (currentState)
        {
            case AIState.Idle:
                //CheckPlayerDistance();
                break;

            case AIState.Search:
                PatrolAndSearch();
                break;

            case AIState.Attack:
                MoveToPlayer();
                break;

            case AIState.Flee:
                RunAwayFromPlayer();
                break;
        }
    }

 
    void CheckPlayerDistance()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance <= MoveToPlayerDistance)
        {
            ChangeState(AIState.Attack);
        }
    }

    void PatrolAndSearch()
    {
        if (patrolPoints.Count > 0)
        {
            agent.SetDestination(patrolPoints[currentPatrolPointIndex].position);

            if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPointIndex].position) < 1f)
            {
                currentPatrolPointIndex++;
                if (patrolPoints.Count == currentPatrolPointIndex)
                {
                    currentPatrolPointIndex = 0;
                }
            }

            CheckPlayerDistance();
        }
    }

   
    void MoveToPlayer()
    {
        agent.SetDestination(playerTransform.position);

        if (Vector3.Distance(transform.position, playerTransform.position) < attackDistance)
        {
            
            Debug.Log("Attack");
            playerTransform.GetComponent<P_STATS>().TakeHP(Random.Range(25, 50));
            AS.PlayOneShot(Roar);
            ChangeState(AIState.Flee);
        }
        if (Vector3.Distance(transform.position, playerTransform.position) > 20)
        {
            ChangeState(AIState.Flee);
        }
    }

   
    void RunAwayFromPlayer()
    {
        /*Vector3 direction = transform.position - playerTransform.position;
        Vector3 newPosition = transform.position + direction.normalized * fleeDistance;

        agent.SetDestination(newPosition);

        if (Vector3.Distance(transform.position, newPosition) < 0.1f)
        {
            ChangeState(AIState.Search);
        }
        */
        agent.SetDestination(patrolPoints[3].transform.position);
        if (Vector3.Distance(transform.position, agent.destination) < 1)
        {
            ChangeState(AIState.Search);
        }
    }

   
    IEnumerator ChangeStateAfterDelay(AIState newState)
    {
        yield return new WaitForSeconds(stateChangeInterval);
        ChangeState(newState);
    }

  
    void ChangeState(AIState newState)
    {
        currentState = newState;
      

        switch (newState)
        {
            case AIState.Idle:
                agent.isStopped = true; 
                break;
            case AIState.Search:
                agent.isStopped = false;
                break;
            case AIState.Attack:
                agent.isStopped = false;
                break;
            case AIState.Flee:
                agent.isStopped = false; 
                break;
        }
    }
}