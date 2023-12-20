using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class UIGamePlayManager : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private GamePlayPanel _gamePlayPanel;
    [SerializeField] private GameOverPanel _gameOverPanel;

    private void Awake()
    {
        Rxmanager.PlayerDie.Subscribe(delegate
        {
            _gamePlayPanel.gameObject.SetActive(false);
            _gameOverPanel.gameObject.SetActive(true);
            float tmp = 0;
            DOVirtual.Float(tmp, 1, 2.5f, (tmp) =>
            {
                _gameOverPanel.gameObject.GetComponent<CanvasGroup>().alpha = tmp;
            });
        }).AddTo(this);
    }


}
