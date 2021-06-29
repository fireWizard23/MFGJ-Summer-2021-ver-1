using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyClasses : MonoBehaviour
{
    public interface IVelocityRotated
    {
        public Vector2 Velocity { get; }
    }
}
