using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MyClasses;


public class VelocityRotator : MonoBehaviour
{
    [SerializeField] private float RotSpeed = 5f;
    [SerializeField] private float AngleOffset = 90f;

    private IVelocityRotated toRotate;

    void Start()
    {
        toRotate = GetComponent<IVelocityRotated>();
    }


    void FixedUpdate()
    {
        if (toRotate != null && toRotate.Velocity != Vector2.zero)
        {
            Vector2 v = toRotate.Velocity;
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle - AngleOffset, Vector3.forward), Time.fixedDeltaTime * RotSpeed);
        }
    }
}
