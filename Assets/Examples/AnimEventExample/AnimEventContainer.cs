using System;
using UnityEngine;

public class AnimEventContainer : MonoBehaviour
{
    public event Action OnEnableParry;
    public void EnableParry()
    {
        OnEnableParry?.Invoke();
        Debug.Log("AnimEventTest: EnableParry");
    }

    public event Action OnDisableParry;
    public void DisableParry()
    {
        OnDisableParry?.Invoke();
        Debug.Log("AnimEventTest: DisableParry");
    }
}
