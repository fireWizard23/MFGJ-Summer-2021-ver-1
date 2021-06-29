using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MyClasses;

public class PlayerController : MonoBehaviour, IVelocityRotated, IKnockbackeable
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
    private float canAttack = 0;

    private Vector2 knockbackEndpoint;

    // ----------------------------------------------------------- PUBLIC FIELDS -----------------------------------------------------------
    public Vector2 Velocity { get { return velocity; } }

    // ----------------------------------------------------------- Components -----------------------------------------------------------
    private Rigidbody2D myRigidbody;
    private SpriteRenderer mySpriteRenderer;
    private CapsuleCollider2D myCollider;
    private Transform myMuzzlePos;
    private Transform myFacingPos;

    // ----------------------------------------------------------- UNITY FUNCTIONS -----------------------------------------------------------

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<CapsuleCollider2D>();
        myMuzzlePos = transform.Find("Muzzle");
        myFacingPos = transform.Find("Facing");
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
        if (Mathf.Abs(velocity.x) < 0.1f && Mathf.Abs(velocity.y) < 0.1f) velocity = Vector2.zero;
        myRigidbody.velocity = velocity;
    }

    // ----------------------------------------------------------- MY FUNCTIONS -----------------------------------------------------------

        private Vector2 GetInputVector()
    {
        float x = 0;
        float y = 0;
        if (Input.GetKey(KeyCode.A)) x = -1f;
        else if (Input.GetKey(KeyCode.D)) x = 1;
        if (Input.GetKey(KeyCode.S)) y = -1f;
        if (Input.GetKey(KeyCode.W)) y = 1f;

        return new Vector2(x, y);

    }

    private States GetNewState()
    {
        switch (currentState)
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
            case States.InKnockack:
                var dist = (((Vector2)transform.position) - knockbackEndpoint).sqrMagnitude;
                if (dist <= 0.1f * 0.1f)
                {
                    knockbackEndpoint = Vector2.zero;
                    return States.Idle;
                }
                return States.InKnockack;
        }
    }
    
    private void DoStateLogic()
    {
        // Is Attacking Logic
        if(isAttacking >= 0f)
        {
            isAttacking += Time.deltaTime;
            if (isAttacking >= myMobInfo.AttackInteruptionDuration) isAttacking = -1f;
        }
        if(canAttack > 0f)
        {
            canAttack += Time.deltaTime;
            if (canAttack >= myMobInfo.AttackCooldown) canAttack = 0f;
        }

        // MOVEMENT
        switch(currentState)
        {
            case States.Idle:
            case States.Walking:
                inputVector = GetInputVector();
                if(Input.GetMouseButton(0) && canAttack == 0)
                {
                    Shoot();
                }

                break;
            case States.InKnockack:
                var half = (knockbackEndpoint - (Vector2)transform.position)/2f;
                myRigidbody.MovePosition(transform.position + ((Vector3)half));
                break;

        }
    }

    private void Shoot()
    {
        currentState = States.Attacking;
        isAttacking = 0f;
        canAttack += Time.deltaTime;
        GameObject go = Pooler.Instance.Get("PlayerBullet");
        //float yDiff = myMuzzlePos.position.y - transform.position.y;
        //Vector2 truePos = new Vector2(myMuzzlePos.position.x, myMuzzlePos.position.y + yDiff);

        //Vector2 dir = (truePos - (Vector2) transform.position).normalized;
        Vector2 dir = (MyUtils.CameraUtils.GetMouseWorldPosition() - transform.position).normalized;
        go.GetComponent<IProjectile>()?.Setup(myMuzzlePos.position, dir );
        //GetKnockback(-1f * 0.5f * dir + (Vector2)transform.position);
    }

    // ----------------------------------------------------------- INTERFACE FUNCTIONS -----------------------------------------------------------

    public void GetKnockback(Vector2 knockbackEndpoint)
    {
        currentState = States.InKnockack;
        var end = knockbackEndpoint;
        var hit = Physics2D.CircleCast(transform.position, mySpriteRenderer.bounds.extents.x, Vector3.forward, 1f, myMobInfo.knockbackLayerMask);
        if (hit)
        {
            Vector2 directionToSelf = ((Vector2)transform.position - hit.point).normalized;
            float signX = Mathf.Sign(directionToSelf.x);
            float signY = Mathf.Sign(directionToSelf.y);
            end.x = hit.point.x + (myCollider.bounds.extents.x * signX);
            end.y = hit.point.y + (myCollider.bounds.extents.y * signY);
        }
        this.knockbackEndpoint = end * 1.3f;

    }




}
