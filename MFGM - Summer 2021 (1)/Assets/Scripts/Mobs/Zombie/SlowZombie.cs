using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZombie : MobBase, MyClasses.IKnockbackeable
{
    public enum States { Idle, Walking, Attacking, InKnockback}


    private States currentState = States.Idle;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        currentState = GetNewStates();
        DoStateLogic();
    }

    

    private States GetNewStates()
    {
        switch(currentState)
        {
            default:
            case States.Idle:
                if (myRigidbody.velocity != Vector2.zero)
                    return States.Walking;
                return States.Idle;
            case States.Walking:
                if (myRigidbody.velocity == Vector2.zero)
                    return States.Idle;
                return States.Walking;
            case States.InKnockback:
                if (myRigidbody.velocity == Vector2.zero)
                    return States.Idle;
                return States.InKnockback;
        }
    }

    private void DoStateLogic()
    {
        if (currentState == States.InKnockback)
        {
            followTargetComponent.enabled = false;
            velocityRotator.enabled = false;
        }

        else
        {
            followTargetComponent.enabled = true;
            velocityRotator.enabled = true;
        }
    }




    public void GetKnockback(Vector2 direction, float length, float duration)
    {
        duration = Mathf.Clamp(duration, 0, 1);
        length = Mathf.Clamp(length, 0, 5);
        currentState = States.InKnockback;

        myRigidbody.AddForce(direction.normalized * length * 10f, ForceMode2D.Impulse);
        MyUtils.Time.SetTimeout(() => {
            myRigidbody.velocity = Vector2.zero;
        }, duration, this);
    }



}
