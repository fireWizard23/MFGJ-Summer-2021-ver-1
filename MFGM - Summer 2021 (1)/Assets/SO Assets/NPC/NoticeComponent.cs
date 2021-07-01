using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NoticeComponent : MonoBehaviour
{
    public NPC_SO myInfo;

    public event Action<GameObject> OnMobEnterRadius;

    void Update()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, myInfo.NoticeRadius, Vector3.forward, 1f, myInfo.NoticeMask);
        foreach(RaycastHit2D hit in hits)
        {
            OnMobEnterRadius?.Invoke(hit.collider.gameObject);
        }

    }

}
