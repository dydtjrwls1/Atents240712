using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WilsonCell : CellBase
{
    // 미로에 포함된 셀인지 확인하고 설정하기 위한 변수
    public bool isMazeMember;

    public WilsonCell next;

    public WilsonCell(int x, int y) : base(x, y)
    {

    }

}
