using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class EnemyPatrol : MonoBehaviour
{
    Animator anim;
    
    public NavMeshAgent agent;
    public Transform[] wayPoint;

    public enum States { PATROL }

    States currentStates;

    private int currentWayPoint;

    private void Start()
    {
        anim = GetComponent<Animator>();

        if(agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    private void Update()
    {
        DoPatrol();
    }

    private void DoPatrol()
    {
        if(agent.destination != wayPoint[currentWayPoint].position)
        {
            agent.destination = wayPoint[currentWayPoint].position;
            anim.SetFloat("EnemyRun", 1);
        }

        if (HasReached())
        {
            currentWayPoint = (currentWayPoint + 1) % wayPoint.Length;
        }
    }

    private bool HasReached()
    {
        return (agent.hasPath && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance);

    }

}