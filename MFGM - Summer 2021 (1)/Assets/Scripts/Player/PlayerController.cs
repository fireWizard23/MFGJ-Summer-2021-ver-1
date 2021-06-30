using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MyClasses;

public class PlayerController : MonoBehaviour, IVelocityRotated, IKnockbackeable
{
    // ----------------------------------------------------------- TOP -----------------------------------------------------------

    private enum States { Idle, Walking, Attacking, InKnockback}

    // ----------------------------------------------------------- TO SERIALIZED -----------------------------------------------------------
    [SerializeField] private MobSO myMobInfo;
    [SerializeField] private WeaponSO myWeaponInfo;
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
    private CapsuleCollider2D myCollider;
    private Transform myMuzzlePos;

    // ----------------------------------------------------------- UNITY FUNCTIONS -----------------------------------------------------------

    void Start()
    {
        Cursor.visible = false;
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CapsuleCollider2D>();
        myMuzzlePos = transform.Find("Muzzle");
    }

    // Update is called once per frame
    void Update()
    {
        currentState = GetNewState();
        DoStateLogic();
    }

    private void FixedUpdate()
    {
        if(currentState != States.Attacking && currentState != States.InKnockback)
        {
            velocity = Vector2.Lerp(velocity, inputVector * myMobInfo.MovementSpeed, myMobInfo.MovementLerpWeight);
            myRigidbody.velocity = velocity;
        }
        else if(currentState != States.InKnockback)
        {
            velocity = Vector2.zero;
            myRigidbody.velocity = velocity;

        }
        if (Mathf.Abs(myRigidbody.velocity.x) < 0.1f && Mathf.Abs(myRigidbody.velocity.y) < 0.1f) myRigidbody.velocity = Vector2.zero;
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

        Vector2 output = new Vector2(x, y);
        if (x != 0 && y != 0) output.Normalize();
        return output;

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
            case States.InKnockback:
                if(myRigidbody.velocity == Vector2.zero)
                {
                    if (isAttacking > 0) return States.Attacking;
                    knockbackEndpoint = Vector2.zero;
                    return States.Idle;
                }
                return States.InKnockback;
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
            

        }
    }

    private void Shoot()
    {
        currentState = States.Attacking;
        isAttacking = 0f;
        canAttack += Time.deltaTime;
        GameObject go = Pooler.Instance.Get("PlayerBullet");
        Vector2 dir = ((Vector2)(MyUtils.CameraUtils.MousePosition - transform.position)).normalized;
        go.GetComponent<IProjectile>()?.Setup(myMuzzlePos.position, dir );
        GetKnockback(-dir, myWeaponInfo.FireKnockback);
    }

    // ----------------------------------------------------------- INTERFACE FUNCTIONS -----------------------------------------------------------

    public void GetKnockback(Vector2 dir, float scale,float duration=0.1f)
    {
        duration = Mathf.Clamp(duration, 0, 1);
        scale = Mathf.Clamp(scale, 0, 5);
        currentState = States.InKnockback;

        myRigidbody.AddForce(dir.normalized * scale * 10f, ForceMode2D.Impulse);
        MyUtils.Time.SetTimeout(() => {
            myRigidbody.velocity = Vector2.zero;
        }, duration, this);

    }




}
