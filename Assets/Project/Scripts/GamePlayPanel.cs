using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UniRx;

public class GamePlayPanel : MonoBehaviour
{ [Header("HpPlayerUI")]
    [SerializeField] private List<GameObject> hpImage;

    [SerializeField] private TextMeshProUGUI timeCount;
    private float countTime = 180;
    private void Awake()
    {
        Rxmanager.DeDuctHpPlayer.Subscribe((deduct) =>
        {
            TurnOffWhenDeductHp();
        }).AddTo(this);
    }

    private void Update()
    {
        if (countTime >= 0)
        {
            countTime -= Time.deltaTime;
            timeCount.text = Mathf.FloorToInt(countTime).ToString()+'s';
        }
        else
        {
            Rxmanager.PlayerDie.OnNext(delegate { });

        }
    }
            private void TurnOffWhenDeductHp()
    {
        for (int i = 2; i>=0; i--)
        {
            if (hpImage[i].activeSelf)
            {
                hpImage[i].SetActive(false);
                return;
            }
        }
        
        
        
    }

}
