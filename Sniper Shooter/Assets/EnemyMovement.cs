using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    public NavMeshAgent navMeshAgent;
    public float speed;
    public Transform target;

    void Start()
    {
        navMeshAgent.speed = speed;
        navMeshAgent.SetDestination(target.position);
    }

    public void SetTarget(Transform newTarget) => target = newTarget;
}
