using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;

public class Test08_ListSort : TestBase
{
    public enum SortType
    {
        intType,
        floatType,
        stringType
    }

    public enum OrderType
    {
        Ascending,
        Descending
    }

    public SortType sortType = SortType.intType;
    public OrderType orderType = OrderType.Ascending;

    protected override void Test1_performed(InputAction.CallbackContext context)
    {
        List<int> list = new List<int>() { 1, 10, 5, 7, 3, 4, 2, 1 };
        Debug.Log("전");
        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log(list[i]);
        }
        list.Sort();
        Debug.Log("후");
        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log(list[i]);
        }
    }

    protected override void Test2_performed(InputAction.CallbackContext context)
    {
        List<Node> list = new List<Node>();

        Node temp = new Node(0, 0);
        temp.G = 10;
        list.Add(temp);

        temp = new Node(1, 0);
        temp.G = 20;
        list.Add(temp);

        temp = new Node(4, 0);
        temp.G = 50;
        list.Add(temp);

        temp = new Node(3, 0);
        temp.G = 40;
        list.Add(temp);

        temp = new Node(2, 0);
        temp.G = 30;
        list.Add(temp);

        Debug.Log("전");
        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log(list[i].F);
        }
        list.Sort();
        Debug.Log("후");
        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log(list[i].F);
        }
    }

    protected override void Test3_performed(InputAction.CallbackContext context)
    {
        Node a = new Node(0, 0);
        Node b = new Node(1, 0);

        Debug.Log(a.GetHashCode());
        Debug.Log(b.GetHashCode());
    }

    protected override void Test4_performed(InputAction.CallbackContext context)
    {
        // sortType 에 따라 TestSort를 정렬
        TestSort temp;
        List<TestSort> list = new List<TestSort>();

        temp = new(0, 1.0f, "aaa");
        list.Add(temp);
        temp = new(4, 11.0f, "bbb");
        list.Add(temp);
        temp = new(3, 31.0f, "eee");
        list.Add(temp);
        temp = new(1, 21.0f, "ddd");
        list.Add(temp);
        temp = new(2, 51.0f, "ccc");
        list.Add(temp);

        switch (sortType)
        {
            case SortType.intType:
                if(orderType == OrderType.Ascending)
                {
                    list.Sort();
                }
                else
                {
                    list.Sort((left, right) => right.A.CompareTo(left.A));
                }
                
                break;
            case SortType.floatType:
                if (orderType == OrderType.Ascending)
                {
                    list.Sort((left, right) => left.B.CompareTo(right.B));
                }
                else
                {
                    list.Sort((left, right) => right.B.CompareTo(left.B));
                }
                break;
            case SortType.stringType:
                if (orderType == OrderType.Ascending)
                {
                    list.Sort((left, right) => left.C.CompareTo(right.C));
                }
                else
                {
                    list.Sort((left, right) => right.C.CompareTo(left.C));
                }
                break;
        }

        TestPrint(list);
        //Comparison<TestSort> comparison = (x, y) => y.C.CompareTo(x.C);
    }
    void TestPrint(List<TestSort> list)
    {
        Debug.Log("리스트 출력");
        foreach (var item in list)
        {
            Debug.Log($"{item.A}, {item.B}, {item.C}");
        }
    }
}