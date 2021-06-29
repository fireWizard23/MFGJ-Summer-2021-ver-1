using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{

    [SerializeField, Range(0.01f, 1f)] private float LerpWeight = 0.5f;

    void Update()
    {
        Vector2 mouse = MyUtils.CameraUtils.GetMouseWorldPosition();
        Vector2 mouseDir = (mouse - (Vector2)transform.position).normalized;
        float angle = MyUtils.VectorUtils.GetAngle(mouseDir);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), LerpWeight);
    }



}
