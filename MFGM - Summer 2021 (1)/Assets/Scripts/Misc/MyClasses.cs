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



}
