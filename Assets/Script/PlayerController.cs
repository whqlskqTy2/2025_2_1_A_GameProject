using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동 설정")]
    public float walkSpped = 3f;
    public float runSpeed = 6f;
    public float rotationSpeed = 10;

    [Header("점프 설정")]
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float landingDuration = 0.3f;


    [Header("공격 설정")]
    public float attackDruation = 0.8f;
    public bool canMoveWhileAttacking = false;

    [Header("컴포넌트")]
    public Animator animator;

    private CharacterController controller;
    private Camera playerCamera;

    //현재 상태
    private float currentSpeed;
    private bool isAttacking = false;
    private bool isLanding = false;
    private float landingTimer;

    private Vector3 velocity;
    private bool isGrounded;
    private bool wasGrounded;
    private float attackTimer;

    private bool isUIMode = false;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = Camera.main;
    }

  
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleCursorLock();
        }

        if (!isUIMode)                  //UI 모드가 아닐때 
        {
            CheckGrounded();
            HandleLanding();
            HandleMovement();
            HandleJump();
            HandleAttack();
            UpdateAnimator();
        }

       
    }


    void HandleMovement()                               //이동 함수 제작
    {
        if ((isAttacking && !canMoveWhileAttacking) || isLanding)
        {
            currentSpeed = 0;
            return;
        }


        float horizontal = Input.GetAxis("Horizontal");
        float verical = Input.GetAxis("Vertical");

        if (horizontal != 0 || verical != 0)            //둘중이 하나라도 입력이 있을때
        {
            //카메라가 보는 방향이 앞쪽으로 되게 설정
            Vector3 cameraForward = playerCamera.transform.forward;
            Vector3 cameraRight = playerCamera.transform.right;
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 moveDirection = cameraForward * verical + cameraRight * horizontal;   //이동 방향 설정

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
        animator.SetBool("isGrounded", isGrounded);

        bool isFalling = !isGrounded && velocity.y < -0.1f;
        animator.SetBool("isFalling", isFalling);
        animator.SetBool("isLanding", isLanding);

    }

    void CheckGrounded()
    {
        // 이전 상태 저장
        wasGrounded = isGrounded;
        isGrounded = controller.isGrounded;   // 캐릭터 컨트롤러에서 받아온다.

        // 땅에서 떨어졌을 때 (지금 프레임은 땅이 아니고, 이전 프레임은 땅)
        if (!isGrounded && wasGrounded)
        {
            Debug.Log("떨어지기 시작");
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;

            // 착지 모션 트리거 및 착지 상태 시작
            if (!wasGrounded && animator != null)
            {
                //animator.SetTrigger("landTrigger");
                isLanding = true;
                landingTimer = landingDuration;
                Debug.Log("착지");
            }
        }
    }

    void HandleLanding()
    {
        if (isLanding)
        {
            landingTimer -= Time.deltaTime;
            if (landingTimer <= 0)
            {
                isLanding = false;
            }
        }
    }

    void HandleAttack()
    {
        if (isAttacking)    // 공격 중일때
        {
            attackTimer -= Time.deltaTime;   // 타이머를 감소 시킨다.
            if (attackTimer <= 0)
            {
                isAttacking = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && !isAttacking)   // 공격중이 아닐때 키를 누르면 공격
        {
            isAttacking = true;                // 공격중 표시
            attackTimer = attackDruation;      // 타이머 리필

            if (animator != null)
            {
                animator.SetTrigger("attackTrigger");
            }
        }
    }


    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)    // 땅 위에 있을때만 점프를 할 수 있다.
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            if (animator != null)
            {
                animator.SetTrigger("jumpTrigger");
            }
        }

        if (!isGrounded)    // 땅에 있지 않을 경우 중력 적용
        {
            velocity.y += gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    public void SetCursorLock(bool lockCursor)   // 마우스 락 설정 함수
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isUIMode = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isUIMode = true;
        }
    }
    public void ToggleCursorLock()
    {
        bool shouldLock = Cursor.lockState != CursorLockMode.Locked;
        SetCursorLock(shouldLock);
    }

    // 참조 0개
    public void SetUIMode(bool uiMode)
    {
        SetCursorLock(!uiMode);
    }


}
