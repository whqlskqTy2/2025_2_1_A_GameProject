using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("�̵� ����")]
    public float walkSpped = 3f;
    public float runSpeed = 6f;
    public float rotationSpeed = 10;

    [Header("���� ����")]
    public float attackDruation = 0.8f;
    public bool canMoveWhileAttacking = false;

    [Header("������Ʈ")]
    public Animator animator;

    private CharacterController controller;
    private Camera playerCamera;

    //���� ����
    private float currentSpeed;
    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = Camera.main;
    }

  
    void Update()
    {
        HandleMovement();
        UpdateAnimator();
    }


    void HandleMovement()                               //�̵� �Լ� ����
    {
        float horizontal = Input.GetAxis("Horizontal");
        float verical = Input.GetAxis("Vertical");

        if (horizontal != 0 || verical != 0)            //������ �ϳ��� �Է��� ������
        {
            //ī�޶� ���� ������ �������� �ǰ� ����
            Vector3 cameraForward = playerCamera.transform.forward;
            Vector3 cameraRight = playerCamera.transform.right;
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 moveDirection = cameraForward * verical + cameraRight * horizontal;   //�̵� ���� ����

            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = runSpeed;
            }
            else
            {
                currentSpeed = walkSpped;
            }

            controller.Move(moveDirection * currentSpeed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            currentSpeed = 0;
        }
    }
    void UpdateAnimator()
    {
        float animatorSpeed = Mathf.Clamp01(currentSpeed / runSpeed);
        animator.SetFloat("speed", animatorSpeed);
    }

}
