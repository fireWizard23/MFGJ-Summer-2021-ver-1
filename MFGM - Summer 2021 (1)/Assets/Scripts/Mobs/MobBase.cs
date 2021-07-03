using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MyClasses;

public abstract class MobBase : MonoBehaviour, IVelocityRotated
{
    public virtual Vector2 Velocity { get { return myRigidbody.velocity; } }
    public NPC_SO myInfo;
    public float NoticeMultiplier = 1f;


    protected Rigidbody2D myRigidbody;
    protected FollowTarget followTargetComponent;
    protected VelocityRotator velocityRotator;
    protected NoticeComponent noticeComponent;


    protected virtual void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        followTargetComponent = GetComponent<FollowTarget>();
        velocityRotator = GetComponent<VelocityRotator>();
        noticeComponent = GetComponent<NoticeComponent>();
        myInfo.myMob = this;

    }




}
