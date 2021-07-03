using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowTarget : MonoBehaviour
{
    [HideInInspector]
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
        if(target != null)
        {
            myAgent.SetDestination(target.position);

            Vector2 targetPos = target.position - transform.position;
            float distance = targetPos.sqrMagnitude;
            //Distance check
            if(distance > myInfo.NoticeRadius * myInfo.NoticeRadius)
            {
                target = null;
                myRigidbody.velocity = Vector2.Lerp(myRigidbody.velocity, Vector2.zero, myInfo.MovementLerpWeight);
                return;
            }

            if (distance > myInfo.StopDistance * myInfo.StopDistance)
            {
                myRigidbody.velocity = Vector2.Lerp(myRigidbody.velocity, 
                    myAgent.desiredVelocity.normalized * myInfo.MovementSpeed,
                    myInfo.MovementLerpWeight);

                myAgent.nextPosition = myRigidbody.position;
            }
            else
            {
                myRigidbody.velocity = Vector2.zero;
            }
        }
        else
        {
            myRigidbody.velocity = Vector2.Lerp(myRigidbody.velocity, Vector2.zero, myInfo.MovementLerpWeight);
        }



    }



}
