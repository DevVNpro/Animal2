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

    [Header("BoosterButton")]
    [SerializeField] private Button shieldButton;
    [SerializeField] private Button hpButton;
    [SerializeField] private Button bomButton;
    [SerializeField] private Button BulletButton;
    [SerializeField] private Button settingButton;
    [Header("BoosterDisplay")]
    [SerializeField] private GameObject displayShield;
    [SerializeField] private GameObject displaybom;
    [SerializeField] private GameObject displayBullet;

    [Header("other")]
    public int timeUseBooster;
    private bool stopTime;
    private Tween tween;

    

    [SerializeField] private TextMeshProUGUI timeCount;
    private void Awake()
    {
        //AddEvent
        shieldButton.onClick.AddListener(UseShieldButton);
        BulletButton.onClick.AddListener(UseBulletButton);

        hpButton.onClick.AddListener(UseHpButton);
        bomButton.onClick.AddListener(UseBom);
        settingButton.onClick.AddListener(ResetLevel);

        Rxmanager.UseHealth.Subscribe((tmp) =>
        {
            AddUiHp();
        }).AddTo(this);
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
        Rxmanager.UseBom.Subscribe((tpm) =>
        {
            StartCoroutine(AnimBomUI());
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
            if (GamePlayManager.countTime >= 0)
            {
                GamePlayManager.countTime -= Time.deltaTime;
                timeCount.text = Mathf.FloorToInt(GamePlayManager.countTime).ToString() + 's';
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
         Rxmanager.UseShield.OnNext(timeUseBooster);
         StartCoroutine(AnimShieldUI());
     }

     private IEnumerator AnimShieldUI()
     {
        displayShield.SetActive(true);
        int timeCount = 0;
        DOVirtual.Float(timeCount, 1, timeUseBooster, (timeCount) =>
          {

              displayShield.transform.GetChild(1).GetComponent<Image>().fillAmount = timeCount;
          });
        shieldButton.gameObject.GetComponent<Image>().color = new Color(0.8f,0.8f,0.8f);
        shieldButton.interactable = false;
        yield return  new WaitForSeconds(timeUseBooster);
        shieldButton.gameObject.GetComponent<Image>().color = new Color(1f,1f,1f);
        shieldButton.interactable = true;
        displayShield.SetActive(false);


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
        Rxmanager.UseBom.OnNext(timeUseBooster);
        StartCoroutine(AnimBomUI());
    }
    private IEnumerator AnimBomUI()
    {
        displaybom.SetActive(true);
        int timeCount = 0;
        DOVirtual.Float(timeCount, 1, timeUseBooster, (timeCount) =>
        {

            displaybom.transform.GetChild(1).GetComponent<Image>().fillAmount = timeCount;
        });
        bomButton.gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        bomButton.interactable = false;
        yield return new WaitForSeconds(timeUseBooster);
        bomButton.gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f);
        bomButton.interactable = true;
        displaybom.SetActive(false);
    }

    #endregion
    #region Bullet
    private void UseBulletButton()
    {
        StartCoroutine(AnimBulletUI());

    }
    IEnumerator AnimBulletUI()
    {
        displayBullet.SetActive(true);
        int timeCount = 0;
        DOVirtual.Float(timeCount, 1, timeUseBooster, (timeCount) =>
        {

            displayBullet.transform.GetChild(1).GetComponent<Image>().fillAmount = timeCount;
        });
        BulletButton.gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        BulletButton.interactable = false;
        yield return new WaitForSeconds(timeUseBooster);
        BulletButton.gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f);
        BulletButton.interactable = true;
        displayBullet.SetActive(false);
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
