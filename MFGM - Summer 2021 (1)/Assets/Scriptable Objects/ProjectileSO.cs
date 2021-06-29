using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/ProjectileSO", fileName = "NewProjectile")]
public class ProjectileSO : ScriptableObject
{
    public float MoveSpeed = 20f;
    public float AttackDamage = 6f;

    [Space]
    public float LifeTime = 5f;




}
