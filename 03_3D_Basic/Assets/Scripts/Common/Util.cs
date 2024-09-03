using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util 
{
    // 피셔-에이츠 알고리즘으로 구현한 셔플
    public static void Shuffle<T> (T[] source)
    {
        for(int i = source.Length - 1; i > -1; i--) 
        {
            int index = Random.Range(0, i + 1); 
            (source[index], source[i]) = (source[i], source[index]); // 스왑하기
        }
    }
}
