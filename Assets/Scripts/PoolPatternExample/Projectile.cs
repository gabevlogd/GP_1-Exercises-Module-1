using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private int _lifeTime = 3;

    public static event Action<Projectile> OnLifeTimeComplete;
    public static event Action<Projectile> OnProjectileHit;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(LifespanTimer());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }


    private void OnTriggerEnter(Collider other)
    {
        OnProjectileHit?.Invoke(this);
    }

    IEnumerator LifespanTimer()
    {
        yield return new WaitForSeconds(_lifeTime);
        OnLifeTimeComplete?.Invoke(this);
    }
}