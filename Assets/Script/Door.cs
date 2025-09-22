using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractablObject
{
    [Header("�� ����")]
    public bool isOpen = false;
    public Vector3 openPositon;
    public float openSpeed = 2f;

    private Vector3 closedPosition;

    protected override void Start()
    {
        base.Start();   // ���� ��� ���� ��ŸƮ �Լ��� �ѹ� ���� ��Ų��.
        objectName = "��";
        interactionText = "[E] �� ����";
        interactionType = InteractionType.Builing;

        closedPosition = transform.position;
        openPositon = closedPosition + Vector3.right * 3f;   // ���������� 3���� �̵�
    }

    protected override void AccessBuilding()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            interactionText = "[E] �� �ݱ�";
            StartCoroutine(MoveDoor(openPositon));
        }
        else
        {
            interactionText = "[E] �� ����";
            StartCoroutine(MoveDoor(closedPosition));
        }
    }

    IEnumerator MoveDoor(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, openSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;
    }


    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }
}
