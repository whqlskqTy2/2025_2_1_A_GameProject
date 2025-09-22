using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static InteractablObject;

public class CoinItem : InteractablObject
{
    [Header("동전 설정")]
    public int coinValue = 10;

    // Unity 메세지 | 참조 3개
    protected override void Start()
    {
        base.Start();
        objectName = "동전";
        interactionText = "[E] 동전 획득";
        interactionType = InteractionType.Item;
    }

    // 참조 3개
    protected override void CollectItem()
    {
        transform.Rotate(Vector3.up * 360f);
        Destroy(gameObject, 0.5f);
    }

}
