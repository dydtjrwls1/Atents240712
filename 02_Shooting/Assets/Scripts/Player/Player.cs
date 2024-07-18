using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    //총알 프리팹
    public GameObject bulletPrefab;

    // 이동속도 
    public float moveSpeed = 10.0f;

    PlayerInputActions playerInputActions;

    Vector3 inputDirection;
    // 애니메이터 컴포넌트를 저장할 변수.
    Animator animator;

    readonly int InputY_String = Animator.StringToHash("InputY");

    Transform fireTransform;

    private void Awake()
    {
        animator = GetComponent<Animator>(); // 자신과 같은 게임오브젝트 내부에 있는 컴포넌트 찾기

        playerInputActions = new PlayerInputActions();

        // 총알 발사용 트랜스폼
        fireTransform = transform.GetChild(0);
    }

    private void OnEnable()
    {
        playerInputActions.Enable();
        playerInputActions.Player.Move.performed += Move;
        playerInputActions.Player.Move.canceled += MoveCanceled;
        playerInputActions.Player.Fire.performed += OnFire;
        
    }

    private void OnDisable()
    {
        playerInputActions.Player.Fire.performed -= OnFire;
        playerInputActions.Player.Move.canceled -= MoveCanceled;
        playerInputActions.Player.Move.performed -= Move;
        playerInputActions.Disable();
    }

    
    private void OnFire(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        Fire();
        Debug.Log("Fire!!");
    }

    private void Move(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        inputDirection = (Vector3)context.ReadValue<Vector2>();

        // animator.SetFloat("InputY", inputDirection.y); 한번 이동할 때마다 "InputY" 라는 문자열이 메모리에 할당되서 비효율 적이다. 그리고 문자열 비교임.
        animator.SetFloat(InputY_String, inputDirection.y);
    }



    private void MoveCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        inputDirection = Vector3.zero;
    }

    private void Update()
    {
        transform.position += Time.deltaTime * moveSpeed * inputDirection; // 초당 inputDirection 방향으로 moveSpeed 의 속도로 이동한다.
        // transform.position += (inputDirection * moveSpeed * Time.deltaTime); 위는 4번곱하고 아래는 6번 곱한다! vector * float * float 이기 때문에! 매우 중요!

    }

    void Fire()
    {
        // Instantiate(bulletPrefab, transform);  자식은 부모를 따라다니므로 이렇게 하면 안됨
        // Instantiate(bulletPrefab, transform.position, Quaternion.identity);  총알이 비행기 가운데에서 생성됨
        // Instantiate(bulletPrefab, transform.position + Vector3.right, Quaternion.identity);  총알 발사 위치를 확인하기 힘들다
        Instantiate(bulletPrefab, fireTransform.position, fireTransform.rotation); // 총알을 fireTransform 의 위치와 회전에 따라 생성한다.
    }
}
