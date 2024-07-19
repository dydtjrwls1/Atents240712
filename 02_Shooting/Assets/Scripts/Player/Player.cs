using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    // 총알 발사 간격
    public float fireInterval = 0.5f;

    //총알 프리팹
    public GameObject bulletPrefab;

    // 총알 발사 이펙트 게임 오브젝트
    GameObject fireFlash;

    // 이동속도 
    public float moveSpeed = 10.0f;

    PlayerInputActions playerInputActions;

    Vector3 inputDirection;

    // 애니메이터 컴포넌트를 저장할 변수.
    Animator animator;

    readonly int InputY_String = Animator.StringToHash("InputY");

    // 총알 발사 위치
    Transform fireTransform;

    // 총알 발사용 코루틴
    IEnumerator fireCoroutine;

    // 총알 발사 이펙트가 보이는 시간.
    WaitForSeconds flashWait;

    private void Awake()
    {
        animator = GetComponent<Animator>(); // 자신과 같은 게임오브젝트 내부에 있는 컴포넌트 찾기

        playerInputActions = new PlayerInputActions();

        // 총알 발사용 트랜스폼
        fireTransform = transform.GetChild(0); // 첫 번째 자식 찾기
        fireFlash = transform.GetChild(1).gameObject; // 두 번째 자식을 찾아서 그 자식의 게임 오브젝트 저장하기

        fireCoroutine = FireCoroutine();

        flashWait = new WaitForSeconds(0.1f);
    }

    private void OnEnable()
    {
        playerInputActions.Enable();
        playerInputActions.Player.Move.performed += Move;
        playerInputActions.Player.Move.canceled += MoveCanceled;
        playerInputActions.Player.Fire.performed += OnFireStart;
        playerInputActions.Player.Fire.canceled += OnFireEnd;
        
    }

    private void OnDisable()
    {
        playerInputActions.Player.Fire.canceled -= OnFireEnd;
        playerInputActions.Player.Fire.performed -= OnFireStart;
        playerInputActions.Player.Move.canceled -= MoveCanceled;
        playerInputActions.Player.Move.performed -= Move;
        playerInputActions.Disable();
    }

    
    private void OnFireStart(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        StartCoroutine(fireCoroutine);
    }

    private void OnFireEnd(UnityEngine.InputSystem.InputAction.CallbackContext _)
    {
        // Debug.Log("Stop Fire");
        // StopAllCoroutines(); // 모든 코루틴 정지 시키기
        StopCoroutine(fireCoroutine);
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
        // 플래시 이펙트 잠깐 켜기
        StartCoroutine(FlashEffect());

        // Instantiate(bulletPrefab, transform);  자식은 부모를 따라다니므로 이렇게 하면 안됨
        // Instantiate(bulletPrefab, transform.position, Quaternion.identity);  총알이 비행기 가운데에서 생성됨
        // Instantiate(bulletPrefab, transform.position + Vector3.right, Quaternion.identity);  총알 발사 위치를 확인하기 힘들다
        Instantiate(bulletPrefab, fireTransform.position, fireTransform.rotation); // 총알을 fireTransform 의 위치와 회전에 따라 생성한다.
    }

    IEnumerator FireCoroutine()
    {
        while (true)
        {
            // Debug.Log("Fire!");
            Fire();
            yield return new WaitForSeconds(fireInterval); // fireInterval 초 만큼 기다렸다가 다시 시작하기
        }

        // 프레임 종료시까지 대기
        // yield return null;
        // yield return new WaitForEndOfFrame();
    }

    IEnumerator FlashEffect()
    {
        fireFlash.SetActive(true); // 게임 오브젝트 활성화 하기

        yield return flashWait;

        fireFlash.SetActive(false);
    }
}
