using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 인터페이스
// 무제한 상속 가능하다
// 모든 멤버가 public 이다
// 멤버 변수가 없다(const 상수는 가능)
// 멤버 함수는 선언만 있다
// 인터페이스를 상속받은 클래스는 반드시 멤버 함수를 구현해야 한다.
public interface IInteractable
{
    bool CanUse // 지금 사용 가능한 지를 확인하는 프로퍼티가 있다고 선언
    {
        get;
    }

    void Use(); // 사용하는 기능이 있다고 선언
} 