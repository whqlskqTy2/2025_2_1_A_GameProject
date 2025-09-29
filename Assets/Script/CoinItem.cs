using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static InteractablObject;

public class CoinItem : InteractablObject
{
    [Header("���� ����")]
    public int coinValue = 10;
    public string questTag = "Coin";

    // Unity �޼��� | ���� 3��
    protected override void Start()
    {
        base.Start();
        objectName = "����";
        interactionText = "[E] ���� ȹ��";
        interactionType = InteractionType.Item;
    }

   
    protected override void CollectItem()
    {

        if (QuestManager.Instance != null)
        {
            QuestManager.Instance.AddCollectProgress(questTag);
        }
        transform.Rotate(Vector3.up * 180f);
        Destroy(gameObject, 0.5f);
    }

}
