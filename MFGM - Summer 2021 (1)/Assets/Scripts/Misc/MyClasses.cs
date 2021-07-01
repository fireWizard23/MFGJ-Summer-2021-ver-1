using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyClasses : MonoBehaviour
{
    public interface IVelocityRotated
    {
        public Vector2 Velocity { get; }
    }

    public interface IProjectile
    {
        public void Setup(Vector2 spawnPos, Vector2 direction);
    }

    public interface IPoolActivatable
    {
        public void PoolActivate(bool a);
    }

    public interface IKnockbackeable
    {
        public void GetKnockback(Vector2 direction, float length, float duration=0.1f);
        
    }

    public interface IKnockbacker
    {
    }





}
