using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowTarget : MonoBehaviour
{
    public Transform target;

    public NPC_SO myInfo;

    private Rigidbody2D myRigidbody;
    private NavMeshAgent myAgent;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAgent = GetComponent<NavMeshAgent>();

        myAgent.updateRotation = false;
        myAgent.updateUpAxis = false;
        myAgent.updatePosition = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        myAgent.SetDestination(target.position);

        Vector2 targetPos = target.position - transform.position;
        bool inDistance = targetPos.sqrMagnitude > myInfo.StopDistance * myInfo.StopDistance;
        if (inDistance)
        {
            myRigidbody.velocity = myAgent.desiredVelocity.normalized;
            myAgent.nextPosition = myRigidbody.position;
        }
        else  
        {
            myRigidbody.velocity = Vector2.zero;
        }
    }



}
