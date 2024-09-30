using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    Light2D light2D;

    private void Awake()
    {
        light2D = GetComponent<Light2D>();
    }

    private void Start()
    {
        Player player = GameManager.Instance.Player;
        player.onDie += OnDie;
    }

    private void OnDie()
    {
        FieldInfo field = typeof(Light2D).GetField("m_ApplyToSortingLayers", BindingFlags.NonPublic | BindingFlags.Instance);

        int[] sortingLayers = new int[] { SortingLayer.NameToID("Player") };

        field.SetValue(light2D, sortingLayers);
    }
}
