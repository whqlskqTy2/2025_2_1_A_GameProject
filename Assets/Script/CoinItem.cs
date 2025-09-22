using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static InteractablObject;

public class CoinItem : InteractablObject
{
    [Header("���� ����")]
    public int coinValue = 10;

    // Unity �޼��� | ���� 3��
    protected override void Start()
    {
        base.Start();
        objectName = "����";
        interactionText = "[E] ���� ȹ��";
        interactionType = InteractionType.Item;
    }

    // ���� 3��
    protected override void CollectItem()
    {
        transform.Rotate(Vector3.up * 360f);
        Destroy(gameObject, 0.5f);
    }

}
