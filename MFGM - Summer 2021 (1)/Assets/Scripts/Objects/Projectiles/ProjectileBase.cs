using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour, MyClasses.IProjectile, MyClasses.IPoolActivatable
{

    public ProjectileSO myProjectileInfo;

    protected Rigidbody2D myRigidbody;

    protected virtual void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        Move();
    }

    protected Vector2 direction = Vector2.zero;


    public virtual void Setup(Vector2 spawnPos, Vector2 direction)
    {
        transform.position = spawnPos;
        this.direction = direction.normalized;
        MyUtils.Time.SetTimeout(() => {
            if (gameObject.activeInHierarchy) DestroySelf();
        }, myProjectileInfo.LifeTime, this);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        DestroySelf();
    }

    protected virtual void Move()
    {
        myRigidbody.velocity = (direction * myProjectileInfo.MoveSpeed);
    }

    private void DestroySelf()
    {
        gameObject.SetActive(false);
    }

    public void PoolActivate(bool a)
    {
        gameObject.SetActive(a);
    }
}
