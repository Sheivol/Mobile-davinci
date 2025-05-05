using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;

    // patrulla
    public Transform[] waypoints;
    int waypointIndex;
    Vector3 target;

    // ver al pj
    public Transform player;
    public float viewRange = 10f;
    public float viewAngle = 60f;

    bool isChasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            isChasing = true;
            agent.SetDestination(player.position);
        }
        else
        {
            if (isChasing)
            {
                isChasing = false;
                UpdateDestination();
            }

            Patrol();
        }
    }

    void Patrol()
    {
        if (Vector3.Distance(transform.position, target) < 1f)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
    }

    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
    }

    void IterateWaypointIndex()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }

    bool CanSeePlayer()
    {
        Vector3 dirToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, dirToPlayer);

        if (dirToPlayer.magnitude < viewRange && angle < viewAngle / 2f)
        {
            Ray ray = new Ray(transform.position + Vector3.up * 1.5f, dirToPlayer.normalized);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, viewRange))
            {
                if (hit.transform == player)
                {
                    return true;
                }
            }
            return true;
        }

        return false;
    }
}
