using GLTFast.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : InteractablObject
{
    public ItemData itemData;       // �� ������Ʈ�� ���� ������ ������
    public int amount = 1;          // ������ ����

  
    public override void Interact()
    {
        base.Interact();

        if (InventoryManager.Instance != null)     // �κ��丮 �Ŵ����� ������
        {
            bool added = InventoryManager.Instance.AddItem(itemData, amount); // �κ��丮�� ������ �߰� �õ�

            if (added)                             // �����ϸ� ������Ʈ ����
            {
                Destroy(gameObject);
            }
        }
    }
}