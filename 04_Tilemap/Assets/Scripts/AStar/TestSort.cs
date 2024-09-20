using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSort : IComparable<TestSort>
{
    int a;
    float b;
    string c;

    public int A => a;
    public float B => b;
    public string C => c;

    public TestSort(int a, float b, string c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }

    public int CompareTo(TestSort other)
    {
        if(other == null) return -1;

        return a.CompareTo(other.A);
    }

    public override bool Equals(object obj)
    {
        return obj is TestSort sort && a == sort.A;
    }

    public override int GetHashCode()
    {
        return a.GetHashCode();
    }

    public static bool operator ==(TestSort left, TestSort right)
    {
        return left.A == right.A;
    }

    public static bool operator !=(TestSort left, TestSort right)
    {
        return left.A != right.A;
    }
    // 기본적으로 a 를 기준으로 정렬한다.
    // 같음의 기준은 a를 기준으로 판단.
}
