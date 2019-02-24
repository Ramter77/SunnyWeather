using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    [Header("Navigation")]
    public int wanderIndex;
    private NavMeshAgent agent;
    private GameObject[] wander;
    private Transform[] wanderPoints;

    [Header("BehaviorStates")]
    public bool attackEnabled = false;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        wander = GameObject.FindGameObjectsWithTag("WanderPoints");
        wanderPoints = new Transform[wander.Length];
        for (int i = 0; i < wander.Length; i++)
        {
            wanderPoints[i] = wander[i].transform;
            //Debug.Log(wanderPoints[i]);
        }
        wanderIndex = Random.Range(0, wanderPoints.Length);
        GoToNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDestinationReached();
    }


    public void GoToNextPoint()
    {
        agent.destination = wanderPoints[wanderIndex].position;
    }

    public void CheckDestinationReached()
    {
        if (!agent.pathPending && agent.remainingDistance < 1f)
        {
            agent.isStopped = true;
            attackEnabled = true;
        }
    }
}
