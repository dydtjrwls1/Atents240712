using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissilePool : ObjectPool<BossMissile>
{
    // Enemy 지만 point 가 없기 때문에 EnemyObjectPool을 쓰지 않는다.
}
