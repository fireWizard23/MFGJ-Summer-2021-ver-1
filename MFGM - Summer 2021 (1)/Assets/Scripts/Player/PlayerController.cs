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

    // ----------------------------------------------------------- PUBLIC FIELDS -----------------------------------------------------------
    public Vector2 Velocity { get { return velocity; } }

    // ----------------------------------------------------------- Components -----------------------------------------------------------
    private Rigidbody2D myRigidbody;


    // ----------------------------------------------------------- UNITY FUNCTIONS -----------------------------------------------------------

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVector = GetInputVector();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            var newBullet = Instantiate(myBullet);
            var proj  =newBullet.GetComponent<MyClasses.IProjectile>();
            proj.Setup(transform.position, Vector2.right);

        }
    }

    private void FixedUpdate()
    {
        velocity = Vector2.Lerp(velocity, inputVector * myMobInfo.MovementSpeed, myMobInfo.MovementLerpWeight);
        myRigidbody.velocity = velocity;
    }

    // ----------------------------------------------------------- MY FUNCTIONS -----------------------------------------------------------

    private Vector2 GetInputVector()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        return MyUtils.Pooling.PVector2.GetVector(x, y);

    }




}
