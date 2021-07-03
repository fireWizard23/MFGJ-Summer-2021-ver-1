using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Scriptables/NPC", fileName ="NewNPC")]
public class NPC_SO : MobSO
{
    [Space] public MobBase myMob;

    [Space]
    public LayerMask AttackMask = 1;

    [Space]
    public float NoticeRadius = 3f;
    public LayerMask NoticeMask = 1;
    public float NoticeMultiplier = 1f;

    [Space] public float StopDistance = 0.5f;




}
