using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BoosterManager : MonoBehaviour
{
        
    [Header("Ref")] 
    [SerializeField] private GameObject shieldBooster;
    [SerializeField] private GameObject bomBooster;
    [SerializeField] private GameObject BulletBooster;
    [SerializeField] private CharacterHealth _characterHealth;

    private void Awake()
    {
        Rxmanager.UseShield.Subscribe((time) =>
        {
            StartCoroutine(ActiveShield(time));
        }).AddTo(this);
        Rxmanager.UseHealth.Subscribe((tmp) =>
        {
            StartCoroutine(ActiveHp());
        }).AddTo(this);
        Rxmanager.UseBom.Subscribe((time) => {
            StartCoroutine(ActiveBom(time));
        }).AddTo(this);
    }

    IEnumerator ActiveShield(int time)
    {
        shieldBooster.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        shieldBooster.gameObject.SetActive(false);
    }

    IEnumerator ActiveHp()
    {
        _characterHealth.AddHealth();
        yield return  new WaitForSeconds(0.1f);
    }
    IEnumerator ActiveBom(int time)
    {
        bomBooster.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        bomBooster.gameObject.SetActive(false);
    }    
    
}
