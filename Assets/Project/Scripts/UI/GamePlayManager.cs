using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DG.Tweening;
using Spine.Unity;
using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{

     [SerializeField]private  GameObject Character;
    [SerializeField] private AnimationReferenceAsset animIdleWhenLock;
    [ReadOnly(true)] public bool PlayerWin;
    [ReadOnly(true)] public bool Playerlose;
    public static int sumStar = 0;
    public static int sumCoin = 0;
    private void Awake()
    {

        Rxmanager.CollectCoin.Subscribe((b)=>
        {
            SaveCoinCollect();
        }).AddTo(this);
        Rxmanager.CollectStar.Subscribe((b) =>
        {
            SaveStarCollect();
        }).AddTo(this);
        Rxmanager.LockCharater.Subscribe((b) =>
        {
            Character.GetComponent<CharacterController>().enabled = false;
            Character.GetComponent<CharacterController>()._characterAnimation.PlayAnimation(animIdleWhenLock,true,1);

        }).AddTo(this);
        Rxmanager.FreeCharater.Subscribe((b) =>
        {
            Character.GetComponent<CharacterController>().enabled = true;
        }).AddTo(this);
    }
    public void SaveStarCollect()
    {
        sumStar += 1;
    }
    public void SaveCoinCollect()
    {
        sumCoin += 1;
    }

    void Start()
    {
        Application.targetFrameRate = 60;
    }
    private void OnEnable()
    {
        SceneManager.sceneUnloaded += SceneManagerOnSceneUnloaded;
    }

    private static void SceneManagerOnSceneUnloaded(Scene scene)
    {
        DOTween.KillAll();

    }

    private void OnDisable()
    {
        SceneManager.sceneUnloaded -= SceneManagerOnSceneUnloaded;
    }

}
