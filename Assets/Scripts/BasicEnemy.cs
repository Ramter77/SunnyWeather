using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    [Header("Navigation")]
    private NavMeshAgent agent;
    private GameObject closest = null;
    [Header("BehaviorStates")]
    public bool attackEnabled = false;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        FindClosestTarget();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDestinationReached();
    }


   

    public void CheckDestinationReached()
    {
        if (!agent.pathPending && agent.remainingDistance < 1f)
        {
            agent.isStopped = true;
            attackEnabled = true;
        }
    }

    public void FindClosestTarget()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("possibleTargets");
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        agent.destination = closest.transform.position;
    }
}
