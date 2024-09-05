using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCamController : MonoBehaviour
{
    // 카트 최대 속도(시작 속도)
    public float maxCartSpeed = 10.0f;
    // 카트 최소 속도
    public float minCartSpeed = 3.0f;
    // 카트가 최대에서 최저로 가는 시간
    public float speedDecreaseDuration = 3.0f;

    // 카메라 속도 변환 커브
    public AnimationCurve cartSpeedCurve;

    // 누적 시간
    float elapsedTime = 0.0f;

    // 카메라 시작 여부
    bool isStart = false;

    CinemachineVirtualCamera vcam;
    CinemachineDollyCart cart;
    Player player;
    Transform playerCameraRoot;

    private void Awake()
    {
        vcam = GetComponentInChildren<CinemachineVirtualCamera>();
        cart = GetComponentInChildren<CinemachineDollyCart>();
    }
    private void Start()
    {
        player = GameManager.Instance.Player;
        playerCameraRoot = player.transform.GetChild(8);
        if(player != null)
        {
            player.onDie += DeathCamStart;
        }
    }

    private void Update()
    {
        if(isStart) // 플레이어가 죽었다는 신호가 들어오면
        {
            transform.position = playerCameraRoot.position; // 카메라를 플레이어 카메라 위치로 옮기기

            elapsedTime += Time.deltaTime; // 시간 누적 시작

            float ratio = cartSpeedCurve.Evaluate(elapsedTime / speedDecreaseDuration);
            cart.m_Speed = minCartSpeed + (maxCartSpeed - minCartSpeed) * ratio; // 카트 속도 조절
        }
    }

    // 플레이어 사망하면 실행
    private void DeathCamStart()
    {
        isStart = true;
        vcam.Priority = 100;
        cart.m_Speed = maxCartSpeed;
        cart.m_Position = 0;
        elapsedTime = 0.0f;
    }
}
