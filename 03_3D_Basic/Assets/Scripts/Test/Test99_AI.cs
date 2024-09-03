using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Test99_AI : TestBase
{
    public NavMeshSurface surface;
    public NavMeshAgent agent;

    AsyncOperation navAsync;

    protected override void Test1_performed(InputAction.CallbackContext _)
    {
        StartCoroutine(UpdateSurface());
    }

    protected override void LClick_performed(InputAction.CallbackContext context)
    {
        // Vector2 screen = context.ReadValue<Vector2>();
        Vector2 screen = Mouse.current.position.value;

        Ray ray = Camera.main.ScreenPointToRay(screen);
        if(Physics.Raycast(ray, out RaycastHit hitInfo, 1000, LayerMask.GetMask("Ground")))
        {
            agent.SetDestination(hitInfo.point);
        }
    }

    IEnumerator UpdateSurface()
    {
        navAsync = surface.UpdateNavMesh(surface.navMeshData);
        while(!navAsync.isDone) // 비동기 명령이 끝날 때 까지 반복
        {
            // navAsync.progress;
            yield return null;
        }
        Debug.Log("업데이트 완료");
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        // agent.remainingDistance; 남아있는 거리
        // agent.pathPending;       길찾기 계산이 끝났는지 아닌지 알려주는 프로퍼티 (true 면 계산 중, false 면 계산 끝)
    }
}
