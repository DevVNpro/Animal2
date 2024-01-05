using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
public class GameWinPanel : MonoBehaviour
{
    [Header("ref")]
    [SerializeField] private List<GameObject> starImages;
    [SerializeField] private TextMeshProUGUI textCoin;
    [SerializeField] private TextMeshProUGUI textTime;
    [SerializeField] private TextMeshProUGUI textTimeCoin;
    [SerializeField] private TextMeshProUGUI textTotalCoin;



    [Header("other")]
    private int totalCoinInMap=0;
    public Ease easeOption;
    void Start()
    {
        StartCoroutine(OnActive());
    }
    IEnumerator OnActive()
    {
        UpdateTextCoin();
        UpdateTextTimeLeft();
        UpdateTextTotal();
        #region UpdateStar
        var starCount = GamePlayManager.sumStar;
        Debug.Log("StarCount :" + starCount);
        for(int  i = 0; i<starCount; i++)
        {
            starImages[i].SetActive(true);
            starImages[i].transform.DOScale(1, 1f).SetEase(easeOption);
            yield return new WaitForSeconds(1f);
        }
        #endregion



    }
    public void UpdateTextCoin()
    {
        var initValue = 0;
        var coinCount = GamePlayManager.sumCoin;
        totalCoinInMap += coinCount;
        DOVirtual.Float(initValue, coinCount, 2f, (initValue) =>
        {
            textCoin.text = Mathf.FloorToInt(initValue).ToString();
        });
    }
    public void UpdateTextEnemy()
    {

    }
    public void UpdateTextTimeLeft()
    {
        var initValue = 0;
        var timeLeft = Mathf.FloorToInt(GamePlayPanel.countTime);
        var initValuecoin = 0;
        var timeLeftCoin = timeLeft / 10;
        totalCoinInMap += timeLeftCoin;

        DOVirtual.Float(initValue, timeLeft, 2f, (initValue) => {
            textTime.text = Mathf.FloorToInt(initValue).ToString()+'s';
        });
        DOVirtual.Float(initValuecoin, timeLeftCoin, 2f, (initValuecoin) => {
            textTimeCoin.text = Mathf.FloorToInt(initValuecoin).ToString();
        });
    }
    public void UpdateTextTotal()
    {
        var initValue = 0;
        DOVirtual.Float(initValue, totalCoinInMap, 2f, (initValue) => {
            textTotalCoin.text = Mathf.FloorToInt(initValue).ToString();
        });

    }
    public void ResetLevel()
    {
        SceneManager.LoadScene(0);
    }
}
