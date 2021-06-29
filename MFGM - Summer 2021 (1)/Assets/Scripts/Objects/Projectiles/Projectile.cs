using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    public ProjectileSO myProjectileInfo;

    protected Rigidbody2D myRigidbody;

    protected virtual void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        
    }

    protected Vector2 direction = Vector2.zero;


    public virtual void Setup(Vector2 spawnPos, Vector2 direction)
    {
        transform.position = spawnPos;
        this.direction = direction;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    protected virtual void Move()
    {
        myRigidbody.AddForce(direction * myProjectileInfo.MoveSpeed);
    }




}
