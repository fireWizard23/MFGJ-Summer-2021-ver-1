using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, MyClasses.IVelocityRotated
{
    // ----------------------------------------------------------- TOP -----------------------------------------------------------

    private enum States { Idle, Walking, Attacking, InKnockack}

    // ----------------------------------------------------------- TO SERIALIZED -----------------------------------------------------------
    //[SerializeField] private float MyMovementSpeed = 5f;
    [SerializeField] private MobSO myMobInfo;
    public GameObject myBullet;

    // ----------------------------------------------------------- PRIVATE FIELDS -----------------------------------------------------------

    private Vector2 inputVector = Vector2.zero;
    private Vector2 velocity = Vector2.zero;
    private States currentState = States.Idle;

    // ----------------------------------------------------------- STATES FIELDS -----------------------------------------------------------
    private float isAttacking = -1f;


    // ----------------------------------------------------------- PUBLIC FIELDS -----------------------------------------------------------
    public Vector2 Velocity { get { return velocity; } }

    // ----------------------------------------------------------- Components -----------------------------------------------------------
    private Rigidbody2D myRigidbody;
    private Transform myMuzzlePos;

    // ----------------------------------------------------------- UNITY FUNCTIONS -----------------------------------------------------------

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myMuzzlePos = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        currentState = GetNewState();
        DoStateLogic();
    }

    private void FixedUpdate()
    {
        if(currentState != States.Attacking)
        {
            velocity = Vector2.Lerp(velocity, inputVector * myMobInfo.MovementSpeed, myMobInfo.MovementLerpWeight);
        }
        else
        {
            velocity = Vector2.zero;
        }

        myRigidbody.velocity = velocity;
    }

    // ----------------------------------------------------------- MY FUNCTIONS -----------------------------------------------------------

    private Vector2 GetInputVector()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        return MyUtils.Pooling.PVector2.GetVector(x, y);

    }

    private States GetNewState()
    {
        switch(currentState)
        {
            default:
            case States.Idle:
                if (velocity != Vector2.zero)
                    return States.Walking;
                return States.Idle;
            case States.Walking:
                if (velocity == Vector2.zero) return States.Idle;
                return States.Walking;
            case States.Attacking:
                if (isAttacking < 0) return States.Idle;
                return States.Attacking;
        }
    }
    
    private void DoStateLogic()
    {
        // Is Attacking Logic
        if(isAttacking >= 0f)
        {
            isAttacking += Time.deltaTime;
            if (isAttacking >= myMobInfo.AttackCooldown) isAttacking = -1f;
        }

        // MOVEMENT
        switch(currentState)
        {
            case States.Idle:
            case States.Walking:
                inputVector = GetInputVector();
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Shoot();
                }
                break;

        }
    }

    private void Shoot()
    {
        currentState = States.Attacking;
        isAttacking = 0f;
        var go = Pooler.Instance.Get("PlayerBullet");
        var dir = (myMuzzlePos.position - transform.position).normalized;
        go.GetComponent<MyClasses.IProjectile>()?.Setup(myMuzzlePos.position, dir);
    }


}
