using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;

    public NPC_SO myInfo;

    private Rigidbody2D myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 targetPos= target.position - transform.position;
        bool inDistance = targetPos.sqrMagnitude > myInfo.StopDistance * myInfo.StopDistance;
        MyUtils.Print(targetPos.sqrMagnitude, myInfo.StopDistance * myInfo.StopDistance);
        if (inDistance)
        {
            myRigidbody.velocity = targetPos.normalized * myInfo.MovementSpeed;
        }
        else
        {
            myRigidbody.velocity = Vector2.zero;
        }
    }



}
