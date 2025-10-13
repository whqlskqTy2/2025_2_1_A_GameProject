using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;      // �̱��� ����

    [Header("Inventory Setting")]
    public int inventorySize = 20;                // �κ��丮 ���� ����
    public GameObject inventoryUI;                // �κ��丮 UI �г�
    public Transform itemSlotParent;              // ���Ե��� �� �θ� ������Ʈ
    public GameObject itemSlotPrefab;             // ���� ������

    [Header("Input")]
    public KeyCode inventoryKey = KeyCode.I;      // �κ��丮 ���� Ű
    private List<InventorySlot> slots = new List<InventorySlot>();  // ��� ���� ����Ʈ
    private bool isInventoryOpen = false;         // �κ��丮�� �����ִ��� Ȯ��

    private void Awake()
    {
        if (Instance == null) Instance = this;    // �̱��� ����
        else Destroy(gameObject);
    }

    void Start()
    {
        CreateInventorySlots();
        inventoryUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            ToggleInventory();
        }
    }
    // �κ��丮 ���Ե��� �����ϴ� �Լ�
    void CreateInventorySlots()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            // ���������� ���� ����
            GameObject slotObj = Instantiate(itemSlotPrefab, itemSlotParent);
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            slots.Add(slot); // ����Ʈ�� �߰�
        }
    }

    // �κ��丮 UI�� ���ų� �ݴ� �Լ�
    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryUI.SetActive(isInventoryOpen);

        if (isInventoryOpen)
        {
            // �κ��丮�� ������ Ŀ�� ���̱�
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            // �κ��丮�� ������ Ŀ�� �����
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public bool AddItem(ItemData item, int amount = 1)
    {
        foreach (InventorySlot slot in slots)                       // 1�ܰ� : �̹� �ִ� �����ۿ� �߰� �õ�(����)
        {
            if (slot.item == item && slot.amount < item.maxStack)   // ���� �������̰� �ִ� ���ú��� ������
            {
                int spaceLeft = item.maxStack - slot.amount;       // ���� ���� ���
                int amountToAdd = Mathf.Min(amount, spaceLeft);    // �߰��� ����
                slot.AddAmount(amountToAdd);
                amount -= amountToAdd;

                if (amount <= 0)                                    // ��� �߰������� ����
                {
                    return true;
                }
            }
        }

        foreach (InventorySlot slot in slots)                       // 2�ܰ� : �� ���Կ� �߰�
        {
            if (slot.item == null)                                 // �� ���� ã��
            {
                slot.SetItem(item, amount);
                return true;
            }
        }

        Debug.Log("�κ��丮�� ���� ��");
        return false;
    }

    public void RemoveItem(ItemData item, int amount = 1)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.item == item)
            {
                slot.RemoveAmount(amount);
                return;
            }
        }
    }

    public int GetItemCount(ItemData item)
    {
        int count = 0;
        foreach (InventorySlot slot in slots)
        {
            if (slot.item == item)
            {
                count += slot.amount;
            }
        }
        return count;
    }


}
