using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BoosterManager : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float timeUseShield;
    [SerializeField] private float timeUseBom;
    [SerializeField] private float timeUseBullet;

    
    
    [Header("Ref")] 
    [SerializeField] private GameObject shieldBooster;
    [SerializeField] private GameObject bomBooster;
    [SerializeField] private GameObject BulletBooster;
    [SerializeField] private CharacterHealth _characterHealth;

    private void Awake()
    {
        Rxmanager.UseShield.Subscribe((tmp) =>
        {
            StartCoroutine(ActiveShield());
        }).AddTo(this);
        Rxmanager.UseHealth.Subscribe((tmp) =>
        {
            StartCoroutine(ActiveHp());
        }).AddTo(this);
        Rxmanager.UseBom.Subscribe((tmp) => {
            StartCoroutine(ActiveBom());
        }).AddTo(this);
    }

    IEnumerator ActiveShield()
    {
        shieldBooster.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeUseShield);
        shieldBooster.gameObject.SetActive(false);
    }

    IEnumerator ActiveHp()
    {
        _characterHealth.AddHealth();
        yield return  new WaitForSeconds(0.1f);
    }
    IEnumerator ActiveBom()
    {
        bomBooster.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeUseBom);
        bomBooster.gameObject.SetActive(false);
    }    
    
}
