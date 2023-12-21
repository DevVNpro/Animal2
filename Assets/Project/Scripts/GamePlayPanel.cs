using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class GamePlayPanel : MonoBehaviour
{ [Header("HpPlayerUI")]
    [SerializeField] private List<GameObject> hpImage;

    [SerializeField] private Button shieldButton;
    [SerializeField] private Button hpButton;
    [SerializeField] private Button bomButton;
    [SerializeField] private Button bulletButton;

    

    [SerializeField] private TextMeshProUGUI timeCount;
    private float countTime = 180;
    private void Awake()
    {
        shieldButton.onClick.AddListener(UseShieldButton);
        hpButton.onClick.AddListener(UseHpButton);
        Rxmanager.DeDuctHpPlayer.Subscribe((deduct) =>
        {
            DeductUiHp(deduct);
        }).AddTo(this);
    }

    #region CountDownTime
    private void Update()
    {
        CountDownTime();
    }

    private void CountDownTime()
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
    

    #endregion
    
     private void DeductUiHp(int deduct)
     {
         int cnt = 0;
        for (int i = 2; i>=0; i--)
        {
            if (hpImage[i].activeSelf)
            {
                hpImage[i].SetActive(false);
                cnt++;
            }

            if (cnt == deduct) return; 
        }
     }

     private void UseHpButton()
     {
         Rxmanager.UseHealth.OnNext(true);
         AddUiHp();
         
     }
     private void AddUiHp()
     {
         if(hpImage[2].activeSelf) return;
         for (int i = 0; i <= 2; i++)
         {
             if (!hpImage[i].activeSelf)
             {
                 hpImage[i].SetActive(true);
                 return;
             }
         }
     }

     private void UseShieldButton()
     {
         Rxmanager.UseShield.OnNext(true);
         StartCoroutine(AnimShieldUI());
     }

     private IEnumerator AnimShieldUI()
     {
         shieldButton.gameObject.GetComponent<Image>().color = new Color(0.5f,0.5f,0.5f);
         shieldButton.interactable = false;
        yield return  new WaitForSeconds(7f);
        shieldButton.gameObject.GetComponent<Image>().color = new Color(1f,1f,1f);
        shieldButton.interactable = true;

         
     }

}
