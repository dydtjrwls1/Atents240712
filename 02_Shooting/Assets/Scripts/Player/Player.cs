using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    // 총알 발사 간격
    public float fireInterval = 0.5f;

    //총알 프리팹
    public GameObject bulletPrefab;

    // 이동속도 
    public float moveSpeed = 10.0f;

    public Vector3 inputDirection;

    public const float FireAngle = 30.0f;

    PlayerInputActions playerInputActions;

    // 총알 발사 이펙트 게임 오브젝트
    GameObject fireFlash;

    // 애니메이터 컴포넌트를 저장할 변수.
    Animator animator;

    readonly int InputY_String = Animator.StringToHash("InputY");

    // 총알 발사 위치
    Transform[] fireTransform;

    // 총알 발사용 코루틴
    IEnumerator fireCoroutine;

    // 총알 발사 이펙트가 보이는 시간.
    WaitForSeconds flashWait;

    Rigidbody2D rigid;

    private const int MinPower = 1;
    private const int MaxPower = 3;

    

    int power = 1;

    int Power
    {
        get => power;
        set
        {
            // 변경이 있을 때만 처리
            if (power != value)
            {
                power = value;

                // power는 MinPower 와 MaxPower 사이
                if(power > MaxPower)
                {
                    ScoreText scoreText = GameManager.Instance.ScoreTextUI;
                    scoreText.AddScore(PowerUp.BonusPoint);
                }
                
                power = Mathf.Clamp(power, MinPower, MaxPower);

                RefreshFireAngles();

            }
        }
    }


    private void Awake()
    {
        animator = GetComponent<Animator>(); // 자신과 같은 게임오브젝트 내부에 있는 컴포넌트 찾기
        rigid = GetComponent<Rigidbody2D>();

        playerInputActions = new PlayerInputActions();

        // 총알 발사용 트랜스폼
        Transform fireRoot = transform.GetChild(0);
        fireTransform = new Transform[fireRoot.childCount]; // 첫 번째 자식 찾기
        for (int i = 0; i < fireRoot.childCount; i++)
        {
            fireTransform[i] = fireRoot.GetChild(i);
        }
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

    private void FixedUpdate()
    {
        // 항상 일정 시간 간격으로 호출된다.
        // Debug.Log(Time.fixedDeltaTime); // 0.02초
        
        rigid.MovePosition(rigid.position + Time.fixedDeltaTime * moveSpeed * (Vector2)inputDirection);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            Power++;
            collision.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if(collision.gameObject.tag == "Enemy") string 비교 절대 금지!
        if (collision.gameObject.CompareTag("Enemy")) // 이쪽을 권장. 위 코딩에 비해 가비지가 덜 생성된다. 즉, 메모리를 덜 사용한다. 생성되는 코드도 훨씬 빠르게 구현되어 있다.
        {
            Debug.Log("적과 부딪혔다.");
        }
        
    }



    //private void Update()
    //{
    //    // 업데이트 함수가 호출되는 시간 간격(Time.deltaTime)은 매번 다르다.
    //    // transform.position += Time.fixedDeltaTime * moveSpeed * inputDirection; // 초당 inputDirection 방향으로 moveSpeed 의 속도로 이동한다. // 한번은 파고 들어간다.
    //    // transform.position += (inputDirection * moveSpeed * Time.deltaTime); 위는 4번곱하고 아래는 6번 곱한다! vector * float * float 이기 때문에! 매우 중요!
    //}

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

   


   

    void Fire(Transform fire)
    {
        // 플래시 이펙트 잠깐 켜기
        StartCoroutine(FlashEffect());

        // Instantiate(bulletPrefab, transform);  자식은 부모를 따라다니므로 이렇게 하면 안됨
        // Instantiate(bulletPrefab, transform.position, Quaternion.identity);  총알이 비행기 가운데에서 생성됨
        // Instantiate(bulletPrefab, transform.position + Vector3.right, Quaternion.identity);  총알 발사 위치를 확인하기 힘들다
        // Instantiate(bulletPrefab, fireTransform.position, fireTransform.rotation); // 총알을 fireTransform 의 위치와 회전에 따라 생성한다.
        Factory.Instance.GetBullet(fire.position, fire.rotation.eulerAngles.z);
    }

    IEnumerator FireCoroutine()
    {
        while (true)
        {
            // Debug.Log("Fire!");
            for (int i = 0; i < Power; i++)
            {
                Fire(fireTransform[i]);
            }
            
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

    void RefreshFireAngles()
    {
        for (int i = 0; i < Power; i++)
        {
            if (i < Power)
            {
                // 1 : 0도
                // 2 : 15도, -15도
                // 3 : 30도, 0도, -30도
                float stratAngle = (Power - 1) * (FireAngle * 0.5f);
                float angleDelta = i * -FireAngle;
                fireTransform[i].rotation = Quaternion.Euler(0, 0, stratAngle + angleDelta);

                // 약간 앞으로 옮기기
                fireTransform[i].localPosition = Vector3.zero;
                fireTransform[i].Translate(0.5f, 0, 0);
                
                fireTransform[i].gameObject.SetActive(true);
            }
            else
            {
                fireTransform[i].gameObject.SetActive(false);
            }
        }
    }
}
