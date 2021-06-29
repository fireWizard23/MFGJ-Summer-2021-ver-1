using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptables/MobSO", fileName ="NewMob")]
public class MobSO : ScriptableObject
{
    [Space]
    public float GivenHealth = 100f;
    public float MovementSpeed = 3f;
    [Range(0.01f, 0.99f)]
    public float MovementLerpWeight = 0.1f;

    [Header("Attack"), Space]
    public float AttackDamage = 5f;
    [Range(0f, 19.999f)]
    public float AttackCooldown = 0.5f;



}
