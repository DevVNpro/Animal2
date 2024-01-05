using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using  UnityEngine.SceneManagement;

public class GamePlayPanel : MonoBehaviour
{ [Header("HpPlayerUI")]
    [SerializeField] private List<GameObject> hpImage;
    [SerializeField] private List<RectTransform> starImage;
    [SerializeField] private GameObject star;
    [SerializeField] private GameObject parentStar;

    [SerializeField] private Button shieldButton;
    [SerializeField] private Button hpButton;
    [SerializeField] private Button bomButton;
    [SerializeField] private Button settingButton;
    private bool stopTime;
    private Tween tween;

    

    [SerializeField] private TextMeshProUGUI timeCount;
    public static float countTime;
    private void Awake()
    {
        countTime = 200;
        //AddEvent
        shieldButton.onClick.AddListener(UseShieldButton);
        hpButton.onClick.AddListener(UseHpButton);
        bomButton.onClick.AddListener(UseBom);
        settingButton.onClick.AddListener(ResetLevel);
        
        //RX
        Rxmanager.DeDuctHpPlayer.Subscribe((deduct) =>
        {
            DeductUiHp(deduct);
        }).AddTo(this);
        Rxmanager.PickStar.Subscribe((Vector3 vector) =>
        {
            MoveStarToUI(vector);
        }).AddTo(this);
        Rxmanager.PlayWin.Subscribe((tmp) =>
        {
            stopTime = true;
        }).AddTo(this);
        Rxmanager.UseShield.Subscribe((tmp) =>
        {
            StartCoroutine(AnimShieldUI());
        }).AddTo(this);
    }

    #region CountDownTime
    private void Update()
    {
        CountDownTime();
    }

    private void CountDownTime()
    {
        if (!stopTime)
        {
            if (countTime >= 0)
            {
                countTime -= Time.deltaTime;
                timeCount.text = Mathf.FloorToInt(countTime).ToString() + 's';
            }
            else
            {
                Rxmanager.PlayerDie.OnNext(true);

            }
        }
    }


    #endregion
    #region Hp
    private void DeductUiHp(int deduct)
    {
        int cnt = 0;
        for (int i = 2; i >= 0; i--)
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
    #endregion
    #region Shield
    private void UseShieldButton()
     {
         Rxmanager.UseShield.OnNext(true);
         StartCoroutine(AnimShieldUI());
     }

     private IEnumerator AnimShieldUI()
     {
         shieldButton.gameObject.GetComponent<Image>().color = new Color(0.8f,0.8f,0.8f);
         shieldButton.interactable = false;
        yield return  new WaitForSeconds(7f);
        shieldButton.gameObject.GetComponent<Image>().color = new Color(1f,1f,1f);
        shieldButton.interactable = true;

         
     }
    #endregion
    #region Star
    private void MoveStarToUI(Vector2 vector)
    {
        for(int i = 0; i<= 2; i++)
        {
            if (starImage[i].gameObject.activeSelf)
            {
                GameObject starUI = Instantiate(star, vector, transform.rotation, parentStar.transform);
                RectTransform starUIRectTransform = starUI.GetComponent<RectTransform>();
               tween = starUIRectTransform.DOAnchorPos3D(new Vector3(starImage[i].anchoredPosition.x, starImage[i].anchoredPosition.y,0f), 1f).SetEase(Ease.Linear).OnComplete(()=>
                {
                    starImage[i].gameObject.SetActive(false);
                 });
                break;
            }
        }

    }

    #endregion
    #region Bom
    private void UseBom()
    {
        Rxmanager.UseBom.OnNext(true);
        StartCoroutine(AnimBomUI());
    }
    private IEnumerator AnimBomUI()
    {
        bomButton.gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        bomButton.interactable = false;
        yield return new WaitForSeconds(7f);
        bomButton.gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f);
        bomButton.interactable = true;


    }

    #endregion

    private void ResetLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDisable()
    {
        tween.Kill();
    }
}
