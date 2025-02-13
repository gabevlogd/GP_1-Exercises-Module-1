using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float health;

    private float _hp;

    public static Action<float> OnHPChanged;

    private void Start()
    {
        _hp = health;
        OnHPChanged?.Invoke(_hp);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            RemoveHP(10);
        }
    }


    private void SetHP(float newValue)
    {
        if (newValue == _hp) return;

        _hp = newValue;

        OnHPChanged?.Invoke(_hp);
    }

    private void AddHP(float valueToAdd)
    {
        float newValue = _hp + valueToAdd;
        SetHP(newValue);
    }

    private void RemoveHP(float valueToRemove)
    {
        float newValue = _hp - valueToRemove;
        SetHP(newValue);
    }

}
