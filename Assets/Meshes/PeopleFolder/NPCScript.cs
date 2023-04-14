using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCScript : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0f;
        }

        if (agent.velocity.magnitude < 0.1)
        {
            animator.Play(0);
        }
    }
    public static Vector3
        RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDir = Random.insideUnitSphere * dist;
        randDir += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, dist, layermask);
        return navHit.position;
    }
}
